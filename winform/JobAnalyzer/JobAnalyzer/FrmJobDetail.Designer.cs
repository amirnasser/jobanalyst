using JobAnalyzer.BLL;

namespace JobAnalyzer
{
    partial class FrmJobDetail
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmJobDetail));
            pnlForm = new Panel();
            btnExperience = new Button();
            label15 = new Label();
            label10 = new Label();
            dtpApply = new DateTimePicker();
            bs = new BindingSource(components);
            dtpCreate = new DateTimePicker();
            btnAttach = new Button();
            btnOpenCoverLetter = new Button();
            btnAttachCoverLetter = new Button();
            bntJonpost = new Button();
            btnLinkCoLinkedin = new Button();
            btnLnkCompany = new Button();
            label14 = new Label();
            txtJobTitle = new TextBox();
            label9 = new Label();
            txtCompany = new TextBox();
            txtWhen = new TextBox();
            label13 = new Label();
            txtWhere = new TextBox();
            label12 = new Label();
            cmbLoaction = new ComboBox();
            cmbJobType = new ComboBox();
            txtSalary = new TextBox();
            label11 = new Label();
            chkApplied = new CheckBox();
            chkMatched = new CheckBox();
            txtMissing = new TextBox();
            txtRequirements = new TextBox();
            txtCoverletter = new TextBox();
            txtReason = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            txtJobPost = new TextBox();
            label3 = new Label();
            btnSave = new Button();
            txtCompanyLinkedIn = new TextBox();
            label2 = new Label();
            txtCompanyUrl = new TextBox();
            label1 = new Label();
            lblCompany = new Label();
            toolStrip1 = new ToolStrip();
            btnProcess = new ToolStripButton();
            btnLoadJobs = new ToolStripButton();
            btnSaveJob = new ToolStripButton();
            btnSaveAllJson = new ToolStripButton();
            btnRefreshFolder = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            btnFirst = new ToolStripButton();
            btnPrev = new ToolStripButton();
            btnNext = new ToolStripButton();
            btnLast = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripButton1 = new ToolStripButton();
            btnExport = new ToolStripButton();
            btnChrome = new ToolStripButton();
            btnDelete = new ToolStripButton();
            btnOpen = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripSeparator3 = new ToolStripSeparator();
            btnLoad = new ToolStripButton();
            btnSaveAll = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            btnSaveToDb = new ToolStripButton();
            btnTuggle = new ToolStripButton();
            toolStripSeparator6 = new ToolStripSeparator();
            btnCreatFolder = new ToolStripButton();
            imageList1 = new ImageList(components);
            statusStrip1 = new StatusStrip();
            txtStatus = new ToolStripStatusLabel();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            lblJobCount = new ToolStripStatusLabel();
            lstJobs = new ListBox();
            cmenu = new ContextMenuStrip(components);
            deleteTSM = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripSeparator();
            sortAToolStripMenuItem = new ToolStripMenuItem();
            sortDescendingToolStripMenuItem = new ToolStripMenuItem();
            wv1 = new Microsoft.Web.WebView2.WinForms.WebView2();
            wv2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            splitContainer1 = new SplitContainer();
            txtSearch = new TextBox();
            label8 = new Label();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            mnuSave = new ToolStripMenuItem();
            databaseToolStripMenuItem = new ToolStripMenuItem();
            saveToDBToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            mnuDelete = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            mnuJobsFromDb = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripSeparator();
            mnuAsc = new ToolStripMenuItem();
            mnuDesc = new ToolStripMenuItem();
            txtTimer = new ToolStripStatusLabel();
            pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bs).BeginInit();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            cmenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)wv1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)wv2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlForm
            // 
            pnlForm.Controls.Add(btnExperience);
            pnlForm.Controls.Add(label15);
            pnlForm.Controls.Add(label10);
            pnlForm.Controls.Add(dtpApply);
            pnlForm.Controls.Add(dtpCreate);
            pnlForm.Controls.Add(btnAttach);
            pnlForm.Controls.Add(btnOpenCoverLetter);
            pnlForm.Controls.Add(btnAttachCoverLetter);
            pnlForm.Controls.Add(bntJonpost);
            pnlForm.Controls.Add(btnLinkCoLinkedin);
            pnlForm.Controls.Add(btnLnkCompany);
            pnlForm.Controls.Add(label14);
            pnlForm.Controls.Add(txtJobTitle);
            pnlForm.Controls.Add(label9);
            pnlForm.Controls.Add(txtCompany);
            pnlForm.Controls.Add(txtWhen);
            pnlForm.Controls.Add(label13);
            pnlForm.Controls.Add(txtWhere);
            pnlForm.Controls.Add(label12);
            pnlForm.Controls.Add(cmbLoaction);
            pnlForm.Controls.Add(cmbJobType);
            pnlForm.Controls.Add(txtSalary);
            pnlForm.Controls.Add(label11);
            pnlForm.Controls.Add(chkApplied);
            pnlForm.Controls.Add(chkMatched);
            pnlForm.Controls.Add(txtMissing);
            pnlForm.Controls.Add(txtRequirements);
            pnlForm.Controls.Add(txtCoverletter);
            pnlForm.Controls.Add(txtReason);
            pnlForm.Controls.Add(label7);
            pnlForm.Controls.Add(label6);
            pnlForm.Controls.Add(label5);
            pnlForm.Controls.Add(label4);
            pnlForm.Controls.Add(txtJobPost);
            pnlForm.Controls.Add(label3);
            pnlForm.Controls.Add(btnSave);
            pnlForm.Controls.Add(txtCompanyLinkedIn);
            pnlForm.Controls.Add(label2);
            pnlForm.Controls.Add(txtCompanyUrl);
            pnlForm.Controls.Add(label1);
            pnlForm.Controls.Add(lblCompany);
            pnlForm.Location = new Point(270, 48);
            pnlForm.Name = "pnlForm";
            pnlForm.Size = new Size(358, 830);
            pnlForm.TabIndex = 1;
            // 
            // btnExperience
            // 
            btnExperience.Location = new Point(12, 696);
            btnExperience.Name = "btnExperience";
            btnExperience.Size = new Size(158, 23);
            btnExperience.TabIndex = 44;
            btnExperience.Text = "Copy Professional Exp.";
            btnExperience.UseVisualStyleBackColor = true;
            btnExperience.Click += btnExperience_Click;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(11, 772);
            label15.Name = "label15";
            label15.Size = new Size(64, 15);
            label15.TabIndex = 43;
            label15.Text = "Apply date";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(10, 734);
            label10.Name = "label10";
            label10.Size = new Size(67, 15);
            label10.TabIndex = 42;
            label10.Text = "Create date";
            label10.Click += label10_Click;
            // 
            // dtpApply
            // 
            dtpApply.DataBindings.Add(new Binding("Value", bs, "AppliedAt", true));
            dtpApply.Location = new Point(85, 766);
            dtpApply.Name = "dtpApply";
            dtpApply.Size = new Size(253, 23);
            dtpApply.TabIndex = 41;
            // 
            // bs
            // 
            bs.DataSource = typeof(JobObject);
            bs.CurrentChanged += bs_CurrentChanged;
            // 
            // dtpCreate
            // 
            dtpCreate.DataBindings.Add(new Binding("Value", bs, "CreatedAt", true));
            dtpCreate.Location = new Point(85, 728);
            dtpCreate.Name = "dtpCreate";
            dtpCreate.Size = new Size(253, 23);
            dtpCreate.TabIndex = 40;
            // 
            // btnAttach
            // 
            btnAttach.Location = new Point(176, 696);
            btnAttach.Name = "btnAttach";
            btnAttach.Size = new Size(162, 23);
            btnAttach.TabIndex = 39;
            btnAttach.Text = "Attach Cover Letter";
            btnAttach.UseVisualStyleBackColor = true;
            btnAttach.Click += btnAttach_Click;
            // 
            // btnOpenCoverLetter
            // 
            btnOpenCoverLetter.Enabled = false;
            btnOpenCoverLetter.Location = new Point(176, 667);
            btnOpenCoverLetter.Name = "btnOpenCoverLetter";
            btnOpenCoverLetter.Size = new Size(162, 23);
            btnOpenCoverLetter.TabIndex = 38;
            btnOpenCoverLetter.Text = "Open Cover Letter";
            btnOpenCoverLetter.UseVisualStyleBackColor = true;
            btnOpenCoverLetter.Click += btnOpenCoverLetter_Click;
            // 
            // btnAttachCoverLetter
            // 
            btnAttachCoverLetter.Location = new Point(12, 667);
            btnAttachCoverLetter.Name = "btnAttachCoverLetter";
            btnAttachCoverLetter.Size = new Size(158, 23);
            btnAttachCoverLetter.TabIndex = 37;
            btnAttachCoverLetter.Text = "Create Cover Letter";
            btnAttachCoverLetter.UseVisualStyleBackColor = true;
            btnAttachCoverLetter.Click += btnAttachCoverLetter_Click;
            // 
            // bntJonpost
            // 
            bntJonpost.Image = ImageResource.g15_16;
            bntJonpost.Location = new Point(306, 210);
            bntJonpost.Name = "bntJonpost";
            bntJonpost.Size = new Size(32, 23);
            bntJonpost.TabIndex = 36;
            bntJonpost.UseVisualStyleBackColor = true;
            bntJonpost.Click += bntJonpost_Click;
            // 
            // btnLinkCoLinkedin
            // 
            btnLinkCoLinkedin.Image = ImageResource.g15_16;
            btnLinkCoLinkedin.Location = new Point(306, 166);
            btnLinkCoLinkedin.Name = "btnLinkCoLinkedin";
            btnLinkCoLinkedin.Size = new Size(32, 23);
            btnLinkCoLinkedin.TabIndex = 35;
            btnLinkCoLinkedin.UseVisualStyleBackColor = true;
            btnLinkCoLinkedin.Click += btnLinkCoLinkedin_Click;
            // 
            // btnLnkCompany
            // 
            btnLnkCompany.Image = ImageResource.g15_16;
            btnLnkCompany.Location = new Point(308, 123);
            btnLnkCompany.Name = "btnLnkCompany";
            btnLnkCompany.Size = new Size(32, 23);
            btnLnkCompany.TabIndex = 34;
            btnLnkCompany.UseVisualStyleBackColor = true;
            btnLnkCompany.Click += btnLnkCompany_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.DataBindings.Add(new Binding("Text", bs, "id", true));
            label14.Location = new Point(85, 11);
            label14.Name = "label14";
            label14.Size = new Size(0, 15);
            label14.TabIndex = 33;
            label14.Click += label14_Click;
            // 
            // txtJobTitle
            // 
            txtJobTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtJobTitle.DataBindings.Add(new Binding("Text", bs, "Title", true));
            txtJobTitle.Enabled = false;
            txtJobTitle.Location = new Point(10, 29);
            txtJobTitle.Name = "txtJobTitle";
            txtJobTitle.Size = new Size(330, 23);
            txtJobTitle.TabIndex = 31;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 11);
            label9.Name = "label9";
            label9.Size = new Size(51, 15);
            label9.TabIndex = 30;
            label9.Text = "Job Title";
            // 
            // txtCompany
            // 
            txtCompany.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCompany.DataBindings.Add(new Binding("Text", bs, "CompanyName", true));
            txtCompany.Location = new Point(10, 79);
            txtCompany.Name = "txtCompany";
            txtCompany.Size = new Size(330, 23);
            txtCompany.TabIndex = 1;
            // 
            // txtWhen
            // 
            txtWhen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtWhen.DataBindings.Add(new Binding("Text", bs, "When", true));
            txtWhen.Location = new Point(12, 638);
            txtWhen.Name = "txtWhen";
            txtWhen.Size = new Size(328, 23);
            txtWhen.TabIndex = 29;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(12, 620);
            label13.Name = "label13";
            label13.Size = new Size(38, 15);
            label13.TabIndex = 28;
            label13.Text = "When";
            // 
            // txtWhere
            // 
            txtWhere.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtWhere.DataBindings.Add(new Binding("Text", bs, "Where", true));
            txtWhere.Location = new Point(10, 594);
            txtWhere.Name = "txtWhere";
            txtWhere.Size = new Size(330, 23);
            txtWhere.TabIndex = 27;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(10, 576);
            label12.Name = "label12";
            label12.Size = new Size(41, 15);
            label12.TabIndex = 26;
            label12.Text = "Where";
            // 
            // cmbLoaction
            // 
            cmbLoaction.DataBindings.Add(new Binding("Text", bs, "JobLocation", true));
            cmbLoaction.FormattingEnabled = true;
            cmbLoaction.Items.AddRange(new object[] { JobLocation.None, JobLocation.Remote, JobLocation.Hybrid, JobLocation.InOffice });
            cmbLoaction.Location = new Point(120, 550);
            cmbLoaction.Name = "cmbLoaction";
            cmbLoaction.Size = new Size(99, 23);
            cmbLoaction.TabIndex = 13;
            // 
            // cmbJobType
            // 
            cmbJobType.DataBindings.Add(new Binding("Text", bs, "JobType", true));
            cmbJobType.FormattingEnabled = true;
            cmbJobType.Items.AddRange(new object[] { JobType.None, JobType.Fulltime, JobType.Parttime, JobType.Contract });
            cmbJobType.Location = new Point(12, 550);
            cmbJobType.Name = "cmbJobType";
            cmbJobType.Size = new Size(99, 23);
            cmbJobType.TabIndex = 12;
            // 
            // txtSalary
            // 
            txtSalary.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSalary.DataBindings.Add(new Binding("Text", bs, "SalaryRange", true));
            txtSalary.Location = new Point(11, 521);
            txtSalary.Name = "txtSalary";
            txtSalary.Size = new Size(329, 23);
            txtSalary.TabIndex = 11;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(11, 503);
            label11.Name = "label11";
            label11.Size = new Size(74, 15);
            label11.TabIndex = 22;
            label11.Text = "Salary Range";
            // 
            // chkApplied
            // 
            chkApplied.AutoSize = true;
            chkApplied.DataBindings.Add(new Binding("CheckState", bs, "Applied", true));
            chkApplied.Location = new Point(137, 481);
            chkApplied.Name = "chkApplied";
            chkApplied.Size = new Size(67, 19);
            chkApplied.TabIndex = 10;
            chkApplied.Text = "Applied";
            chkApplied.UseVisualStyleBackColor = true;
            chkApplied.CheckedChanged += chkApplied_CheckedChanged;
            // 
            // chkMatched
            // 
            chkMatched.AutoSize = true;
            chkMatched.DataBindings.Add(new Binding("CheckState", bs, "Matched", true));
            chkMatched.Location = new Point(11, 481);
            chkMatched.Name = "chkMatched";
            chkMatched.Size = new Size(73, 19);
            chkMatched.TabIndex = 9;
            chkMatched.Text = "Matched";
            chkMatched.UseVisualStyleBackColor = true;
            // 
            // txtMissing
            // 
            txtMissing.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtMissing.DataBindings.Add(new Binding("Text", bs, "MissingRequirements", true));
            txtMissing.Location = new Point(10, 452);
            txtMissing.Name = "txtMissing";
            txtMissing.Size = new Size(330, 23);
            txtMissing.TabIndex = 8;
            // 
            // txtRequirements
            // 
            txtRequirements.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtRequirements.DataBindings.Add(new Binding("Text", bs, "JobRequirements", true));
            txtRequirements.Location = new Point(10, 408);
            txtRequirements.Name = "txtRequirements";
            txtRequirements.Size = new Size(330, 23);
            txtRequirements.TabIndex = 7;
            // 
            // txtCoverletter
            // 
            txtCoverletter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCoverletter.DataBindings.Add(new Binding("Text", bs, "Coverletter", true));
            txtCoverletter.Location = new Point(10, 299);
            txtCoverletter.Multiline = true;
            txtCoverletter.Name = "txtCoverletter";
            txtCoverletter.Size = new Size(330, 88);
            txtCoverletter.TabIndex = 6;
            // 
            // txtReason
            // 
            txtReason.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtReason.DataBindings.Add(new Binding("Text", bs, "Reason", true));
            txtReason.Location = new Point(10, 255);
            txtReason.Name = "txtReason";
            txtReason.Size = new Size(330, 23);
            txtReason.TabIndex = 5;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(10, 434);
            label7.Name = "label7";
            label7.Size = new Size(124, 15);
            label7.TabIndex = 15;
            label7.Text = "Missing Requirements";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(10, 390);
            label6.Name = "label6";
            label6.Size = new Size(101, 15);
            label6.TabIndex = 13;
            label6.Text = "Job Requirements";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 281);
            label5.Name = "label5";
            label5.Size = new Size(71, 15);
            label5.TabIndex = 11;
            label5.Text = "Cover Letter";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 237);
            label4.Name = "label4";
            label4.Size = new Size(45, 15);
            label4.TabIndex = 9;
            label4.Text = "Reason";
            // 
            // txtJobPost
            // 
            txtJobPost.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtJobPost.DataBindings.Add(new Binding("Text", bs, "JobPostUrl", true));
            txtJobPost.Location = new Point(10, 211);
            txtJobPost.Name = "txtJobPost";
            txtJobPost.Size = new Size(290, 23);
            txtJobPost.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 193);
            label3.Name = "label3";
            label3.Size = new Size(69, 15);
            label3.TabIndex = 7;
            label3.Text = "Job Post Url";
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(318, 1392);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // txtCompanyLinkedIn
            // 
            txtCompanyLinkedIn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCompanyLinkedIn.DataBindings.Add(new Binding("Text", bs, "CompanyLinkedIn", true));
            txtCompanyLinkedIn.Location = new Point(10, 167);
            txtCompanyLinkedIn.Name = "txtCompanyLinkedIn";
            txtCompanyLinkedIn.Size = new Size(292, 23);
            txtCompanyLinkedIn.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 149);
            label2.Name = "label2";
            label2.Size = new Size(107, 15);
            label2.TabIndex = 4;
            label2.Text = "Company LinkedIn";
            // 
            // txtCompanyUrl
            // 
            txtCompanyUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCompanyUrl.DataBindings.Add(new Binding("Text", bs, "CompanyUrl", true));
            txtCompanyUrl.Location = new Point(10, 123);
            txtCompanyUrl.Name = "txtCompanyUrl";
            txtCompanyUrl.Size = new Size(292, 23);
            txtCompanyUrl.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 105);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 2;
            label1.Text = "Company URL";
            // 
            // lblCompany
            // 
            lblCompany.AutoSize = true;
            lblCompany.Location = new Point(10, 61);
            lblCompany.Name = "lblCompany";
            lblCompany.Size = new Size(59, 15);
            lblCompany.TabIndex = 0;
            lblCompany.Text = "Company";
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnProcess, btnLoadJobs, btnSaveJob, btnSaveAllJson, btnRefreshFolder, toolStripSeparator5, btnFirst, btnPrev, btnNext, btnLast, toolStripSeparator1, toolStripButton1, btnExport, btnChrome, btnDelete, btnOpen, toolStripSeparator2, toolStripSeparator3, btnLoad, btnSaveAll, toolStripSeparator4, btnSaveToDb, btnTuggle, toolStripSeparator6, btnCreatFolder });
            toolStrip1.Location = new Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1184, 25);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnProcess
            // 
            btnProcess.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnProcess.Image = ImageResource.play;
            btnProcess.ImageTransparentColor = Color.Magenta;
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(23, 22);
            btnProcess.Text = "toolStripButton1";
            btnProcess.Click += btnProcess_Click;
            // 
            // btnLoadJobs
            // 
            btnLoadJobs.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnLoadJobs.Image = ImageResource.g19_16;
            btnLoadJobs.ImageTransparentColor = Color.Magenta;
            btnLoadJobs.Name = "btnLoadJobs";
            btnLoadJobs.Size = new Size(23, 22);
            btnLoadJobs.Text = "toolStripButton1";
            btnLoadJobs.TextImageRelation = TextImageRelation.Overlay;
            btnLoadJobs.ToolTipText = "Open Job files";
            btnLoadJobs.Click += btnLoadJobs_Click;
            // 
            // btnSaveJob
            // 
            btnSaveJob.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSaveJob.Image = ImageResource._save_16;
            btnSaveJob.ImageTransparentColor = Color.Magenta;
            btnSaveJob.Name = "btnSaveJob";
            btnSaveJob.Size = new Size(23, 22);
            btnSaveJob.Text = "toolStripButton1";
            btnSaveJob.ToolTipText = "Save job";
            btnSaveJob.Click += btnSaveJob_Click;
            // 
            // btnSaveAllJson
            // 
            btnSaveAllJson.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSaveAllJson.Image = ImageResource._saveas_16;
            btnSaveAllJson.ImageTransparentColor = Color.Magenta;
            btnSaveAllJson.Name = "btnSaveAllJson";
            btnSaveAllJson.Size = new Size(23, 22);
            btnSaveAllJson.Text = "toolStripButton2";
            btnSaveAllJson.ToolTipText = "Save all as json";
            btnSaveAllJson.Click += btnSaveAllJson_Click;
            // 
            // btnRefreshFolder
            // 
            btnRefreshFolder.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnRefreshFolder.Image = ImageResource.g45_16;
            btnRefreshFolder.ImageTransparentColor = Color.Magenta;
            btnRefreshFolder.Name = "btnRefreshFolder";
            btnRefreshFolder.Size = new Size(23, 22);
            btnRefreshFolder.Text = "toolStripButton1";
            btnRefreshFolder.ToolTipText = "Refresh Jobs";
            btnRefreshFolder.Click += btnRefreshFolder_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 25);
            // 
            // btnFirst
            // 
            btnFirst.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnFirst.Image = ImageResource._first_16;
            btnFirst.ImageTransparentColor = Color.Magenta;
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new Size(23, 22);
            btnFirst.ToolTipText = "First";
            btnFirst.Click += btnFirst_Click;
            // 
            // btnPrev
            // 
            btnPrev.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnPrev.Image = ImageResource._prev_16;
            btnPrev.ImageTransparentColor = Color.Magenta;
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(23, 22);
            btnPrev.Text = "toolStripButton2";
            btnPrev.Click += btnPrev_Click;
            // 
            // btnNext
            // 
            btnNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnNext.Image = ImageResource._next_16;
            btnNext.ImageTransparentColor = Color.Magenta;
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(23, 22);
            btnNext.Text = "toolStripButton3";
            btnNext.ToolTipText = "Next";
            btnNext.Click += btnNext_Click;
            // 
            // btnLast
            // 
            btnLast.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnLast.Image = ImageResource._last_16;
            btnLast.ImageTransparentColor = Color.Magenta;
            btnLast.Name = "btnLast";
            btnLast.Size = new Size(23, 22);
            btnLast.Text = "toolStripButton4";
            btnLast.ToolTipText = "Last";
            btnLast.Click += btnLast_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = ImageResource.edit;
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(23, 22);
            toolStripButton1.Text = "Cover letter";
            toolStripButton1.ToolTipText = "Create Cover Letter";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // btnExport
            // 
            btnExport.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnExport.Image = ImageResource.g22_16;
            btnExport.ImageTransparentColor = Color.Magenta;
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(23, 22);
            btnExport.Text = "toolStripButton1";
            btnExport.ToolTipText = "Export Data File";
            btnExport.Click += btnExport_Click;
            // 
            // btnChrome
            // 
            btnChrome.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnChrome.Image = ImageResource.g9_16;
            btnChrome.ImageTransparentColor = Color.Magenta;
            btnChrome.Name = "btnChrome";
            btnChrome.Size = new Size(23, 22);
            btnChrome.Text = "Open in Chrome";
            btnChrome.ToolTipText = "Open Job Post in Chrome";
            btnChrome.Click += btnChrome_Click;
            // 
            // btnDelete
            // 
            btnDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnDelete.Image = ImageResource.g30_16;
            btnDelete.ImageTransparentColor = Color.Magenta;
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(23, 22);
            btnDelete.Text = "toolStripButton1";
            btnDelete.ToolTipText = "Delete Job";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnOpen
            // 
            btnOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnOpen.Image = ImageResource.g19_16;
            btnOpen.ImageTransparentColor = Color.Magenta;
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(23, 22);
            btnOpen.Text = "toolStripButton2";
            btnOpen.ToolTipText = "Open HTML Job File";
            btnOpen.Click += btnOpen_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
            // 
            // btnLoad
            // 
            btnLoad.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnLoad.Image = ImageResource._import_16;
            btnLoad.ImageTransparentColor = Color.Magenta;
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(23, 22);
            btnLoad.Text = "toolStripButton1";
            btnLoad.ToolTipText = "Load Data";
            btnLoad.Click += btnLoad_Click;
            // 
            // btnSaveAll
            // 
            btnSaveAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSaveAll.Image = ImageResource.g31_16;
            btnSaveAll.ImageTransparentColor = Color.Magenta;
            btnSaveAll.Name = "btnSaveAll";
            btnSaveAll.Size = new Size(23, 22);
            btnSaveAll.Text = "toolStripButton1";
            btnSaveAll.ToolTipText = "Save All";
            btnSaveAll.Click += btnSaveAll_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 25);
            // 
            // btnSaveToDb
            // 
            btnSaveToDb.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSaveToDb.Image = ImageResource.g12_16;
            btnSaveToDb.ImageTransparentColor = Color.Magenta;
            btnSaveToDb.Name = "btnSaveToDb";
            btnSaveToDb.Size = new Size(23, 22);
            btnSaveToDb.Text = "toolStripButton1";
            btnSaveToDb.ToolTipText = "Save to Database and Delete it from folder";
            btnSaveToDb.Click += btnSaveToDb_Click;
            // 
            // btnTuggle
            // 
            btnTuggle.Checked = true;
            btnTuggle.CheckState = CheckState.Checked;
            btnTuggle.Image = ImageResource.g45_16;
            btnTuggle.ImageTransparentColor = Color.Magenta;
            btnTuggle.Name = "btnTuggle";
            btnTuggle.Size = new Size(106, 22);
            btnTuggle.Text = "From Database";
            btnTuggle.Click += btnTuggle_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(6, 25);
            // 
            // btnCreatFolder
            // 
            btnCreatFolder.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnCreatFolder.Image = (Image)resources.GetObject("btnCreatFolder.Image");
            btnCreatFolder.ImageTransparentColor = Color.Magenta;
            btnCreatFolder.Name = "btnCreatFolder";
            btnCreatFolder.Size = new Size(23, 22);
            btnCreatFolder.Text = "toolStripButton2";
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { txtStatus, txtTimer, toolStripStatusLabel1, lblJobCount });
            statusStrip1.Location = new Point(0, 883);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1184, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // txtStatus
            // 
            txtStatus.Name = "txtStatus";
            txtStatus.Size = new Size(1031, 17);
            txtStatus.Spring = true;
            txtStatus.Text = "toolStripStatusLabel1";
            txtStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(94, 17);
            toolStripStatusLabel1.Text = "Number of Jobs:";
            // 
            // lblJobCount
            // 
            lblJobCount.DataBindings.Add(new Binding("Text", this, "JobCount", true, DataSourceUpdateMode.OnPropertyChanged));
            lblJobCount.Name = "lblJobCount";
            lblJobCount.Size = new Size(13, 17);
            lblJobCount.Text = "0";
            // 
            // lstJobs
            // 
            lstJobs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lstJobs.ContextMenuStrip = cmenu;
            lstJobs.FormattingEnabled = true;
            lstJobs.ItemHeight = 15;
            lstJobs.Location = new Point(12, 78);
            lstJobs.Name = "lstJobs";
            lstJobs.Size = new Size(252, 799);
            lstJobs.TabIndex = 4;
            lstJobs.SelectedIndexChanged += lstJobs_SelectedIndexChanged;
            // 
            // cmenu
            // 
            cmenu.Items.AddRange(new ToolStripItem[] { deleteTSM, toolStripMenuItem4, sortAToolStripMenuItem, sortDescendingToolStripMenuItem });
            cmenu.Name = "contextMenuStrip1";
            cmenu.Size = new Size(161, 76);
            // 
            // deleteTSM
            // 
            deleteTSM.Image = ImageResource.g30_16;
            deleteTSM.Name = "deleteTSM";
            deleteTSM.ShortcutKeys = Keys.Delete;
            deleteTSM.Size = new Size(160, 22);
            deleteTSM.Text = "Delete";
            deleteTSM.Click += deleteTSM_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(157, 6);
            // 
            // sortAToolStripMenuItem
            // 
            sortAToolStripMenuItem.Name = "sortAToolStripMenuItem";
            sortAToolStripMenuItem.Size = new Size(160, 22);
            sortAToolStripMenuItem.Text = "Sort Ascending";
            // 
            // sortDescendingToolStripMenuItem
            // 
            sortDescendingToolStripMenuItem.Name = "sortDescendingToolStripMenuItem";
            sortDescendingToolStripMenuItem.Size = new Size(160, 22);
            sortDescendingToolStripMenuItem.Text = "Sort Descending";
            // 
            // wv1
            // 
            wv1.AllowExternalDrop = true;
            wv1.CreationProperties = null;
            wv1.DefaultBackgroundColor = Color.White;
            wv1.Dock = DockStyle.Fill;
            wv1.Location = new Point(0, 0);
            wv1.Name = "wv1";
            wv1.Size = new Size(534, 227);
            wv1.TabIndex = 5;
            wv1.ZoomFactor = 1D;
            // 
            // wv2
            // 
            wv2.AllowExternalDrop = true;
            wv2.CreationProperties = null;
            wv2.DefaultBackgroundColor = Color.White;
            wv2.Dock = DockStyle.Fill;
            wv2.Location = new Point(0, 0);
            wv2.Name = "wv2";
            wv2.Size = new Size(534, 593);
            wv2.TabIndex = 6;
            wv2.ZoomFactor = 1D;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.BorderStyle = BorderStyle.Fixed3D;
            splitContainer1.Location = new Point(634, 48);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(wv1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(wv2);
            splitContainer1.Size = new Size(538, 832);
            splitContainer1.SplitterDistance = 231;
            splitContainer1.TabIndex = 7;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(57, 51);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(207, 23);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.KeyPress += txtSearch_KeyPress;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 54);
            label8.Name = "label8";
            label8.Size = new Size(48, 15);
            label8.TabIndex = 9;
            label8.Text = "Search: ";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, databaseToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1184, 24);
            menuStrip1.TabIndex = 10;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mnuSave });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // mnuSave
            // 
            mnuSave.Name = "mnuSave";
            mnuSave.ShortcutKeys = Keys.Control | Keys.S;
            mnuSave.Size = new Size(138, 22);
            mnuSave.Text = "&Save";
            mnuSave.Click += mnuSave_Click;
            // 
            // databaseToolStripMenuItem
            // 
            databaseToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToDBToolStripMenuItem, toolStripMenuItem1, mnuDelete, toolStripMenuItem2, mnuJobsFromDb, toolStripMenuItem3, mnuAsc, mnuDesc });
            databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            databaseToolStripMenuItem.Size = new Size(42, 20);
            databaseToolStripMenuItem.Text = "&Jobs";
            // 
            // saveToDBToolStripMenuItem
            // 
            saveToDBToolStripMenuItem.Name = "saveToDBToolStripMenuItem";
            saveToDBToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            saveToDBToolStripMenuItem.Size = new Size(225, 22);
            saveToDBToolStripMenuItem.Text = "&Save to DB";
            saveToDBToolStripMenuItem.Click += saveToDBToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(222, 6);
            // 
            // mnuDelete
            // 
            mnuDelete.Name = "mnuDelete";
            mnuDelete.ShortcutKeys = Keys.Control | Keys.Delete;
            mnuDelete.Size = new Size(225, 22);
            mnuDelete.Text = "&Delete";
            mnuDelete.Click += mnuDelete_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(222, 6);
            // 
            // mnuJobsFromDb
            // 
            mnuJobsFromDb.Name = "mnuJobsFromDb";
            mnuJobsFromDb.Size = new Size(225, 22);
            mnuJobsFromDb.Text = "&Load All From Databas";
            mnuJobsFromDb.Click += mnuJobsFromDb_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(222, 6);
            // 
            // mnuAsc
            // 
            mnuAsc.Name = "mnuAsc";
            mnuAsc.ShortcutKeys = Keys.Control | Keys.Up;
            mnuAsc.Size = new Size(225, 22);
            mnuAsc.Text = "Sort Ascending";
            mnuAsc.Click += mnuAcc_Click;
            // 
            // mnuDesc
            // 
            mnuDesc.Name = "mnuDesc";
            mnuDesc.ShortcutKeys = Keys.Control | Keys.Down;
            mnuDesc.Size = new Size(225, 22);
            mnuDesc.Text = "Sort Descending";
            mnuDesc.Click += mnuDec_Click;
            // 
            // txtTimer
            // 
            txtTimer.Name = "txtTimer";
            txtTimer.Size = new Size(0, 17);
            // 
            // FrmJobDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 905);
            Controls.Add(label8);
            Controls.Add(txtSearch);
            Controls.Add(splitContainer1);
            Controls.Add(lstJobs);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            Controls.Add(pnlForm);
            MainMenuStrip = menuStrip1;
            Name = "FrmJobDetail";
            Text = "Job Detail";
            WindowState = FormWindowState.Maximized;
            Load += FrmJobDetail_Load;
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bs).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            cmenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)wv1).EndInit();
            ((System.ComponentModel.ISupportInitialize)wv2).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlForm;
        private TextBox txtCompany;
        private TextBox txtWhen;
        private Label label13;
        private TextBox txtWhere;
        private Label label12;
        private ComboBox cmbLoaction;
        private ComboBox cmbJobType;
        private TextBox txtSalary;
        private Label label11;
        private CheckBox chkApplied;
        private CheckBox chkMatched;
        private TextBox txtMissing;
        private TextBox txtRequirements;
        private TextBox txtCoverletter;
        private TextBox txtReason;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private TextBox txtJobPost;
        private Label label3;
        private Button btnSave;
        private TextBox txtCompanyLinkedIn;
        private Label label2;
        private TextBox txtCompanyUrl;
        private Label label1;
        private Label lblCompany;
        private ToolStrip toolStrip1;
        private ToolStripButton btnFirst;
        private ToolStripButton btnPrev;
        private ToolStripButton btnNext;
        private ToolStripButton btnLast;
        private ImageList imageList1;
        private BindingSource bs;
        private ToolStripSeparator toolStripSeparator1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel txtStatus;
        private ListBox lstJobs;
        private Microsoft.Web.WebView2.WinForms.WebView2 wv1;
        private Microsoft.Web.WebView2.WinForms.WebView2 wv2;
        private SplitContainer splitContainer1;
        private ToolStripButton btnChrome;
        private ToolStripButton btnOpen;
        private ToolStripSeparator toolStripSeparator2;
        private TextBox txtSearch;
        private Label label8;
        private ToolStripButton btnExport;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton btnSaveAll;
        private ToolStripButton btnLoad;
        private TextBox txtJobTitle;
        private Label label9;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton btnSaveToDb;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem databaseToolStripMenuItem;
        private ToolStripMenuItem saveToDBToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel lblJobCount;
        private ToolStripMenuItem mnuDelete;
        private ToolStripButton btnDelete;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem mnuJobsFromDb;
        private ToolStripButton btnTuggle;
        private Label label14;
        private ToolStripButton btnProcess;
        private ToolStripSeparator toolStripSeparator5;
        private Button bntJonpost;
        private Button btnLinkCoLinkedin;
        private Button btnLnkCompany;
        private ToolStripButton btnSaveJob;
        private ToolStripMenuItem mnuSave;
        private ToolStripButton btnLoadJobs;
        private ToolStripButton btnRefreshFolder;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripButton toolStripButton1;
        private Button btnOpenCoverLetter;
        private Button btnAttachCoverLetter;
        private Button btnAttach;
        private Label label15;
        private Label label10;
        private DateTimePicker dtpApply;
        private DateTimePicker dtpCreate;
        private ToolStripButton btnSaveAllJson;
        private Button btnExperience;
        private ContextMenuStrip cmenu;
        private ToolStripMenuItem deleteTSM;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem mnuAsc;
        private ToolStripMenuItem mnuDesc;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripMenuItem sortAToolStripMenuItem;
        private ToolStripMenuItem sortDescendingToolStripMenuItem;
        private ToolStripButton btnCreatFolder;
        private ToolStripStatusLabel txtTimer;
    }
}