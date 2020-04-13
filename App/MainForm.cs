using SynonymsLib;
using System;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using System.Drawing;
using SearchLib;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using Newtonsoft.Json;
using Match = SearchLib.Match;

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
        Dictionary<string, TreeNode> _fileNodes;
        private int _lastHighlightLineStartIndex;
        private int _lastHighlightLineEndIndex;

        public IList<string> TargetWords => txtTargets.Text.Split('\n','\r')?.Select(w => w.Trim()).Where(w => w.Length > 0).Distinct().ToList() ?? new List<string>();

        public MainForm()
        {
            InitializeComponent();
            txtFileViewer.ScrollBars = RichTextBoxScrollBars.None;
            _searchHelper = new SearchHelper(CheckForCancellation, OnFile, OnFileMatch, OnMatch, OnException, OnComplete);
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _settingsFileName = $@"{path}\settings.txt";
            _expandedFirstNode = false;
            _fileNodes = new Dictionary<string, TreeNode>();
        }

        #region Event handlers
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text) && string.IsNullOrWhiteSpace(txtTargets.Text)) 
            {
                MessageBox.Show("Enter a search term");
                return;
            }

            _currentFile = null;
            _filesProcessed = 0;
            _cancelled = false;
            tvMatches.Nodes.Clear();
            txtFileViewer.Text = "";
            _expandedFirstNode = false;
            _fileNodes.Clear();
            _lastHighlightLineStartIndex = _lastHighlightLineEndIndex = 0;

            // Spin off thread to do the recon
            var tSearch = new Thread(new ParameterizedThreadStart(SearchThread));
            var searchParams = new SearchParams {Path = txtPath.Text, Filter = txtFilter.Text, SearchPattern = txtSearch.Text, TargetWords = TargetWords};
            tSearch.Start(searchParams);
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

                DrawTextWithHighlightedWords(e.Node.Text,
                                             wordsToHighlight.ToArray<string>(),
                                             e.Graphics,
                                             e.Bounds,
                                             (e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected);
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
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Enter a search term");
                return;
            }

            var searchPattern = txtSearch.Text;
            var targetWords = TargetWords;
            //if (!targetWords.Contains(searchPattern))
            //{
            //    targetWords.Add(searchPattern);
            //    AppendTextBoxText(txtTargets, searchPattern);
            //}

            // Look up synonym of search word
            var synonyms = Lookup.GetSynonyms(searchPattern).Select(s => s.Word).Distinct().ToList();
            foreach (var synonym in synonyms)
            {
                if (!targetWords.Contains(synonym))
                {
                    AppendTextBoxText(txtTargets, synonym);
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

        private void txtTargets_TextChanged(object sender, EventArgs e)
        {
            var count = TargetWords.Count;
            var suffix = count > 0 ? "s" : "";
            lblTargetCount.Text = $"{count} item{suffix}";
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

        private void btnExpandCollapse_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in tvMatches.Nodes)
            {
                var file = (string)node.Tag;
                if (_fileNodes.ContainsKey(file))
                {
                    if (node.IsExpanded)
                    {
                        node.Collapse();
                    }
                    else
                    {
                        node.ExpandAll();
                    }
                }
            }
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
            var match = node.Tag as SearchLib.Match;
            string file;

            if (match == null)
            {
                file = node.Tag as string;
                foreach (TreeNode childNode in node.Nodes)
                {
                    SelectNode(childNode);
                }
            }
            else
            {
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
                    if (_lastHighlightLineStartIndex >= 0 && _lastHighlightLineEndIndex > 0)
                    {
                        HighlightSelection(_lastHighlightLineStartIndex, _lastHighlightLineEndIndex, txtFileViewer.BackColor);
                    }

                    // highlight line
                    HighlightSelection(match.LineStartIndex, match.LineEndIndex, Color.LightGoldenrodYellow);
                    _lastHighlightLineStartIndex = match.LineStartIndex;
                    _lastHighlightLineEndIndex = match.LineEndIndex;

                    if (match.Position > txtFileViewer.BottomVisibleCharIndex || match.Position < txtFileViewer.TopVisibleCharIndex)
                    {
                        txtFileViewer.ScrollToCaret();
                    }
                }

                txtFileViewer.Select(match.Position, match.Word.Length);
                txtFileViewer.SelectionBackColor = Color.Orange;
            }
            else
            {
                txtFileViewer.Select(0, 0);
            }

            SetLineAndColumn();
        }

        private void HighlightSelection(int startIndex, int endIndex, Color color)
        {
            Debug.WriteLine($"start index = {startIndex}, length = {endIndex - startIndex}");
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
                    txtSearch.Text = settings.SearchText;
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
            settings.SearchText = txtSearch.Text;
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

        private void InvokeIfRequiredNoArgs(DelegateMethod method) 
        {
            if (this.InvokeRequired)
            {
                this.Invoke(method);
            }
            else
            {
                method();
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

        private void AddFileToResults(string file, int count)
        {
            var fAddFileToResults = 
                new Action<string,int> ((f,c) =>
                {
                    var label = $"{f} ({c} items)";
                    var fileNode = tvMatches.Nodes.Add(label);

                    if (!_fileNodes.ContainsKey(file))
                    {
                        _fileNodes.Add(file, fileNode);
                    }

                    fileNode.Name = file;
                    fileNode.Tag = file;
                    if (tvMatches.SelectedNode == null && tvMatches.Nodes.Count > 0)
                    {
                        tvMatches.SelectedNode = tvMatches.Nodes[0];
                        SelectNode(tvMatches.SelectedNode);
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
                var searchPattern = searchParams.SearchPattern;
                if (!string.IsNullOrWhiteSpace(searchPattern) && !targetWords.Contains(searchPattern)) 
                {
                    targetWords.Add(searchPattern);
                    AppendTextBoxText(txtTargets, searchPattern);
                }

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
                SetStatusTxt($"{tex.Message}");
            }
            catch (Exception ex)
            {
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
            SetProgressBarValue(++_filesProcessed);
            if (_cancelled)
            {
                SetStatusTxt("Search was canceled.");
                return;
            }
        }

        private void OnFileMatch(string file, int count)
        {
            _lastHighlightLineStartIndex = _lastHighlightLineEndIndex = 0;
            AddFileToResults(file, count);
        }

        private void OnMatch(SearchLib.Match match, int matchCount)
        {
            var onMatch = new Action<SearchLib.Match>(x =>
            {
                var node = new TreeNode($"({1 + x.LineNumber},{1 + x.StartIndex}): {x.Line}");
                node.Tag = match;
                var fileNode = tvMatches.Nodes.Find(match.File, false)[0];
                fileNode.Nodes.Add(node);
                if (fileNode.Nodes.Count == matchCount && !_expandedFirstNode)
                {
                    _expandedFirstNode = true;
                    fileNode.Expand();
                }
                SelectNode(node);
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

        private void OnException(string file, Exception ex) {
            SetStatusTxt($"ProcessFile: file = {file}, msg = {ex.Message}");
        }

        private void OnComplete()
        {
        }

        private void DrawTextWithHighlightedWords(string text, string[] wordsToHighlight, Graphics g, Rectangle bounds, bool selected)
        {
            var textSize = GetTextSize(g, text);

            //Default location is the node bounds location.
            var location = bounds.Location;

            //Add padding to the left of the text, which is half the difference between the node bounds width and the text width.
            location.Offset((bounds.Width - textSize.Width) / 2, 0);

            //Split text into substrings to highlight and not highlight.
            var words = Regex.Split(text, @"([,\s;\.\t\{\}\[\]()])").Where(w => w.Length > 0).ToArray();
            var wordsWithHighlightStatus = Array.ConvertAll(words,
                                                            (s) => Tuple.Create(s, wordsToHighlight.Contains(s)));

            for (int i = 0; i < wordsWithHighlightStatus.Length; i++)
            {
                var wordWithHighlightStatus = wordsWithHighlightStatus[i];
                var word = wordWithHighlightStatus.Item1;
                var highlight = wordWithHighlightStatus.Item2;
                var size = Size;

                //Draw the current word.
                size = GetTextSize(g, word);
                DrawText(g,
                     word,
                     location,
                     Color.Black,
                     highlight ? Color.Orange : Color.Transparent);
                location.Offset(size.Width, 0);
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

        #endregion

        private void txtFileViewer_SelectionChanged(object sender, EventArgs e)
        {
            lblSelection.Text = txtFileViewer.SelectedText.Length.ToString();
        }
    }
}
