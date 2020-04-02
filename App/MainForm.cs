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

namespace _2ndbrainalpha
{
    public partial class MainForm : Form
    {
        bool _cancelled;
        SearchHelper _searchHelper;
        int _filesProcessed;
        const int LINE_NUMBER_LEN = 5;
        string _currentFile;
        delegate void DelegateMethod(params object[] args);
        IList<string> _targets;
        string _settingsFileName;

        public int LineNumberOffset => LINE_NUMBER_LEN + 2;

        public IList<string> TargetWords => txtTargets.Text.Split('\n','\r')?.Select(w => w.Trim()).Where(w => w.Length > 0).Distinct().ToList() ?? new List<string>();

        public MainForm()
        {
            InitializeComponent();
            txtFileViewer.ScrollBars = RichTextBoxScrollBars.None;
            _searchHelper = new SearchHelper(CheckForCancellation, OnFile, OnFileMatch, OnMatch, OnException);
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _settingsFileName = $@"{path}\settings.txt";
        }

        #region Event handlers
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text)) 
            {
                MessageBox.Show("Enter a search term");
                return;
            }

            _currentFile = null;
            _filesProcessed = 0;
            _cancelled = false;
            tvMatches.Nodes.Clear();
            txtFileViewer.Text = "";

            // Spin off thread to do the recon
            var tSearch = new Thread(new ParameterizedThreadStart(SearchThread));
            tSearch.Start();
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
            var match = e.Node.Tag as SearchLib.Match;
            string file;
            bool isFileNode = match == null;

            if (match == null)
            {
                file = e.Node.Tag as string;
            }
            else
            {
                file = match.File;
            }

            int position = 0;
            // Load file if not already loaded
            if (file != _currentFile)
            {
                _currentFile = file;
                var lines = File.ReadAllLines(file);
                var sb = new StringBuilder();
                var charsPerLine = CalculateMaxCharsPerLine(txtFileViewer);
                Debug.WriteLine($"charsPerLine = {charsPerLine}");

                for (int n = 0; n < lines.Length; n++)
                {
                    var line = lines[n];
                    sb.Append($"{line}{Environment.NewLine}");
                    if (match != null && n < match.LineNumber)
                    {
                        position += line.Length + 1;
                    }
                }

                txtFileViewer.Text = sb.ToString();
            }
            else
            {
                var lines = txtFileViewer.Text.Split(Environment.NewLine.ToCharArray());
                for (int n = 0; n < lines.Length; n++)
                {
                    var line = lines[n];
                    if (match != null && n < match.LineNumber)
                    {
                        position += line.Length + 1 /* for newline */;
                    }
                }
            }

            if (match != null)
            {
                position += match.StartIndex;
                txtFileViewer.Select(position, match.Word.Length);
            }
            else
            {
                txtFileViewer.Select(0, 0);
            }
            SetLineAndColumn();
            txtFileViewer.SelectionBackColor = Color.Orange;
            txtFileViewer.ScrollToCaret();
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

        #endregion

        #region Private methods
        private void LoadSettings()
        {
            if (!File.Exists(_settingsFileName))
                return;

            var settingsText = File.ReadAllText(_settingsFileName);
            if (settingsText != null) {
                var settings = JsonConvert.DeserializeObject<Settings>(settingsText);
                if (settings != null) {
                    txtPath.Text = settings.Path;
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

        private int CalculateMaxCharsPerLine(RichTextBox tb)
        {
            Graphics g = tb.CreateGraphics();
            float twoCharW = g.MeasureString("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", tb.Font).Width;
            float oneCharW = g.MeasureString("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", tb.Font).Width;
            return (int)((float)(tb.ClientRectangle.Width - tb.Margin.Horizontal - 15) / (twoCharW - oneCharW));
        }

        // Display status message in the toolstrip area
        private void SetStatusTxt(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new Action<string>((x) =>
                    {
                        lblStatusText.Text = x;
                    }),
                    new object[] { msg });
            }
            else
            {
                lblStatusText.Text = msg;
            }
        }

        private void AppendTextBoxText(TextBox tb, string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new Action<string>((x) =>
                    {
                        tb.Text += $"{x}{Environment.NewLine}";
                    }),
                    new object[] { text });
            }
            else
            {
                tb.Text += $"{text}{Environment.NewLine}";
            }
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
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<int>(x => { progressBarFiles.Value = x; lblFileCount.Text = x.ToString(); } ), new object[] { value});
            }
            else
            {
                progressBarFiles.Value = value;
            }
        }

        private void SetProgressBarMaximum(int max)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<int>(x => { progressBarFiles.Maximum = x; lblMaxFileCount.Text = x.ToString(); }), new object[] { max });
            }
            else
            {
                progressBarFiles.Maximum = max;
            }
        }

        private void AddFileToResults(string file)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(x => {
                    var fileNode = tvMatches.Nodes.Add($"{x}:");
                    fileNode.Name = file;
                    fileNode.Tag = file;
                }), 
                new object[] { file });
            }
            else
            {
                var fileNode = tvMatches.Nodes.Add($"{file}:");
                fileNode.Name = file;
                fileNode.Tag = file;
            }
        }

        private void SearchThread(object arg)
        {
            try
            {
                var targetWords = TargetWords;
                var searchPattern = txtSearch.Text;
                if (!targetWords.Contains(searchPattern)) 
                {
                    targetWords.Add(searchPattern);
                    AppendTextBoxText(txtTargets, searchPattern);
                }

                if (!string.IsNullOrEmpty(txtPath.Text))
                {
                    string[] files = Directory.GetFiles(txtPath.Text, "*.txt", SearchOption.AllDirectories);
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

        private void OnFileMatch(string file)
        {
            AddFileToResults(file);
        }

        private void OnMatch(SearchLib.Match match)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<SearchLib.Match>(x => {
                    var node = new TreeNode($"{x.Line} : ({1 + x.LineNumber},{1 + x.StartIndex})");
                    node.Tag = match;
                    var fileNode = tvMatches.Nodes.Find(match.File, false)[0];
                    fileNode.Nodes.Add(node);
                    tvMatches.ExpandAll();
                }),
                new object[] { match });
            }
            else
            {
                var node = new TreeNode($"{match.Line} : ({1 + match.LineNumber},{1 + match.StartIndex})");
                node.Tag = match;
                var fileNode = tvMatches.Nodes.Find(match.File, false)[0];
                fileNode.Nodes.Add(node);
                tvMatches.ExpandAll();
            }
        }

        private void OnException(string file, Exception ex) {
            SetStatusTxt($"ProcessFile: file = {file}, msg = {ex.Message}");
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

            UpdateLineNumbers();
        }
        #endregion

        private void txtFileViewer_VScroll(object sender, EventArgs e)
        {

        }
    }
}
