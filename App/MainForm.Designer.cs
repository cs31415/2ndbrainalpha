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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSelectPath = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.txtPath = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtFilter = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
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
            this.txtFileViewer = new _2ndbrainalpha.SyncTextBox();
            this.txtLineNumbers = new _2ndbrainalpha.SyncTextBox();
            this.statusStripResults = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblLineNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblColumnNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSelection = new System.Windows.Forms.ToolStripStatusLabel();
            this.cbExpandAll = new System.Windows.Forms.CheckBox();
            this.tvMatches = new _2ndbrainalpha.BufferedTreeView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddSynonyms = new System.Windows.Forms.Button();
            this.txtTargets = new System.Windows.Forms.TextBox();
            this.splitContainerMaster = new System.Windows.Forms.SplitContainer();
            this.splitContainerSideBar = new System.Windows.Forms.SplitContainer();
            this.lblTargetCount = new System.Windows.Forms.Label();
            this.txtThesaurusLookup = new System.Windows.Forms.TextBox();
            this.cbTargetsToggle = new System.Windows.Forms.CheckBox();
            this.lbTargets = new System.Windows.Forms.CheckedListBox();
            this.ctxMenuFileNode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddAntonyms = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSideBar)).BeginInit();
            this.splitContainerSideBar.Panel1.SuspendLayout();
            this.splitContainerSideBar.Panel2.SuspendLayout();
            this.splitContainerSideBar.SuspendLayout();
            this.ctxMenuFileNode.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSelectPath,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.txtPath,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.txtFilter,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(1189, 25);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(34, 22);
            this.toolStripLabel2.Text = "Path:";
            // 
            // txtPath
            // 
            this.txtPath.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(202, 25);
            this.txtPath.Text = "W:\\Writings";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel1.Text = "Filter:";
            // 
            // txtFilter
            // 
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(52, 25);
            this.txtFilter.Text = "*.txt";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // statusStripBottom
            // 
            this.statusStripBottom.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStripBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBarFiles,
            this.lblStatusText,
            this.toolStripStatusLabel3,
            this.lblFileCount,
            this.toolStripStatusLabel1,
            this.lblMaxFileCount,
            this.toolStripStatusLabel2});
            this.statusStripBottom.Location = new System.Drawing.Point(0, 799);
            this.statusStripBottom.Name = "statusStripBottom";
            this.statusStripBottom.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this.statusStripBottom.Size = new System.Drawing.Size(1189, 24);
            this.statusStripBottom.TabIndex = 3;
            this.statusStripBottom.Text = "statusStrip1";
            // 
            // progressBarFiles
            // 
            this.progressBarFiles.Name = "progressBarFiles";
            this.progressBarFiles.Size = new System.Drawing.Size(150, 18);
            // 
            // lblStatusText
            // 
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Size = new System.Drawing.Size(0, 19);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(60, 19);
            this.toolStripStatusLabel3.Text = "Processed";
            // 
            // lblFileCount
            // 
            this.lblFileCount.Name = "lblFileCount";
            this.lblFileCount.Size = new System.Drawing.Size(13, 19);
            this.lblFileCount.Text = "0";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(12, 19);
            this.toolStripStatusLabel1.Text = "/";
            // 
            // lblMaxFileCount
            // 
            this.lblMaxFileCount.Name = "lblMaxFileCount";
            this.lblMaxFileCount.Size = new System.Drawing.Size(13, 19);
            this.lblMaxFileCount.Text = "0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(28, 19);
            this.toolStripStatusLabel2.Text = "files";
            // 
            // splitContainerResults
            // 
            this.splitContainerResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerResults.Location = new System.Drawing.Point(0, 0);
            this.splitContainerResults.Margin = new System.Windows.Forms.Padding(2);
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
            this.splitContainerResults.Panel2.Controls.Add(this.cbExpandAll);
            this.splitContainerResults.Panel2.Controls.Add(this.tvMatches);
            this.splitContainerResults.Size = new System.Drawing.Size(941, 774);
            this.splitContainerResults.SplitterDistance = 392;
            this.splitContainerResults.SplitterWidth = 2;
            this.splitContainerResults.TabIndex = 5;
            // 
            // txtFileViewer
            // 
            this.txtFileViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFileViewer.Buddy = this.txtLineNumbers;
            this.txtFileViewer.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileViewer.Location = new System.Drawing.Point(38, 0);
            this.txtFileViewer.Margin = new System.Windows.Forms.Padding(2);
            this.txtFileViewer.Name = "txtFileViewer";
            this.txtFileViewer.ReadOnly = true;
            this.txtFileViewer.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtFileViewer.Size = new System.Drawing.Size(897, 372);
            this.txtFileViewer.TabIndex = 2;
            this.txtFileViewer.Text = "";
            this.txtFileViewer.SelectionChanged += new System.EventHandler(this.txtFileViewer_SelectionChanged);
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
            this.txtLineNumbers.Location = new System.Drawing.Point(2, 0);
            this.txtLineNumbers.Margin = new System.Windows.Forms.Padding(2);
            this.txtLineNumbers.Name = "txtLineNumbers";
            this.txtLineNumbers.ReadOnly = true;
            this.txtLineNumbers.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtLineNumbers.Size = new System.Drawing.Size(52, 372);
            this.txtLineNumbers.TabIndex = 4;
            this.txtLineNumbers.Text = "";
            // 
            // statusStripResults
            // 
            this.statusStripResults.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStripResults.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel4,
            this.lblLineNumber,
            this.toolStripStatusLabel5,
            this.lblColumnNumber,
            this.toolStripStatusLabel6,
            this.lblPosition,
            this.toolStripStatusLabel7,
            this.lblSelection});
            this.statusStripResults.Location = new System.Drawing.Point(0, 370);
            this.statusStripResults.Name = "statusStripResults";
            this.statusStripResults.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this.statusStripResults.Size = new System.Drawing.Size(941, 22);
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
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(53, 17);
            this.toolStripStatusLabel6.Text = "Position:";
            // 
            // lblPosition
            // 
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(58, 17);
            this.toolStripStatusLabel7.Text = "Selection:";
            // 
            // lblSelection
            // 
            this.lblSelection.Name = "lblSelection";
            this.lblSelection.Size = new System.Drawing.Size(0, 17);
            // 
            // cbExpandAll
            // 
            this.cbExpandAll.AutoSize = true;
            this.cbExpandAll.Location = new System.Drawing.Point(2, 2);
            this.cbExpandAll.Margin = new System.Windows.Forms.Padding(2);
            this.cbExpandAll.Name = "cbExpandAll";
            this.cbExpandAll.Size = new System.Drawing.Size(125, 17);
            this.cbExpandAll.TabIndex = 6;
            this.cbExpandAll.Text = "Expand/collapse all";
            this.cbExpandAll.UseVisualStyleBackColor = true;
            this.cbExpandAll.CheckedChanged += new System.EventHandler(this.cbExpandAll_CheckedChanged);
            // 
            // tvMatches
            // 
            this.tvMatches.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvMatches.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvMatches.FullRowSelect = true;
            this.tvMatches.HideSelection = false;
            this.tvMatches.LineColor = System.Drawing.Color.DimGray;
            this.tvMatches.Location = new System.Drawing.Point(0, 23);
            this.tvMatches.Margin = new System.Windows.Forms.Padding(2);
            this.tvMatches.Name = "tvMatches";
            this.tvMatches.Size = new System.Drawing.Size(935, 360);
            this.tvMatches.TabIndex = 4;
            this.tvMatches.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.tvMatches_DrawNode);
            this.tvMatches.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvMatches_NodeMouseClick);
            this.tvMatches.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvMatches_KeyDown);
            this.tvMatches.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tvMatches_KeyUp);
            this.tvMatches.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvMatches_MouseDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(417, 0);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(138, 25);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(559, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(138, 25);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Search Terms:";
            // 
            // btnAddSynonyms
            // 
            this.btnAddSynonyms.Location = new System.Drawing.Point(147, 17);
            this.btnAddSynonyms.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddSynonyms.Name = "btnAddSynonyms";
            this.btnAddSynonyms.Size = new System.Drawing.Size(107, 24);
            this.btnAddSynonyms.TabIndex = 14;
            this.btnAddSynonyms.Text = "Add synonyms";
            this.btnAddSynonyms.UseVisualStyleBackColor = true;
            this.btnAddSynonyms.Click += new System.EventHandler(this.btnAddSynonyms_Click);
            // 
            // txtTargets
            // 
            this.txtTargets.AcceptsReturn = true;
            this.txtTargets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargets.Location = new System.Drawing.Point(0, 64);
            this.txtTargets.Margin = new System.Windows.Forms.Padding(2);
            this.txtTargets.Multiline = true;
            this.txtTargets.Name = "txtTargets";
            this.txtTargets.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTargets.Size = new System.Drawing.Size(255, 328);
            this.txtTargets.TabIndex = 15;
            this.txtTargets.TextChanged += new System.EventHandler(this.txtTargets_TextChanged);
            // 
            // splitContainerMaster
            // 
            this.splitContainerMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMaster.Location = new System.Drawing.Point(0, 25);
            this.splitContainerMaster.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainerMaster.Name = "splitContainerMaster";
            // 
            // splitContainerMaster.Panel1
            // 
            this.splitContainerMaster.Panel1.Controls.Add(this.splitContainerSideBar);
            // 
            // splitContainerMaster.Panel2
            // 
            this.splitContainerMaster.Panel2.Controls.Add(this.splitContainerResults);
            this.splitContainerMaster.Size = new System.Drawing.Size(1189, 774);
            this.splitContainerMaster.SplitterDistance = 254;
            this.splitContainerMaster.SplitterWidth = 2;
            this.splitContainerMaster.TabIndex = 16;
            // 
            // splitContainerSideBar
            // 
            this.splitContainerSideBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerSideBar.Location = new System.Drawing.Point(0, 0);
            this.splitContainerSideBar.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainerSideBar.Name = "splitContainerSideBar";
            this.splitContainerSideBar.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerSideBar.Panel1
            // 
            this.splitContainerSideBar.Panel1.AutoScroll = true;
            this.splitContainerSideBar.Panel1.Controls.Add(this.btnAddAntonyms);
            this.splitContainerSideBar.Panel1.Controls.Add(this.btnAddSynonyms);
            this.splitContainerSideBar.Panel1.Controls.Add(this.lblTargetCount);
            this.splitContainerSideBar.Panel1.Controls.Add(this.txtThesaurusLookup);
            this.splitContainerSideBar.Panel1.Controls.Add(this.txtTargets);
            this.splitContainerSideBar.Panel1.Controls.Add(this.label1);
            // 
            // splitContainerSideBar.Panel2
            // 
            this.splitContainerSideBar.Panel2.AutoScroll = true;
            this.splitContainerSideBar.Panel2.Controls.Add(this.cbTargetsToggle);
            this.splitContainerSideBar.Panel2.Controls.Add(this.lbTargets);
            this.splitContainerSideBar.Size = new System.Drawing.Size(254, 774);
            this.splitContainerSideBar.SplitterDistance = 392;
            this.splitContainerSideBar.SplitterWidth = 2;
            this.splitContainerSideBar.TabIndex = 0;
            // 
            // lblTargetCount
            // 
            this.lblTargetCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTargetCount.AutoSize = true;
            this.lblTargetCount.Location = new System.Drawing.Point(1, 49);
            this.lblTargetCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTargetCount.Name = "lblTargetCount";
            this.lblTargetCount.Size = new System.Drawing.Size(49, 13);
            this.lblTargetCount.TabIndex = 16;
            this.lblTargetCount.Text = "0 item(s)";
            // 
            // txtThesaurusLookup
            // 
            this.txtThesaurusLookup.Location = new System.Drawing.Point(1, 18);
            this.txtThesaurusLookup.Margin = new System.Windows.Forms.Padding(2);
            this.txtThesaurusLookup.Name = "txtThesaurusLookup";
            this.txtThesaurusLookup.Size = new System.Drawing.Size(144, 22);
            this.txtThesaurusLookup.TabIndex = 17;
            // 
            // cbTargetsToggle
            // 
            this.cbTargetsToggle.AutoSize = true;
            this.cbTargetsToggle.Checked = true;
            this.cbTargetsToggle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTargetsToggle.Location = new System.Drawing.Point(2, 1);
            this.cbTargetsToggle.Margin = new System.Windows.Forms.Padding(2);
            this.cbTargetsToggle.Name = "cbTargetsToggle";
            this.cbTargetsToggle.Size = new System.Drawing.Size(121, 17);
            this.cbTargetsToggle.TabIndex = 1;
            this.cbTargetsToggle.Text = "Check/Uncheck All";
            this.cbTargetsToggle.UseVisualStyleBackColor = true;
            this.cbTargetsToggle.CheckedChanged += new System.EventHandler(this.cbTargetsToggle_CheckedChanged);
            // 
            // lbTargets
            // 
            this.lbTargets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTargets.CheckOnClick = true;
            this.lbTargets.FormattingEnabled = true;
            this.lbTargets.IntegralHeight = false;
            this.lbTargets.Location = new System.Drawing.Point(0, 21);
            this.lbTargets.Margin = new System.Windows.Forms.Padding(2);
            this.lbTargets.Name = "lbTargets";
            this.lbTargets.Size = new System.Drawing.Size(254, 363);
            this.lbTargets.Sorted = true;
            this.lbTargets.TabIndex = 0;
            this.lbTargets.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lbTargets_ItemCheck);
            // 
            // ctxMenuFileNode
            // 
            this.ctxMenuFileNode.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ctxMenuFileNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopy});
            this.ctxMenuFileNode.Name = "ctxMenuFileNode";
            this.ctxMenuFileNode.Size = new System.Drawing.Size(103, 26);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.Size = new System.Drawing.Size(102, 22);
            this.mnuCopy.Text = "Copy";
            this.mnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
            // 
            // btnAddAntonyms
            // 
            this.btnAddAntonyms.Location = new System.Drawing.Point(147, 40);
            this.btnAddAntonyms.Name = "btnAddAntonyms";
            this.btnAddAntonyms.Size = new System.Drawing.Size(107, 24);
            this.btnAddAntonyms.TabIndex = 18;
            this.btnAddAntonyms.Text = "Add antonyms";
            this.btnAddAntonyms.UseVisualStyleBackColor = true;
            this.btnAddAntonyms.Click += new System.EventHandler(this.btnAddAntonyms_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1189, 823);
            this.Controls.Add(this.splitContainerMaster);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.statusStripBottom);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "2nd Brain";
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
            this.splitContainerResults.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerResults)).EndInit();
            this.splitContainerResults.ResumeLayout(false);
            this.statusStripResults.ResumeLayout(false);
            this.statusStripResults.PerformLayout();
            this.splitContainerMaster.Panel1.ResumeLayout(false);
            this.splitContainerMaster.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMaster)).EndInit();
            this.splitContainerMaster.ResumeLayout(false);
            this.splitContainerSideBar.Panel1.ResumeLayout(false);
            this.splitContainerSideBar.Panel1.PerformLayout();
            this.splitContainerSideBar.Panel2.ResumeLayout(false);
            this.splitContainerSideBar.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSideBar)).EndInit();
            this.splitContainerSideBar.ResumeLayout(false);
            this.ctxMenuFileNode.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox txtPath;
        private System.Windows.Forms.ToolStripButton btnSelectPath;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderBrowser;
        public SyncTextBox txtFileViewer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Label lblTargetCount;
        private System.Windows.Forms.ContextMenuStrip ctxMenuFileNode;
        private System.Windows.Forms.ToolStripMenuItem mnuCopy;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel lblPosition;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel lblSelection;
        private System.Windows.Forms.SplitContainer splitContainerSideBar;
        private System.Windows.Forms.CheckedListBox lbTargets;
        private System.Windows.Forms.CheckBox cbTargetsToggle;
        private System.Windows.Forms.CheckBox cbExpandAll;
        private System.Windows.Forms.TextBox txtThesaurusLookup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button btnAddAntonyms;
    }
}

