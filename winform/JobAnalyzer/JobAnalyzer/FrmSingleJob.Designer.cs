using JobAnalyzer.BLL;

namespace JobAnalyzer
{
    partial class FrmSingleJob
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
            splitContainer1 = new SplitContainer();
            wv1 = new Microsoft.Web.WebView2.WinForms.WebView2();
            wv2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            pnlForm = new Panel();
            txtCompany = new TextBox();
            bs = new BindingSource(components);
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
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)wv1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)wv2).BeginInit();
            pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bs).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.BorderStyle = BorderStyle.Fixed3D;
            splitContainer1.Location = new Point(376, 12);
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
            splitContainer1.Size = new Size(624, 809);
            splitContainer1.SplitterDistance = 403;
            splitContainer1.TabIndex = 9;
            // 
            // wv1
            // 
            wv1.AllowExternalDrop = true;
            wv1.CreationProperties = null;
            wv1.DefaultBackgroundColor = Color.White;
            wv1.Dock = DockStyle.Fill;
            wv1.Location = new Point(0, 0);
            wv1.Name = "wv1";
            wv1.Size = new Size(620, 399);
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
            wv2.Size = new Size(620, 398);
            wv2.TabIndex = 6;
            wv2.ZoomFactor = 1D;
            // 
            // pnlForm
            // 
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
            pnlForm.Location = new Point(12, 12);
            pnlForm.Name = "pnlForm";
            pnlForm.Size = new Size(358, 807);
            pnlForm.TabIndex = 8;
            // 
            // txtCompany
            // 
            txtCompany.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCompany.DataBindings.Add(new Binding("Text", bs, "CompanyName", true));
            txtCompany.Location = new Point(12, 29);
            txtCompany.Name = "txtCompany";
            txtCompany.Size = new Size(333, 23);
            txtCompany.TabIndex = 1;
            // 
            // bs
            // 
            bs.DataSource = typeof(JobObject);
            // 
            // txtWhen
            // 
            txtWhen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtWhen.DataBindings.Add(new Binding("Text", bs, "When", true));
            txtWhen.Location = new Point(13, 707);
            txtWhen.Name = "txtWhen";
            txtWhen.Size = new Size(331, 23);
            txtWhen.TabIndex = 29;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(13, 689);
            label13.Name = "label13";
            label13.Size = new Size(38, 15);
            label13.TabIndex = 28;
            label13.Text = "When";
            // 
            // txtWhere
            // 
            txtWhere.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtWhere.DataBindings.Add(new Binding("Text", bs, "Where", true));
            txtWhere.Location = new Point(11, 663);
            txtWhere.Name = "txtWhere";
            txtWhere.Size = new Size(333, 23);
            txtWhere.TabIndex = 27;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(11, 645);
            label12.Name = "label12";
            label12.Size = new Size(41, 15);
            label12.TabIndex = 26;
            label12.Text = "Where";
            // 
            // cmbLoaction
            // 
            cmbLoaction.FormattingEnabled = true;
            cmbLoaction.Items.AddRange(new object[] { JobLocation.None, JobLocation.Remote, JobLocation.Hybrid, JobLocation.InOffice });
            cmbLoaction.Location = new Point(121, 619);
            cmbLoaction.Name = "cmbLoaction";
            cmbLoaction.Size = new Size(99, 23);
            cmbLoaction.TabIndex = 13;
            // 
            // cmbJobType
            // 
            cmbJobType.FormattingEnabled = true;
            cmbJobType.Items.AddRange(new object[] { JobType.None, JobType.Fulltime, JobType.Parttime, JobType.Contract });
            cmbJobType.Location = new Point(13, 619);
            cmbJobType.Name = "cmbJobType";
            cmbJobType.Size = new Size(99, 23);
            cmbJobType.TabIndex = 12;
            // 
            // txtSalary
            // 
            txtSalary.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSalary.DataBindings.Add(new Binding("Text", bs, "SalaryRange", true));
            txtSalary.Location = new Point(12, 590);
            txtSalary.Name = "txtSalary";
            txtSalary.Size = new Size(332, 23);
            txtSalary.TabIndex = 11;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(12, 572);
            label11.Name = "label11";
            label11.Size = new Size(74, 15);
            label11.TabIndex = 22;
            label11.Text = "Salary Range";
            // 
            // chkApplied
            // 
            chkApplied.AutoSize = true;
            chkApplied.DataBindings.Add(new Binding("Checked", bs, "Applied", true));
            chkApplied.Location = new Point(138, 550);
            chkApplied.Name = "chkApplied";
            chkApplied.Size = new Size(67, 19);
            chkApplied.TabIndex = 10;
            chkApplied.Text = "Applied";
            chkApplied.UseVisualStyleBackColor = true;
            // 
            // chkMatched
            // 
            chkMatched.AutoSize = true;
            chkMatched.DataBindings.Add(new Binding("Checked", bs, "Matched", true));
            chkMatched.Location = new Point(12, 550);
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
            txtMissing.Location = new Point(14, 459);
            txtMissing.Multiline = true;
            txtMissing.Name = "txtMissing";
            txtMissing.Size = new Size(333, 85);
            txtMissing.TabIndex = 8;
            // 
            // txtRequirements
            // 
            txtRequirements.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtRequirements.DataBindings.Add(new Binding("Text", bs, "JobRequirements", true));
            txtRequirements.Location = new Point(12, 358);
            txtRequirements.Multiline = true;
            txtRequirements.Name = "txtRequirements";
            txtRequirements.Size = new Size(333, 80);
            txtRequirements.TabIndex = 7;
            // 
            // txtCoverletter
            // 
            txtCoverletter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCoverletter.DataBindings.Add(new Binding("Text", bs, "Coverletter", true));
            txtCoverletter.Location = new Point(12, 249);
            txtCoverletter.Multiline = true;
            txtCoverletter.Name = "txtCoverletter";
            txtCoverletter.Size = new Size(333, 88);
            txtCoverletter.TabIndex = 6;
            // 
            // txtReason
            // 
            txtReason.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtReason.DataBindings.Add(new Binding("Text", bs, "Reason", true));
            txtReason.Location = new Point(12, 205);
            txtReason.Name = "txtReason";
            txtReason.Size = new Size(333, 23);
            txtReason.TabIndex = 5;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 441);
            label7.Name = "label7";
            label7.Size = new Size(124, 15);
            label7.TabIndex = 15;
            label7.Text = "Missing Requirements";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 340);
            label6.Name = "label6";
            label6.Size = new Size(101, 15);
            label6.TabIndex = 13;
            label6.Text = "Job Requirements";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 231);
            label5.Name = "label5";
            label5.Size = new Size(71, 15);
            label5.TabIndex = 11;
            label5.Text = "Cover Letter";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 187);
            label4.Name = "label4";
            label4.Size = new Size(45, 15);
            label4.TabIndex = 9;
            label4.Text = "Reason";
            // 
            // txtJobPost
            // 
            txtJobPost.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtJobPost.DataBindings.Add(new Binding("Text", bs, "JobPostUrl", true));
            txtJobPost.Location = new Point(12, 161);
            txtJobPost.Name = "txtJobPost";
            txtJobPost.Size = new Size(333, 23);
            txtJobPost.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 143);
            label3.Name = "label3";
            label3.Size = new Size(69, 15);
            label3.TabIndex = 7;
            label3.Text = "Job Post Url";
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(476, 1894);
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
            txtCompanyLinkedIn.Location = new Point(12, 117);
            txtCompanyLinkedIn.Name = "txtCompanyLinkedIn";
            txtCompanyLinkedIn.Size = new Size(333, 23);
            txtCompanyLinkedIn.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 99);
            label2.Name = "label2";
            label2.Size = new Size(107, 15);
            label2.TabIndex = 4;
            label2.Text = "Company LinkedIn";
            // 
            // txtCompanyUrl
            // 
            txtCompanyUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCompanyUrl.DataBindings.Add(new Binding("Text", bs, "CompanyUrl", true));
            txtCompanyUrl.Location = new Point(12, 73);
            txtCompanyUrl.Name = "txtCompanyUrl";
            txtCompanyUrl.Size = new Size(333, 23);
            txtCompanyUrl.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 55);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 2;
            label1.Text = "Company URL";
            // 
            // lblCompany
            // 
            lblCompany.AutoSize = true;
            lblCompany.Location = new Point(12, 11);
            lblCompany.Name = "lblCompany";
            lblCompany.Size = new Size(59, 15);
            lblCompany.TabIndex = 0;
            lblCompany.Text = "Company";
            // 
            // FrmSingleJob
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1012, 842);
            Controls.Add(splitContainer1);
            Controls.Add(pnlForm);
            Name = "FrmSingleJob";
            Text = "Job";
            WindowState = FormWindowState.Maximized;
            Load += FrmSingleJob_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)wv1).EndInit();
            ((System.ComponentModel.ISupportInitialize)wv2).EndInit();
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bs).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Microsoft.Web.WebView2.WinForms.WebView2 wv1;
        private Microsoft.Web.WebView2.WinForms.WebView2 wv2;
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
        private BindingSource bs;
    }
}