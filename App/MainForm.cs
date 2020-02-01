using SynonymsLib;
using System;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using System.Drawing;
using SearchLib;
using System.Text.RegularExpressions;

namespace _2ndbrainalpha
{
    public partial class MainForm : Form
    {
        bool _cancelled;
        SearchHelper _searchHelper;
        TreeNode _currentFileNode;
        int _filesProcessed;

        public MainForm()
        {
            InitializeComponent();
            _searchHelper = new SearchHelper(CheckForCancellation, OnFile, OnFileMatch, OnMatch, OnException);
        }

        #region Event handlers
        private void btnSearch_Click(object sender, EventArgs e)
        {
            _filesProcessed = 0;
            tvMatches.Nodes.Clear();
            txtFileViewer.Text = "";
            txtSynonyms.Text = "";

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

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {

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
                var wordsToHighlight = txtSynonyms.Text.Split('\n', '\r').Select(w => w.Trim()).Where(w => w.Length > 0).Distinct().ToArray();

                DrawTextWithHighlightedWords(e.Node.Text,
                                             wordsToHighlight,
                                             e.Graphics,
                                             e.Bounds,
                                             (e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected);
            }
        }

        private void tvMatches_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            txtFileViewer.Text = string.Empty;
            var match = e.Node.Tag as SearchLib.Match;
            txtFileViewer.Text = File.ReadAllText(match.File).Replace("\n", Environment.NewLine);
        }

        #endregion

        #region Private methods
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
                    tvMatches.BeginUpdate();
                    _currentFileNode = tvMatches.Nodes.Add($"{x}:");
                    _currentFileNode.Tag = file;
                    tvMatches.EndUpdate();
                }), 
                new object[] { file });
            }
            else
            {
                tvMatches.BeginUpdate();
                _currentFileNode = tvMatches.Nodes.Add($"{file}:");
                _currentFileNode.Tag = file;
                tvMatches.EndUpdate();
            }
        }

        private void SearchThread(object arg)
        {
            try
            {
                txtSynonyms.Text = string.Empty;

                var searchPattern = txtSearch.Text;
                // Look up synonym of search word
                var synonyms = Lookup.GetSynonyms(searchPattern).Select(s => s.Word).Distinct();
                foreach (var synonym in synonyms)
                {
                    AppendTextBoxText(txtSynonyms, synonym);
                }

                if (!string.IsNullOrEmpty(txtPath.Text))
                {
                    string[] files = Directory.GetFiles(txtPath.Text, "*.txt", SearchOption.TopDirectoryOnly);
                    var fileCount = files.Length;
                    SetProgressBarMaximum(fileCount);

                    _searchHelper.SearchFiles(files, synonyms.ToList());    
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
                    tvMatches.BeginUpdate();
                    var node = new TreeNode(x.Line);
                    node.Tag = match;
                    _currentFileNode.Nodes.Add(node);
                    tvMatches.ExpandAll();
                    tvMatches.EndUpdate();
                }),
                new object[] { match });
            }
            else
            {
                tvMatches.BeginUpdate();
                var node = new TreeNode(match.Line);
                node.Tag = match;
                _currentFileNode.Nodes.Add(node);
                tvMatches.ExpandAll();
                tvMatches.EndUpdate();
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
            var words = Regex.Split(text, @"([,\s;\.\t(){}\[\]])").Where(w => w.Length > 0).ToArray();
            var wordsWithHighlightStatus = Array.ConvertAll(words,
                                                            (s) => Tuple.Create(s, wordsToHighlight.Contains(s)));

            for (int i = 0; i < wordsWithHighlightStatus.GetUpperBound(0); i++)
            {
                var wordWithHighlightStatus = wordsWithHighlightStatus[i];
                var word = wordWithHighlightStatus.Item1;
                var highlight = wordWithHighlightStatus.Item2;
                var size = Size;

                //Draw a space before all but the first word.
                /*if (i > 0)
                {
                    size = GetTextSize(g, " ");
                    DrawText(g,
                         " ",
                         location,
                         Color.Black,
                         Color.Transparent);
                    location.Offset(size.Width, 0);
                }*/

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

        #endregion
    }
}
