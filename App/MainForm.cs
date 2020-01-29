using SynonymsLib;
using System;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using Ganss.Text;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _2ndbrainalpha
{
    public partial class MainForm : Form
    {
        bool _cancelled;
        SearchHelper _searchHelper;

        public MainForm()
        {
            InitializeComponent();
            _searchHelper = new SearchHelper(CheckForCancellation, OnFile, OnMatch, OnException);
        }

        #region Event handlers
        private void btnSearch_Click(object sender, EventArgs e)
        {
            txtMatches.Text = "";
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

        private void SearchThread(object arg)
        {
            try
            {
                txtSynonyms.Text = string.Empty;

                var searchPattern = txtSearch.Text;
                // Look up synonym of search word
                var synonyms = Lookup.GetSynonyms(searchPattern);
                foreach (var synonym in synonyms)
                {
                    AppendTextBoxText(txtSynonyms, synonym.Word);
                }

                var trie = new AhoCorasick(synonyms.Select(s => s.Word));
                if (!string.IsNullOrEmpty(txtPath.Text))
                {
                    string[] files = Directory.GetFiles(txtPath.Text, "*.txt", SearchOption.TopDirectoryOnly);
                    var fileCount = files.Length;
                    SetProgressBarMaximum(fileCount);
                    int filesProcessed = 0;
                    foreach (var file in files)
                    {
                        SetProgressBarValue(++filesProcessed);
                        if (_cancelled)
                        {
                            SetStatusTxt("Search was canceled.");
                            return;
                        }
                        _searchHelper.ProcessFile(file, trie, synonyms);
                    }
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
            AppendTextBoxText(txtMatches, $"{file}:");
        }

        private void OnMatch(string line, string word, int lineNumber, int column)
        {
            AppendTextBoxText(txtMatches, $"{line}{Environment.NewLine}{word}({lineNumber},{column})");
        }

        private void OnException(string file, Exception ex) {
            SetStatusTxt($"ProcessFile: file = {file}, msg = {ex.Message}");
        }

        #endregion
    }
}
