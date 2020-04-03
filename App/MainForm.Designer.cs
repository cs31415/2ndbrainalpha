namespace _2ndbrainalpha
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.txtPath = new System.Windows.Forms.ToolStripTextBox();
            this.btnSelectPath = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dlgFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStripBottom = new System.Windows.Forms.StatusStrip();
            this.progressBarFiles = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFileCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMaxFileCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainerResults = new System.Windows.Forms.SplitContainer();
            this.statusStripResults = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblLineNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblColumnNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddSynonyms = new System.Windows.Forms.Button();
            this.txtTargets = new System.Windows.Forms.TextBox();
            this.splitContainerMaster = new System.Windows.Forms.SplitContainer();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.txtFilter = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.txtFileViewer = new _2ndbrainalpha.SyncTextBox();
            this.txtLineNumbers = new _2ndbrainalpha.SyncTextBox();
            this.tvMatches = new _2ndbrainalpha.BufferedTreeView();
            this.toolStrip1.SuspendLayout();
            this.statusStripBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerResults)).BeginInit();
            this.splitContainerResults.Panel1.SuspendLayout();
            this.splitContainerResults.Panel2.SuspendLayout();
            this.splitContainerResults.SuspendLayout();
            this.statusStripResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMaster)).BeginInit();
            this.splitContainerMaster.Panel1.SuspendLayout();
            this.splitContainerMaster.Panel2.SuspendLayout();
            this.splitContainerMaster.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.txtPath,
            this.btnSelectPath,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.txtFilter,
            this.toolStripSeparator3,
            this.toolStripLabel3,
            this.txtSearch,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1284, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // txtPath
            // 
            this.txtPath.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(350, 25);
            this.txtPath.Text = "W:\\Writings";
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSelectPath.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectPath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectPath.Image")));
            this.btnSelectPath.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(58, 22);
            this.btnSelectPath.Text = "Browse...";
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // statusStripBottom
            // 
            this.statusStripBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBarFiles,
            this.lblStatusText,
            this.toolStripStatusLabel3,
            this.lblFileCount,
            this.toolStripStatusLabel1,
            this.lblMaxFileCount,
            this.toolStripStatusLabel2});
            this.statusStripBottom.Location = new System.Drawing.Point(0, 939);
            this.statusStripBottom.Name = "statusStripBottom";
            this.statusStripBottom.Size = new System.Drawing.Size(1284, 22);
            this.statusStripBottom.TabIndex = 3;
            this.statusStripBottom.Text = "statusStrip1";
            // 
            // progressBarFiles
            // 
            this.progressBarFiles.Name = "progressBarFiles";
            this.progressBarFiles.Size = new System.Drawing.Size(100, 16);
            // 
            // lblStatusText
            // 
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(60, 17);
            this.toolStripStatusLabel3.Text = "Processed";
            // 
            // lblFileCount
            // 
            this.lblFileCount.Name = "lblFileCount";
            this.lblFileCount.Size = new System.Drawing.Size(13, 17);
            this.lblFileCount.Text = "0";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(12, 17);
            this.toolStripStatusLabel1.Text = "/";
            // 
            // lblMaxFileCount
            // 
            this.lblMaxFileCount.Name = "lblMaxFileCount";
            this.lblMaxFileCount.Size = new System.Drawing.Size(13, 17);
            this.lblMaxFileCount.Text = "0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(28, 17);
            this.toolStripStatusLabel2.Text = "files";
            // 
            // splitContainerResults
            // 
            this.splitContainerResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerResults.Location = new System.Drawing.Point(0, 0);
            this.splitContainerResults.Name = "splitContainerResults";
            this.splitContainerResults.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerResults.Panel1
            // 
            this.splitContainerResults.Panel1.AutoScroll = true;
            this.splitContainerResults.Panel1.Controls.Add(this.txtFileViewer);
            this.splitContainerResults.Panel1.Controls.Add(this.txtLineNumbers);
            this.splitContainerResults.Panel1.Controls.Add(this.statusStripResults);
            // 
            // splitContainerResults.Panel2
            // 
            this.splitContainerResults.Panel2.AutoScroll = true;
            this.splitContainerResults.Panel2.Controls.Add(this.tvMatches);
            this.splitContainerResults.Size = new System.Drawing.Size(1004, 914);
            this.splitContainerResults.SplitterDistance = 465;
            this.splitContainerResults.TabIndex = 5;
            // 
            // statusStripResults
            // 
            this.statusStripResults.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel4,
            this.lblLineNumber,
            this.toolStripStatusLabel5,
            this.lblColumnNumber});
            this.statusStripResults.Location = new System.Drawing.Point(0, 443);
            this.statusStripResults.Name = "statusStripResults";
            this.statusStripResults.Size = new System.Drawing.Size(1004, 22);
            this.statusStripResults.TabIndex = 3;
            this.statusStripResults.Text = "statusStrip2";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(35, 17);
            this.toolStripStatusLabel4.Text = "Line: ";
            // 
            // lblLineNumber
            // 
            this.lblLineNumber.Name = "lblLineNumber";
            this.lblLineNumber.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(31, 17);
            this.toolStripStatusLabel5.Text = "Col: ";
            // 
            // lblColumnNumber
            // 
            this.lblColumnNumber.Name = "lblColumnNumber";
            this.lblColumnNumber.Size = new System.Drawing.Size(0, 17);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(989, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(112, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(1107, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Target words:";
            // 
            // btnAddSynonyms
            // 
            this.btnAddSynonyms.Location = new System.Drawing.Point(16, 25);
            this.btnAddSynonyms.Name = "btnAddSynonyms";
            this.btnAddSynonyms.Size = new System.Drawing.Size(104, 23);
            this.btnAddSynonyms.TabIndex = 14;
            this.btnAddSynonyms.Text = "Add synonyms";
            this.btnAddSynonyms.UseVisualStyleBackColor = true;
            this.btnAddSynonyms.Click += new System.EventHandler(this.btnAddSynonyms_Click);
            // 
            // txtTargets
            // 
            this.txtTargets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargets.Location = new System.Drawing.Point(3, 54);
            this.txtTargets.Multiline = true;
            this.txtTargets.Name = "txtTargets";
            this.txtTargets.Size = new System.Drawing.Size(270, 860);
            this.txtTargets.TabIndex = 15;
            // 
            // splitContainerMaster
            // 
            this.splitContainerMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMaster.Location = new System.Drawing.Point(0, 25);
            this.splitContainerMaster.Name = "splitContainerMaster";
            // 
            // splitContainerMaster.Panel1
            // 
            this.splitContainerMaster.Panel1.AutoScroll = true;
            this.splitContainerMaster.Panel1.Controls.Add(this.label1);
            this.splitContainerMaster.Panel1.Controls.Add(this.txtTargets);
            this.splitContainerMaster.Panel1.Controls.Add(this.btnAddSynonyms);
            // 
            // splitContainerMaster.Panel2
            // 
            this.splitContainerMaster.Panel2.Controls.Add(this.splitContainerResults);
            this.splitContainerMaster.Size = new System.Drawing.Size(1284, 914);
            this.splitContainerMaster.SplitterDistance = 276;
            this.splitContainerMaster.TabIndex = 16;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel1.Text = "Filter:";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(34, 22);
            this.toolStripLabel2.Text = "Path:";
            // 
            // txtFilter
            // 
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(69, 22);
            this.toolStripLabel3.Text = "Search Text:";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // txtFileViewer
            // 
            this.txtFileViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFileViewer.Buddy = this.txtLineNumbers;
            this.txtFileViewer.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileViewer.Location = new System.Drawing.Point(69, 0);
            this.txtFileViewer.Name = "txtFileViewer";
            this.txtFileViewer.ReadOnly = true;
            this.txtFileViewer.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtFileViewer.Size = new System.Drawing.Size(932, 441);
            this.txtFileViewer.TabIndex = 2;
            this.txtFileViewer.Text = "";
            this.txtFileViewer.Click += new System.EventHandler(this.txtFileViewer_Click);
            this.txtFileViewer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFileViewer_KeyDown);
            this.txtFileViewer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFileViewer_KeyUp);
            // 
            // txtLineNumbers
            // 
            this.txtLineNumbers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLineNumbers.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtLineNumbers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLineNumbers.Buddy = null;
            this.txtLineNumbers.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLineNumbers.Location = new System.Drawing.Point(3, 0);
            this.txtLineNumbers.Name = "txtLineNumbers";
            this.txtLineNumbers.ReadOnly = true;
            this.txtLineNumbers.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtLineNumbers.Size = new System.Drawing.Size(85, 441);
            this.txtLineNumbers.TabIndex = 4;
            this.txtLineNumbers.Text = "";
            // 
            // tvMatches
            // 
            this.tvMatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMatches.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvMatches.FullRowSelect = true;
            this.tvMatches.HideSelection = false;
            this.tvMatches.LineColor = System.Drawing.Color.DimGray;
            this.tvMatches.Location = new System.Drawing.Point(0, 0);
            this.tvMatches.Name = "tvMatches";
            this.tvMatches.Size = new System.Drawing.Size(1004, 445);
            this.tvMatches.TabIndex = 4;
            this.tvMatches.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.tvMatches_DrawNode);
            this.tvMatches.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvMatches_NodeMouseClick);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1284, 961);
            this.Controls.Add(this.splitContainerMaster);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.statusStripBottom);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.Text = "Synonym Search";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStripBottom.ResumeLayout(false);
            this.statusStripBottom.PerformLayout();
            this.splitContainerResults.Panel1.ResumeLayout(false);
            this.splitContainerResults.Panel1.PerformLayout();
            this.splitContainerResults.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerResults)).EndInit();
            this.splitContainerResults.ResumeLayout(false);
            this.statusStripResults.ResumeLayout(false);
            this.statusStripResults.PerformLayout();
            this.splitContainerMaster.Panel1.ResumeLayout(false);
            this.splitContainerMaster.Panel1.PerformLayout();
            this.splitContainerMaster.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMaster)).EndInit();
            this.splitContainerMaster.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox txtSearch;
        private System.Windows.Forms.ToolStripTextBox txtPath;
        private System.Windows.Forms.ToolStripButton btnSelectPath;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderBrowser;
        public SyncTextBox txtFileViewer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.StatusStrip statusStripBottom;
        private System.Windows.Forms.ToolStripProgressBar progressBarFiles;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusText;
        private System.Windows.Forms.ToolStripStatusLabel lblFileCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblMaxFileCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private _2ndbrainalpha.BufferedTreeView tvMatches;
        private System.Windows.Forms.SplitContainer splitContainerResults;
        private System.Windows.Forms.StatusStrip statusStripResults;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel lblLineNumber;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel lblColumnNumber;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddSynonyms;
        private System.Windows.Forms.TextBox txtTargets;
        private System.Windows.Forms.SplitContainer splitContainerMaster;
        private SyncTextBox txtLineNumbers;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtFilter;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

