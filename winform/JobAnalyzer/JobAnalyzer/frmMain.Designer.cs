using DocumentFormat.OpenXml.CustomProperties;
using JobAnalyzer.BLL;
using MahApps.Metro.IconPacks;

namespace JobAnalyzer
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            toolStrip1 = new ToolStrip();
            btnOpen = new ToolStripButton();
            btnProcess = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnCoverLetter = new ToolStripButton();
            btnRefresh = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            bntCreateFolder = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            btnDelete = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            btnOpenInBrowser = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            btnProcessAll = new ToolStripButton();
            toolStripSeparator6 = new ToolStripSeparator();
            btnQuery = new ToolStripButton();
            toolStripSeparator7 = new ToolStripSeparator();
            btnExport = new ToolStripButton();
            btnShowJob = new ToolStripButton();
            btnLoadJob = new ToolStripButton();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            mnuProcess = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            txtStatus = new ToolStripStatusLabel();
            progress = new ToolStripProgressBar();
            splitContainer1 = new SplitContainer();
            lsvData = new ListView();
            splitter2 = new Splitter();
            lstJobPosts = new ListBox();
            lstJobPostMenu = new ContextMenuStrip(components);
            mnuDelete = new ToolStripMenuItem();
            txtSearch = new TextBox();
            textBox2 = new TextBox();
            splitContainer2 = new SplitContainer();
            wv2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            rchJson = new RichTextBox();
            splitter1 = new Splitter();
            wv1 = new Microsoft.Web.WebView2.WinForms.WebView2();
            jobObjectBindingSource = new BindingSource(components);
            bgw = new System.ComponentModel.BackgroundWorker();
            btnHtmlCleanUp = new ToolStripButton();
            toolStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            lstJobPostMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)wv2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)wv1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)jobObjectBindingSource).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnOpen, btnProcess, toolStripSeparator1, btnCoverLetter, btnRefresh, toolStripSeparator2, bntCreateFolder, toolStripSeparator3, btnDelete, toolStripSeparator4, btnOpenInBrowser, toolStripSeparator5, btnProcessAll, toolStripSeparator6, btnQuery, toolStripSeparator7, btnExport, btnShowJob, btnLoadJob, btnHtmlCleanUp });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1178, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnOpen
            // 
            btnOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnOpen.Image = (Image)resources.GetObject("btnOpen.Image");
            btnOpen.ImageScaling = ToolStripItemImageScaling.None;
            btnOpen.ImageTransparentColor = Color.Magenta;
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(23, 22);
            btnOpen.Text = "Select Folder";
            btnOpen.Click += btnOpen_Click;
            // 
            // btnProcess
            // 
            btnProcess.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnProcess.Image = (Image)resources.GetObject("btnProcess.Image");
            btnProcess.ImageTransparentColor = Color.Magenta;
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(23, 22);
            btnProcess.Text = "Process";
            btnProcess.Click += btnProcess_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // btnCoverLetter
            // 
            btnCoverLetter.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnCoverLetter.Image = (Image)resources.GetObject("btnCoverLetter.Image");
            btnCoverLetter.ImageScaling = ToolStripItemImageScaling.None;
            btnCoverLetter.ImageTransparentColor = Color.Magenta;
            btnCoverLetter.Name = "btnCoverLetter";
            btnCoverLetter.Size = new Size(23, 22);
            btnCoverLetter.Text = "toolStripButton2";
            btnCoverLetter.Click += toolStripButton2_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnRefresh.Image = (Image)resources.GetObject("btnRefresh.Image");
            btnRefresh.ImageTransparentColor = Color.Magenta;
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(23, 22);
            btnRefresh.Text = "toolStripButton1";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // bntCreateFolder
            // 
            bntCreateFolder.DisplayStyle = ToolStripItemDisplayStyle.Image;
            bntCreateFolder.Image = (Image)resources.GetObject("bntCreateFolder.Image");
            bntCreateFolder.ImageTransparentColor = Color.Magenta;
            bntCreateFolder.Name = "bntCreateFolder";
            bntCreateFolder.Size = new Size(23, 22);
            bntCreateFolder.Text = "toolStripButton1";
            bntCreateFolder.Click += bntCreateFolder_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
            // 
            // btnDelete
            // 
            btnDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnDelete.Image = (Image)resources.GetObject("btnDelete.Image");
            btnDelete.ImageTransparentColor = Color.Magenta;
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(23, 22);
            btnDelete.Text = "toolStripButton1";
            btnDelete.Click += btnDelete_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 25);
            // 
            // btnOpenInBrowser
            // 
            btnOpenInBrowser.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnOpenInBrowser.Image = (Image)resources.GetObject("btnOpenInBrowser.Image");
            btnOpenInBrowser.ImageTransparentColor = Color.Magenta;
            btnOpenInBrowser.Name = "btnOpenInBrowser";
            btnOpenInBrowser.Size = new Size(23, 22);
            btnOpenInBrowser.Text = "Open in Chrome";
            btnOpenInBrowser.Click += btnOpenInBrowser_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 25);
            // 
            // btnProcessAll
            // 
            btnProcessAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnProcessAll.Image = (Image)resources.GetObject("btnProcessAll.Image");
            btnProcessAll.ImageTransparentColor = Color.Magenta;
            btnProcessAll.Name = "btnProcessAll";
            btnProcessAll.Size = new Size(23, 22);
            btnProcessAll.Text = "Process All";
            btnProcessAll.Click += btnProcessAll_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(6, 25);
            // 
            // btnQuery
            // 
            btnQuery.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnQuery.Image = (Image)resources.GetObject("btnQuery.Image");
            btnQuery.ImageTransparentColor = Color.Magenta;
            btnQuery.Name = "btnQuery";
            btnQuery.Size = new Size(23, 22);
            btnQuery.Text = "Check Database";
            btnQuery.Click += btnQuery_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(6, 25);
            // 
            // btnExport
            // 
            btnExport.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnExport.Image = (Image)resources.GetObject("btnExport.Image");
            btnExport.ImageTransparentColor = Color.Magenta;
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(23, 22);
            btnExport.Text = "Export data file";
            btnExport.Click += btnExport_Click;
            // 
            // btnShowJob
            // 
            btnShowJob.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnShowJob.Image = (Image)resources.GetObject("btnShowJob.Image");
            btnShowJob.ImageTransparentColor = Color.Magenta;
            btnShowJob.Name = "btnShowJob";
            btnShowJob.Size = new Size(23, 22);
            btnShowJob.Text = "Job Detail";
            btnShowJob.Click += btnShowJob_Click;
            // 
            // btnLoadJob
            // 
            btnLoadJob.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnLoadJob.Image = (Image)resources.GetObject("btnLoadJob.Image");
            btnLoadJob.ImageTransparentColor = Color.Magenta;
            btnLoadJob.Name = "btnLoadJob";
            btnLoadJob.Size = new Size(23, 22);
            btnLoadJob.Text = "toolStripButton1";
            btnLoadJob.Click += btnLoadJob_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 25);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(3, 1, 0, 1);
            menuStrip1.Size = new Size(1178, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, mnuProcess });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 22);
            fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(139, 22);
            openToolStripMenuItem.Text = "&Open Folder";
            // 
            // mnuProcess
            // 
            mnuProcess.Name = "mnuProcess";
            mnuProcess.ShortcutKeys = Keys.F5;
            mnuProcess.Size = new Size(139, 22);
            mnuProcess.Text = "&Process";
            mnuProcess.Click += mnuProcess_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { txtStatus, progress });
            statusStrip1.Location = new Point(0, 1025);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1178, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // txtStatus
            // 
            txtStatus.Name = "txtStatus";
            txtStatus.Size = new Size(1061, 17);
            txtStatus.Spring = true;
            // 
            // progress
            // 
            progress.Name = "progress";
            progress.Size = new Size(100, 16);
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 25);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lsvData);
            splitContainer1.Panel1.Controls.Add(splitter2);
            splitContainer1.Panel1.Controls.Add(lstJobPosts);
            splitContainer1.Panel1.Controls.Add(txtSearch);
            splitContainer1.Panel1.Controls.Add(textBox2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Panel2.Controls.Add(rchJson);
            splitContainer1.Panel2.Controls.Add(splitter1);
            splitContainer1.Panel2.Controls.Add(wv1);
            splitContainer1.Size = new Size(1178, 1000);
            splitContainer1.SplitterDistance = 387;
            splitContainer1.TabIndex = 3;
            // 
            // lsvData
            // 
            lsvData.Dock = DockStyle.Fill;
            lsvData.Location = new Point(0, 428);
            lsvData.Name = "lsvData";
            lsvData.Size = new Size(387, 572);
            lsvData.TabIndex = 3;
            lsvData.UseCompatibleStateImageBehavior = false;
            lsvData.SelectedIndexChanged += lsvData_SelectedIndexChanged;
            // 
            // splitter2
            // 
            splitter2.Dock = DockStyle.Top;
            splitter2.Location = new Point(0, 425);
            splitter2.Name = "splitter2";
            splitter2.Size = new Size(387, 3);
            splitter2.TabIndex = 2;
            splitter2.TabStop = false;
            // 
            // lstJobPosts
            // 
            lstJobPosts.ContextMenuStrip = lstJobPostMenu;
            lstJobPosts.Dock = DockStyle.Top;
            lstJobPosts.FormattingEnabled = true;
            lstJobPosts.ItemHeight = 15;
            lstJobPosts.Location = new Point(0, 46);
            lstJobPosts.Name = "lstJobPosts";
            lstJobPosts.Size = new Size(387, 379);
            lstJobPosts.TabIndex = 0;
            lstJobPosts.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            lstJobPosts.DoubleClick += lstJobPosts_DoubleClick;
            // 
            // lstJobPostMenu
            // 
            lstJobPostMenu.Items.AddRange(new ToolStripItem[] { mnuDelete });
            lstJobPostMenu.Name = "lstJobPostMenu";
            lstJobPostMenu.Size = new Size(132, 26);
            // 
            // mnuDelete
            // 
            mnuDelete.Name = "mnuDelete";
            mnuDelete.ShortcutKeys = Keys.Delete;
            mnuDelete.Size = new Size(131, 22);
            mnuDelete.Text = "&Delete";
            mnuDelete.Click += mnuDelete_Click;
            // 
            // txtSearch
            // 
            txtSearch.Dock = DockStyle.Top;
            txtSearch.Location = new Point(0, 23);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(387, 23);
            txtSearch.TabIndex = 4;
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.KeyPress += txtSearch_KeyPress;
            // 
            // textBox2
            // 
            textBox2.Dock = DockStyle.Top;
            textBox2.Location = new Point(0, 0);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(387, 23);
            textBox2.TabIndex = 5;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 312);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(wv2);
            splitContainer2.Size = new Size(787, 688);
            splitContainer2.SplitterDistance = 545;
            splitContainer2.TabIndex = 2;
            // 
            // wv2
            // 
            wv2.AllowExternalDrop = true;
            wv2.CreationProperties = null;
            wv2.DefaultBackgroundColor = Color.White;
            wv2.Dock = DockStyle.Fill;
            wv2.Location = new Point(0, 0);
            wv2.Name = "wv2";
            wv2.Size = new Size(545, 688);
            wv2.TabIndex = 1;
            wv2.ZoomFactor = 1D;
            // 
            // rchJson
            // 
            rchJson.Location = new Point(85, 245);
            rchJson.Name = "rchJson";
            rchJson.Size = new Size(121, 112);
            rchJson.TabIndex = 0;
            rchJson.Text = "";
            rchJson.Visible = false;
            // 
            // splitter1
            // 
            splitter1.Dock = DockStyle.Top;
            splitter1.Location = new Point(0, 309);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(787, 3);
            splitter1.TabIndex = 1;
            splitter1.TabStop = false;
            // 
            // wv1
            // 
            wv1.AllowExternalDrop = true;
            wv1.CreationProperties = null;
            wv1.DefaultBackgroundColor = Color.White;
            wv1.Dock = DockStyle.Top;
            wv1.Location = new Point(0, 0);
            wv1.Name = "wv1";
            wv1.Size = new Size(787, 309);
            wv1.TabIndex = 1;
            wv1.ZoomFactor = 1D;
            // 
            // jobObjectBindingSource
            // 
            jobObjectBindingSource.DataSource = typeof(JobObject);
            // 
            // bgw
            // 
            bgw.WorkerReportsProgress = true;
            bgw.DoWork += bgw_DoWork;
            bgw.ProgressChanged += bgw_ProgressChanged;
            // 
            // btnHtmlCleanUp
            // 
            btnHtmlCleanUp.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnHtmlCleanUp.Image = (Image)resources.GetObject("btnHtmlCleanUp.Image");
            btnHtmlCleanUp.ImageTransparentColor = Color.Magenta;
            btnHtmlCleanUp.Name = "btnHtmlCleanUp";
            btnHtmlCleanUp.Size = new Size(23, 22);
            btnHtmlCleanUp.Text = "toolStripButton1";
            btnHtmlCleanUp.ToolTipText = "HTML Clean up";
            btnHtmlCleanUp.Click += btnHtmlCleanUp_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1178, 1047);
            Controls.Add(menuStrip1);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2, 1, 2, 1);
            Name = "frmMain";
            Text = "Job Analyzer";
            WindowState = FormWindowState.Maximized;
            Load += frm1_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            lstJobPostMenu.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)wv2).EndInit();
            ((System.ComponentModel.ISupportInitialize)wv1).EndInit();
            ((System.ComponentModel.ISupportInitialize)jobObjectBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private MenuStrip menuStrip1;
        private ToolStripButton btnOpen;
        private StatusStrip statusStrip1;
        private SplitContainer splitContainer1;
        private ListBox lstJobPosts;
        private Splitter splitter1;
        private ToolStripStatusLabel txtStatus;
        private ToolStripButton btnCoverLetter;
        //private ComboBox cmbModels;
        private ToolStripButton btnProcess;
        private ToolStripSeparator toolStripSeparator1;
        private Microsoft.Web.WebView2.WinForms.WebView2 wv1;
        private RichTextBox rchJson;
        private ToolStripButton btnRefresh;
        private ToolStripButton bntCreateFolder;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton btnDelete;
        private ContextMenuStrip lstJobPostMenu;
        private ToolStripMenuItem mnuDelete;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton btnOpenInBrowser;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem mnuProcess;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton btnProcessAll;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripButton btnQuery;
        private ListView lsvData;
        private Splitter splitter2;
        private SplitContainer splitContainer2;
        private Microsoft.Web.WebView2.WinForms.WebView2 wv2;
        private BindingSource jobObjectBindingSource;
        private TextBox textBox7;
        private Label label10;
        private TextBox textBox6;
        private Label label9;
        private TextBox textBox5;
        private Label label8;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox2;
        private System.ComponentModel.BackgroundWorker bgw;
        private ToolStripProgressBar progress;
        private TextBox txtSearch;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripButton btnExport;
        private ToolStripButton btnShowJob;
        private ToolStripButton btnLoadJob;
        private ToolStripButton btnHtmlCleanUp;
    }
}
