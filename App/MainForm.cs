using System;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using System.Drawing;
using SearchLib;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using Newtonsoft.Json;
using Match = SearchLib.Match;
using System.Collections.Concurrent;
using ThesaurusLib;

namespace _2ndbrainalpha
{
    public partial class MainForm : Form
    {
        bool _cancelled;
        SearchHelper _searchHelper;
        int _filesProcessed;
        string _currentFile;
        delegate void DelegateMethod(params object[] args);
        string _settingsFileName;
        private bool _expandedFirstNode;
        ConcurrentDictionary<string, TreeNode> _fileNodes;
        private int _lastHighlightLineStartIndex;
        private int _lastHighlightLineEndIndex;
        private int _lastSelectedLineNumber;
        private ConcurrentDictionary<string, FileMatchData> _matchResults;
        private bool _suspendFilters;
        private bool _lastFile;
        private ILogger _logger;
        private IAntonymLookup _antonymLookup;

        public IList<string> TargetWords => txtTargets.Text.Split('\n','\r')?.Select(w => w.Trim()).Where(w => w.Length > 0).Distinct().ToList() ?? new List<string>();

        public MainForm()
        {
            InitializeComponent();
            txtFileViewer.ScrollBars = RichTextBoxScrollBars.None;
            _searchHelper = new SearchHelper(CheckForCancellation, OnFile, OnFileMatch, OnMatch, OnException, OnComplete);
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _settingsFileName = $@"{path}\settings.txt";
            _antonymLookup = new AntonymLookup(path);
            _expandedFirstNode = false;
            _fileNodes = new ConcurrentDictionary<string, TreeNode>();
            _matchResults=new ConcurrentDictionary<string, FileMatchData>();
            _logger = new Logger(path);
        }

        #region Event handlers
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (/*string.IsNullOrWhiteSpace(txtSearch.Text) && */string.IsNullOrWhiteSpace(txtTargets.Text)) 
            {
                MessageBox.Show("Enter a search term(s)");
                return;
            }

            btnCancel.Visible = true;

            _currentFile = null;
            _filesProcessed = 0;
            _cancelled = false;
            tvMatches.Nodes.Clear();
            txtFileViewer.Text = "";
            _expandedFirstNode = false;
            _fileNodes.Clear();
            _lastHighlightLineStartIndex = _lastHighlightLineEndIndex = 0;
            _lastSelectedLineNumber = 0;
            _matchResults.Clear();
            _suspendFilters = true;
            _lastFile = false;

            // populate list box
            var lastTargets = new Dictionary<string, TargetWord>();
            foreach (TargetWord targetWord in lbTargets.Items)
            {
                lastTargets.Add(targetWord.Word, targetWord);
            }

            lbTargets.Items.Clear();
            foreach (var targetWord in TargetWords)
            {
                if (!lbTargets.Items.Cast<TargetWord>().Any(t => t.Word.ToLower() == targetWord.ToLower()))
                {
                    var matchCount = lastTargets.ContainsKey(targetWord) ? lastTargets[targetWord].MatchCount : 0;
                    var idx = lbTargets.Items.Add(new TargetWord(targetWord, matchCount));
                    lbTargets.SetItemChecked(idx, true);
                }
            }

            foreach (TargetWord targetWord in lbTargets.Items)
            {
                targetWord.MatchCount = 0;
            }

            // Spin off thread to do the search
            var tSearch = new Thread(new ParameterizedThreadStart(SearchThread));
            var searchParams = new SearchParams {Path = txtPath.Text, Filter = txtFilter.Text/*, SearchPattern = txtSearch.Text*/, TargetWords = TargetWords};
            tSearch.Start(searchParams);

            tvMatches.Focus();
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            this.dlgFolderBrowser = new FolderBrowserDialog();
            this.dlgFolderBrowser.RootFolder = Environment.SpecialFolder.MyComputer;
            this.dlgFolderBrowser.ShowNewFolderButton = false;

            // Show the FolderBrowserDialog.
            DialogResult result = dlgFolderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPath.Text = dlgFolderBrowser.SelectedPath;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cancelled = true;
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void tvMatches_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            var parentNode = e.Node.Parent;

            if (parentNode == null || e.Bounds.X == -1)
            {
                e.DrawDefault = true;
            }
            else
            {
                var wordsToHighlight =
                    TargetWords
                    .ToArray();

                var node = e.Node;
                var match = node.Tag as Match;

                DrawTextWithHighlightedWords(node,
                                             e.Graphics,
                                             e.Bounds);
            }
        }

        private void tvMatches_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var node = e.Node;
            SelectNode(node, true);
        }

        private void txtFileViewer_Click(object sender, EventArgs e)
        {
            SetLineAndColumn();
        }

        private void txtFileViewer_KeyUp(object sender, KeyEventArgs e)
        {
            SetLineAndColumn();
        }

        private void txtFileViewer_KeyDown(object sender, KeyEventArgs e)
        {
            SetLineAndColumn();
        }

        private void btnAddSynonyms_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtThesaurusLookup.Text))
            {
                MessageBox.Show("Enter a word");
                return;
            }

            var searchPattern = txtThesaurusLookup.Text;
            if (!TargetWords.Any(t => t.Equals(searchPattern, StringComparison.OrdinalIgnoreCase)))
            {
                AppendTextBoxText(txtTargets, searchPattern);
            }

            var targetWords = TargetWords;

            // Look up synonym of search word
            var synonyms = ThesaurusLib.Lookup.GetSynonyms(searchPattern).Select(s => s.Word).Distinct().ToList();
            foreach (var synonym in synonyms)
            {
                if (!targetWords.Any(t => t.Equals(synonym, StringComparison.OrdinalIgnoreCase)))
                {
                    AppendTextBoxText(txtTargets, synonym);
                }
            }
        }

        private void btnAddAntonyms_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtThesaurusLookup.Text))
            {
                MessageBox.Show("Enter a word");
                return;
            }

            var searchPattern = txtThesaurusLookup.Text;
            var targetWords = TargetWords;

            // Look up antonym of search word
            var antonyms = _antonymLookup.GetAntonyms(searchPattern)?.Distinct()?.ToList();
            if (antonyms == null)
            {
                MessageBox.Show("No antonyms found");
                return;
            }
            foreach (var antonym in antonyms)
            {
                if (!targetWords.Any(t => t.Equals(antonym, StringComparison.OrdinalIgnoreCase)))
                {
                    AppendTextBoxText(txtTargets, antonym);
                }
            }

        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            UpdateLineNumbers();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            CopySelectedNodeToClipboard(tvMatches);
        }

        private void tvMatches_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.C))
            {
                CopySelectedNodeToClipboard(tvMatches);
                e.SuppressKeyPress = true;
            }
            else
            {
                //SelectNode(tvMatches.SelectedNode, true);
            }
        }

        private void tvMatches_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData != (Keys.Control | Keys.C))
            {
                SelectNode(tvMatches.SelectedNode, true);
            }
        }

        private void tvMatches_MouseDown(object sender, MouseEventArgs e)
        {
            // Make sure this is the right button.
            if (e.Button != MouseButtons.Right) return;

            // Select this node.
            TreeNode node = tvMatches.GetNodeAt(e.X, e.Y);
            tvMatches.SelectedNode = node;

            // See if we got a node.
            if (node == null) return;

            ctxMenuFileNode.Show(tvMatches, new Point(e.X, e.Y));
        }

        private void txtFileViewer_SelectionChanged(object sender, EventArgs e)
        {
            lblSelection.Text = txtFileViewer.SelectedText.Length.ToString();
        }

        private void cbExpandAll_CheckedChanged(object sender, EventArgs e)
        {
            var expandAll = cbExpandAll.Checked;
            TreeNode topNode = tvMatches.TopNode;
            tvMatches.SuspendLayout();
            foreach (TreeNode node in tvMatches.Nodes)
            {
                var file = (string)node.Tag;
                if (_fileNodes.ContainsKey(file))
                {
                    if (expandAll)
                    {
                        node.ExpandAll();
                    }
                    else
                    {
                        node.Collapse();
                    }
                }
            }

            tvMatches.TopNode = topNode;
            tvMatches.ResumeLayout();
        }

        private void txtTargets_TextChanged(object sender, EventArgs e)
        {
            var count = TargetWords.Count;
            lblTargetCount.Text = $"{count} item(s)";
        }

        private void lbTargets_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate {
                if (lbTargets.Items.Count == TargetWords.Count)
                {
                    _suspendFilters = false;
                }

                if (!_suspendFilters && ((TargetWord)lbTargets.Items[e.Index]).MatchCount > 0)
                {
                    // filter tree view on selected indices
                    FilterMatches();
                    tvMatches.Invalidate();
                }
            });
        }

        private void cbTargetsToggle_CheckedChanged(object sender, EventArgs e)
        {
            txtFileViewer.SuspendLayout();
            txtLineNumbers.SuspendLayout();
            TreeNode topNode = tvMatches.TopNode;
            tvMatches.SuspendLayout();
            _suspendFilters = true;
            for (int i=0; i < lbTargets.Items.Count; i++)
            {
                lbTargets.SetItemChecked(i, cbTargetsToggle.Checked);
            }
            // filter tree view on selected indices
            FilterMatches();
            _suspendFilters = false;
            tvMatches.TopNode = topNode;
            tvMatches.ResumeLayout();
            txtFileViewer.ResumeLayout();
            txtLineNumbers.ResumeLayout();
        }
        #endregion

        #region Private methods
        private void CopySelectedNodeToClipboard(TreeView tv)
        {
            if (tvMatches.SelectedNode != null)
            {
                Clipboard.SetText(tv.SelectedNode.Text);
            }
        }

        private void SelectNode(TreeNode node, bool scrollToCaret = false)
        {
            if (node == null)
                return;

            var match = node.Tag as SearchLib.Match;
            string file;

            if (match == null)
            {
                // file node
                file = node.Tag as string;
                UnhighlightPreviousSelectedLine(node);
                foreach (TreeNode childNode in node.Nodes)
                {
                    SelectNode(childNode, scrollToCaret);
                }
            }
            else
            {
                // match node
                file = match.File;
            }

            if (file == null)
            {
                return;
            }

            // Load file if not already loaded
            if (file != _currentFile)
            {
                _currentFile = file;
                txtFileViewer.Text = File.ReadAllText(file);
            }

            if (match != null)
            {
                if (scrollToCaret)
                {
                    // unhighlight previously highlighted line, if any
                    UnhighlightPreviousSelectedLine(node.Parent);

                    // highlight line
                    HighlightSelection(match.LineStartIndex, match.LineEndIndex, Color.LightGoldenrodYellow);
                    _lastHighlightLineStartIndex = match.LineStartIndex;
                    _lastHighlightLineEndIndex = match.LineEndIndex;
                    _lastSelectedLineNumber = match.LineNumber;

                    // Scroll view if selection is out of visible range
                    if (match.Position > txtFileViewer.BottomVisibleCharIndex || match.Position < txtFileViewer.TopVisibleCharIndex)
                    {
                        txtFileViewer.ScrollToCaret();
                    }
                }

                HighlightSelection(match.Position, match.Position + match.Word.Length, Color.Orange);
            }
            else
            {
                txtFileViewer.Select(0, 0);
            }

            SetLineAndColumn();
        }

        private void UnhighlightPreviousSelectedLine(TreeNode fileNode)
        {
            if (_lastHighlightLineStartIndex >= 0 && _lastHighlightLineEndIndex > 0)
            {
                HighlightSelection(_lastHighlightLineStartIndex, _lastHighlightLineEndIndex, txtFileViewer.BackColor);

                // restore matched word highlights
                foreach (TreeNode childNode in fileNode.Nodes)
                {
                    var m = childNode.Tag as Match;
                    if (m.LineNumber == _lastSelectedLineNumber)
                    {
                        SelectNode(childNode);
                    }
                }
            }
        }

        private void HighlightSelection(int startIndex, int endIndex, Color color)
        {
            //Debug.WriteLine($"start index = {startIndex}, length = {endIndex - startIndex}");
            txtFileViewer.Select(startIndex, endIndex - startIndex);
            txtFileViewer.SelectionBackColor = color;
        }

        private void UpdateLineNumbers()
        {
            var sbLineNumbers = new StringBuilder();
            var startCharIdx = 0;
            var lineNum = 0;
            var prevLineNum = lineNum;
            var displayLineNum = 0;
            foreach (var line in txtFileViewer.Lines)
            {
                prevLineNum = lineNum;
                //if (!string.IsNullOrEmpty(line))
                {
                    sbLineNumbers.Append($"{displayLineNum + 1}");
                    displayLineNum++;
                }
                startCharIdx = startCharIdx + line.Length + 1 /* for newline */;
                lineNum = txtFileViewer.GetLineFromCharIndex(startCharIdx);
                for (int i = 0; i < lineNum - prevLineNum; i++)
                {
                    sbLineNumbers.Append($"{Environment.NewLine}");
                }
            }
            txtLineNumbers.Text = sbLineNumbers.ToString();
        }

        private void LoadSettings()
        {
            if (!File.Exists(_settingsFileName))
                return;

            var settingsText = File.ReadAllText(_settingsFileName);
            if (settingsText != null) {
                var settings = JsonConvert.DeserializeObject<Settings>(settingsText);
                if (settings != null) {
                    txtPath.Text = settings.Path;
                    txtFilter.Text = settings.Filter;
                    //txtSearch.Text = settings.SearchText;
                    if (settings.TargetWords != null) {
                        foreach (var word in settings.TargetWords) {
                            AppendTextBoxText(txtTargets, word);
                        }
                    }
                }
            }            
        }

        private void SaveSettings()
        {
            var settings = new Settings();
            settings.Path = txtPath.Text;
            settings.Filter = txtFilter.Text;
            //settings.SearchText = txtSearch.Text;
            settings.TargetWords = 
                txtTargets
                    .Text
                    .Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
            var selectedNode = tvMatches.SelectedNode?.Tag as SearchLib.Match;
            settings.SelectedNode = selectedNode != null ? JsonConvert.SerializeObject(selectedNode) : null;
            File.WriteAllText(_settingsFileName, JsonConvert.SerializeObject(settings));
        }

        // Display status message in the toolstrip area
        private void SetStatusTxt(string msg)
        {
            InvokeIfRequired(
                    x =>
                    {
                        lblStatusText.Text = (string)x[0];
                    },
                    msg);
        }

        private void AppendTextBoxText(TextBox tb, string text)
        {
            InvokeIfRequired(
                    x =>
                    {
                        tb.Text += $"{x[0]}{Environment.NewLine}";
                    },
                    text);
        }

        private void InvokeIfRequired(DelegateMethod method, params object[] args) 
        {
            if (this.InvokeRequired)
            {
                this.Invoke(method, new object[] { args });
            }
            else
            {
                method(args);
            }
        }

        private void SetProgressBarValue(int value)
        {
            InvokeIfRequired(x => { progressBarFiles.Value = (int)x[0]; lblFileCount.Text = x[0].ToString(); }, value);
        }

        private void SetProgressBarMaximum(int max)
        {
            InvokeIfRequired(x => { progressBarFiles.Maximum = (int)x[0]; lblMaxFileCount.Text = x[0].ToString(); }, max);
        }

        private void SearchThread(object arg)
        {
            try
            {
                SetStatusTxt("");
                var searchParams = arg as SearchParams;
                if (searchParams == null)
                {
                    SetStatusTxt("Error in search parameters.");
                    return;
                }

                var targetWords = searchParams.TargetWords;
                /*var searchPattern = searchParams.SearchPattern;
                if (!string.IsNullOrWhiteSpace(searchPattern) && !targetWords.Contains(searchPattern)) 
                {
                    targetWords.Add(searchPattern);
                    AppendTextBoxText(txtTargets, searchPattern);
                }*/

                var path = $"{searchParams.Path}";
                if (!string.IsNullOrEmpty(path))
                {
                    var filter = string.IsNullOrWhiteSpace(searchParams.Filter) ? "*.*" : searchParams.Filter; 
                    string[] files = Directory.GetFiles(path, filter, SearchOption.AllDirectories);
                    var fileCount = files.Length;
                    SetProgressBarMaximum(fileCount);

                    _searchHelper.SearchFiles(files, targetWords);    
                }
            }
            catch (ThreadAbortException tex)
            {
                LogEx(tex);
                SetStatusTxt($"{tex.Message}");
            }
            catch (Exception ex)
            {
                LogEx(ex);
                SetStatusTxt($"{ex.Message}");
            }
        }

        private bool CheckForCancellation()
        {
            Thread.Sleep(0);
            return _cancelled;
        }

        private void OnFile(string file)
        {
            InvokeIfRequired(x =>
            {
                int filesProcessed = 0;
                int fileCount = 0;
                int.TryParse(x[0].ToString(), out filesProcessed);
                lblFileCount.Text = filesProcessed.ToString();
                
                progressBarFiles.Value = filesProcessed;
                if (filesProcessed % 5 == 0)
                {
                    this.Refresh();
                }

                int.TryParse(lblMaxFileCount.Text, out fileCount);
                if (filesProcessed == fileCount)
                {
                    btnCancel.Visible = false;
                    _lastFile = true;
                }
            }, Interlocked.Increment(ref _filesProcessed));
            if (_cancelled)
            {
                InvokeIfRequired(x => {
                    btnCancel.Visible = false;
                },  null);
                SetStatusTxt("Search was canceled.");
                return;
            }
        }

        private void OnFileMatch(string file, int count)
        {
            _lastHighlightLineStartIndex = _lastHighlightLineEndIndex = 0;
            _lastSelectedLineNumber = 0;
            if (!_matchResults.ContainsKey((file)))
            {
                var fileMatchData = new FileMatchData(count);
                //Log($"_matchResults.Add ({file}, {fileMatchData.ToString()})");
                _matchResults.TryAdd(file, fileMatchData);
            }
            AddFileToResults(file, count);
        }

        private void OnMatch(SearchLib.Match match, int matchCount)
        {
            var fUpdateWordCount = new Action<Match>((m) =>
            {
                for (int i = 0; i < lbTargets.Items.Count; i++)
                {
                    var targetWord = lbTargets.Items[i] as TargetWord;
                    if (targetWord.Word.ToLower() == m.Word.ToLower())
                    {
                        targetWord.MatchCount++;
                        lbTargets.Items[i] = lbTargets.Items[i];
                    }
                }
            });
            if (this.InvokeRequired)
            {
                this.Invoke(fUpdateWordCount, match);
            }
            else
            {
                fUpdateWordCount(match);
            }

            if (_matchResults.ContainsKey(match.File))
            {
                var fileMatchData = _matchResults[match.File];
                fileMatchData.Matches.Add(match);
                if (_lastFile && fileMatchData.MatchCount == fileMatchData.Matches.Count)
                {
                    OnComplete();
                }
            }
            AddMatchToResults(match, matchCount, false);
        }

        private void AddFileToResults(string file, int count)
        {
            var fAddFileToResults =
                new Action<string, int>((f, c) =>
                {
                    var label = $"{f} ({c} items)";
                    var nodes = tvMatches.Nodes.Find(f, false);
                    TreeNode fileNode;
                    if (nodes.Length > 0)
                    {
                        fileNode = nodes[0];
                    }
                    else
                    {
                        fileNode = tvMatches.Nodes.Add(label);
                    }

                    _fileNodes.TryAdd(file, fileNode);

                    fileNode.Name = file;
                    fileNode.Tag = file;
                    if (tvMatches.SelectedNode == null && tvMatches.Nodes.Count > 0 && tvMatches.Nodes[0].Nodes.Count > 0)
                    {
                        tvMatches.SelectedNode = tvMatches.Nodes[0].Nodes[0];
                        SelectNode(tvMatches.SelectedNode, true);
                    }
                });
            if (this.InvokeRequired)
            {
                this.Invoke(fAddFileToResults, file, count);
            }
            else
            {
                fAddFileToResults(file, count);
            }
        }

        private void AddMatchToResults(Match match, int matchCount, bool selectNode=false)
        {
            var onMatch = new Action<SearchLib.Match>(x =>
            {
                var node = new TreeNode($"({1 + x.LineNumber},{1 + x.StartIndex}): {x.Line}");
                node.Tag = match;
                var nodes = tvMatches.Nodes.Find(match.File, false);
                if (nodes.Length > 0)
                {
                    var fileNode = nodes[0];
                    fileNode.Nodes.Add(node);
                    if (fileNode.Nodes.Count == matchCount && !_expandedFirstNode)
                    {
                        _expandedFirstNode = true;
                        fileNode.Expand();
                    }

                    if (selectNode)
                    {
                        SelectNode(node);
                    }
                }
            });

            if (this.InvokeRequired)
            {
                this.Invoke(onMatch, match);
            }
            else
            {
                onMatch(match);
            }
        }

        private void FilterMatches()
        {
            tvMatches.Nodes.Clear();
            foreach (TargetWord item in lbTargets.CheckedItems)
            {
                var word = item.Word;
                foreach (var file in _matchResults.Keys)
                {
                    var fileMatchData = _matchResults[file];
                    var matchCount = fileMatchData.MatchCount;
                    bool addedFile = false;
                    var filteredMatches = fileMatchData
                        .Matches
                        .Where(match => match.Word.ToLower() == word.ToLower())
                        .ToList();
                    var filteredMatchCount = filteredMatches.Count;

                    foreach (var match in filteredMatches)
                    {
                        if (!addedFile)
                        {
                            AddFileToResults(file, filteredMatchCount);
                            addedFile = true;
                        }
                        AddMatchToResults(match, filteredMatchCount, false);
                    }
                }
            }
        }

        private void OnException(string file, Exception ex)
        {
            LogEx(ex);
            SetStatusTxt($"ProcessFile: file = {file}, msg = {ex.Message}");
        }

        private void OnComplete()
        {
            // Remove targets checked listbox items with zero counts
            InvokeIfRequired((x) =>
            {
                /*_suspendFilters = true;
                for (int i=lbTargets.Items.Count - 1; i >= 0; i--)
                {
                    TargetWord item = lbTargets.Items[i] as TargetWord;
                    if (item.MatchCount == 0)
                    {
                        lbTargets.Items.RemoveAt(i);
                    }
                }

                var sItems = new List<TargetWord>();
                var lbItems = lbTargets.Items.Cast<TargetWord>().OrderByDescending(t => t.MatchCount);
                foreach (TargetWord word in lbItems)
                {
                    sItems.Add(word);
                }
                lbTargets.Items.Clear();
                foreach (TargetWord word in sItems)
                {
                    var idx = lbTargets.Items.Add(word);
                    lbTargets.SetItemChecked(idx, true);
                }

                _suspendFilters = false;*/
            }, null);
        }

        private void DrawTextWithHighlightedWords(TreeNode node, Graphics g, Rectangle bounds)
        {
            string text = node.Text;
            var match = node.Tag as Match;
            string wordToHighlight = match.Word;
            int startPosition = match?.StartIndex ?? 0;
            int offset = ($"({match.LineNumber+1},{match.StartIndex+1}): ").Length;

            var textSize = GetTextSize(g, text);

            //Default location is the node bounds location.
            var location = bounds.Location;

            //Add padding to the left of the text, which is half the difference between the node bounds width and the text width.
            location.Offset((bounds.Width - textSize.Width) / 2, 0);

            //Split text into substrings to highlight and not highlight.
            var words = Regex.Split(text, @"([,\s;\.\t\{\}\[\]()])").Where(w => w.Length > 0).ToArray();
            var wordsWithHighlightStatus = 
                Array.ConvertAll(
                    words,
                    (s) => Tuple.Create(s, wordToHighlight.ToLower() == s.ToLower()));

            var currentPos = 0;
            for (int i = 0; i < wordsWithHighlightStatus.Length; i++)
            {
                var wordWithHighlightStatus = wordsWithHighlightStatus[i];
                var word = wordWithHighlightStatus.Item1;
                
                var size = Size;

                //Draw the current word.
                size = GetTextSize(g, word);
                var highlight = wordWithHighlightStatus.Item2 && (currentPos - offset) == startPosition ;
                DrawText(g,
                     word,
                     location,
                     Color.Black,
                     highlight ? Color.Orange : Color.Transparent);
                location.Offset(size.Width, 0);
                currentPos += word.Length;
            }
        }

        private Size GetTextSize(Graphics g, string text)
        {
            return TextRenderer.MeasureText(g,
                                            text,
                                            tvMatches.Font,
                                            Size.Empty,
                                            TextFormatFlags.NoPadding);
        }
            
        private void DrawText(Graphics g, string text, Point location, Color foreColor, Color backColor)
        {
            TextRenderer.DrawText(g,
                               text,
                               tvMatches.Font,
                               location,
                               foreColor,
                               backColor,
                               TextFormatFlags.NoPadding);
        }

        private void SetLineAndColumn()
        {
            var remainder = txtFileViewer.SelectionStart;
            int lineNum = 0;
            int col = 1;
            bool found = false;
            var lines = txtFileViewer.Text?.Split(Environment.NewLine.ToCharArray());
            if (lines != null)
            {
                for (int n = 0; n < lines.Length; n++)
                {
                    var line = lines[n];
                    remainder = remainder - line.Length - 1;
                    if (remainder >= 0)
                    {
                        lineNum++;
                    }
                    else
                    {
                        found = true;
                        col = line.Length + remainder + 2;
                        break;
                    }
                }
            }
            lblLineNumber.Text = found ? (lineNum + 1).ToString() : string.Empty;
            lblColumnNumber.Text = found ? col.ToString() : string.Empty;
            lblPosition.Text = (txtFileViewer.SelectionStart + 1).ToString();
            UpdateLineNumbers();
        }

        private void Log(string msg)
        {
            _logger.Log(msg);
        }
        
        private void LogEx(Exception ex)
        {
            _logger.LogException(ex);
        }

        #endregion
    }
}
