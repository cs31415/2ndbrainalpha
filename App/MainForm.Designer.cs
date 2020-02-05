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
            this.btnSelectPath = new System.Windows.Forms.ToolStripButton();
            this.txtPath = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dlgFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.txtSynonyms = new System.Windows.Forms.TextBox();
            this.txtFileViewer = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBarFiles = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFileCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMaxFileCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tvMatches = new _2ndbrainalpha.BufferedTreeView();
            this.splitContainerResults = new System.Windows.Forms.SplitContainer();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblLineNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblColumnNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerResults)).BeginInit();
            this.splitContainerResults.Panel1.SuspendLayout();
            this.splitContainerResults.Panel2.SuspendLayout();
            this.splitContainerResults.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtPath,
            this.btnSelectPath,
            this.toolStripSeparator1,
            this.txtSearch,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1284, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSelectPath.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectPath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectPath.Image")));
            this.btnSelectPath.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(57, 22);
            this.btnSelectPath.Text = "Browse...";
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // txtPath
            // 
            this.txtPath.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(250, 25);
            this.txtPath.Text = "Z:\\Google Drive";
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
            // txtSynonyms
            // 
            this.txtSynonyms.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtSynonyms.Location = new System.Drawing.Point(0, 25);
            this.txtSynonyms.Multiline = true;
            this.txtSynonyms.Name = "txtSynonyms";
            this.txtSynonyms.ReadOnly = true;
            this.txtSynonyms.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSynonyms.Size = new System.Drawing.Size(270, 936);
            this.txtSynonyms.TabIndex = 1;
            // 
            // txtFileViewer
            // 
            this.txtFileViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileViewer.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileViewer.Location = new System.Drawing.Point(0, 0);
            this.txtFileViewer.Name = "txtFileViewer";
            this.txtFileViewer.ReadOnly = true;
            this.txtFileViewer.Size = new System.Drawing.Size(1011, 432);
            this.txtFileViewer.TabIndex = 2;
            this.txtFileViewer.Text = "";
            this.txtFileViewer.WordWrap = false;
            this.txtFileViewer.Click += new System.EventHandler(this.txtFileViewer_Click);
            this.txtFileViewer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFileViewer_KeyDown);
            this.txtFileViewer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFileViewer_KeyUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBarFiles,
            this.lblStatusText,
            this.toolStripStatusLabel3,
            this.lblFileCount,
            this.toolStripStatusLabel1,
            this.lblMaxFileCount,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(270, 939);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1014, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
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
            // tvMatches
            // 
            this.tvMatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMatches.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvMatches.Location = new System.Drawing.Point(0, 0);
            this.tvMatches.Name = "tvMatches";
            this.tvMatches.Size = new System.Drawing.Size(1014, 453);
            this.tvMatches.TabIndex = 4;
            this.tvMatches.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.tvMatches_DrawNode);
            this.tvMatches.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvMatches_NodeMouseClick);
            // 
            // splitContainerResults
            // 
            this.splitContainerResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerResults.Location = new System.Drawing.Point(270, 25);
            this.splitContainerResults.Name = "splitContainerResults";
            this.splitContainerResults.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerResults.Panel1
            // 
            this.splitContainerResults.Panel1.Controls.Add(this.statusStrip2);
            this.splitContainerResults.Panel1.Controls.Add(this.txtFileViewer);
            // 
            // splitContainerResults.Panel2
            // 
            this.splitContainerResults.Panel2.Controls.Add(this.tvMatches);
            this.splitContainerResults.Size = new System.Drawing.Size(1014, 914);
            this.splitContainerResults.SplitterDistance = 457;
            this.splitContainerResults.TabIndex = 5;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel4,
            this.lblLineNumber,
            this.toolStripStatusLabel5,
            this.lblColumnNumber});
            this.statusStrip2.Location = new System.Drawing.Point(0, 435);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(1014, 22);
            this.statusStrip2.TabIndex = 3;
            this.statusStrip2.Text = "statusStrip2";
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
            this.btnSearch.Location = new System.Drawing.Point(651, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(112, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(769, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1284, 961);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.splitContainerResults);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txtSynonyms);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.Text = "Synonym Search";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainerResults.Panel1.ResumeLayout(false);
            this.splitContainerResults.Panel1.PerformLayout();
            this.splitContainerResults.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerResults)).EndInit();
            this.splitContainerResults.ResumeLayout(false);
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox txtSearch;
        private System.Windows.Forms.ToolStripTextBox txtPath;
        private System.Windows.Forms.ToolStripButton btnSelectPath;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderBrowser;
        public System.Windows.Forms.TextBox txtSynonyms;
        public System.Windows.Forms.RichTextBox txtFileViewer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progressBarFiles;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusText;
        private System.Windows.Forms.ToolStripStatusLabel lblFileCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblMaxFileCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private _2ndbrainalpha.BufferedTreeView tvMatches;
        private System.Windows.Forms.SplitContainer splitContainerResults;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel lblLineNumber;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel lblColumnNumber;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnCancel;
    }
}

