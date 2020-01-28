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

        public MainForm()
        {
            InitializeComponent();
        }

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
                        ProcessFile(file, trie, synonyms);
                        SetProgressBarValue(++filesProcessed);
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

        private void ProcessFile(string file, AhoCorasick trie, IList<Entry> synonyms)
        {
            try
            {
                if (_cancelled)
                {
                    SetStatusTxt("Search was canceled.");
                    return;
                }
                // find occurences of search word and synonyms in file
                var lines = File.ReadAllLines(file);
                bool writeFileHeader = true;
                var currentLineNumber = 0;
                foreach (var line in lines)
                {
                    currentLineNumber++;
                    if (_cancelled)
                    {
                        SetStatusTxt("Search was canceled.");
                        return;
                    }

                    var matches = trie
                        .Search(line)
                        .Where(m => {
                            var chars = line.ToCharArray();
                            var leftSpace = IsWhiteSpace(chars[Math.Max(m.Index - 1, 0)]);
                            var rightSpace = IsWhiteSpace(chars[Math.Min(m.Index + m.Word.Length, line.Length - 1)]);
                            return leftSpace && rightSpace;
                        });
                    if (writeFileHeader && matches.Count() > 0)
                    {
                        AppendTextBoxText(txtMatches, $"{file}:");
                        writeFileHeader = false;
                    }
                    matches
                        .ToList()
                        .ForEach(m => AppendTextBoxText(txtMatches, $"{line}{Environment.NewLine}{m.Word}({currentLineNumber},{m.Index})"));
                }
            }
            catch (Exception ex)
            {
                SetStatusTxt($"ProcessFile: file = {file}, msg = {ex.Message}");
            }
        }

        private bool IsWhiteSpace(char c)
        {
            return string.IsNullOrWhiteSpace(c.ToString()) || Regex.IsMatch(c.ToString(), "[.,;\"{}]");
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
    }
}
