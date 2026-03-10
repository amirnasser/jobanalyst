using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using JobAnalyzer.BLL;
using JobAnalyzer.Data;
using Newtonsoft.Json.Bson;
using Scriban;
using Scriban.Runtime;

namespace JobAnalyzer
{
    public partial class FrmJobDetail : Form
    {
        #region Form Property
        public static List<JobObject> Jobs;
        public static List<JobObject> DbJobs;
        public bool JobsFromDb { get; set; }
        JobObject Current { get; set; }
        public string Folder { get; set; }
        public int JobCount => bs.List?.Count ?? 0;
        public const string CURRENT_FOLDER = "current_folder.txt";

        #endregion

        #region Form Methods
        public FrmJobDetail()
        {
            if (File.Exists(CURRENT_FOLDER))
            {
                Folder = File.ReadAllText(CURRENT_FOLDER);
                Jobs = GetJobs(Folder);
            }
            else
            {
                OpenLoadDialogAndLoadJobs();
            }

            InitializeComponent();
        }

        private void OpenLoadDialogAndLoadJobs()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                DefaultExt = ".json",
                Filter = "Json files (*.json)|*.html|All files (*.*)|*.*",
                CheckFileExists = false,
                FileName = "Select any job file in the folder to load all jobs from that folder"

            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Folder = Path.GetDirectoryName(openFileDialog.FileName);
                File.WriteAllText(CURRENT_FOLDER, Folder);
                Jobs = GetJobs(Folder);
            }
        }

        private async void FrmJobDetail_Load(object sender, EventArgs e)
        {
            await SetBindin(Jobs);
        }

        private async Task SetBindin(List<JobObject> jobs)
        {
            if (jobs != null)
            {
                bs.DataSource = new BindingList<JobObject>(jobs);
                bs.ResetBindings(false);
                //bs.Position = 0;
                lblJobCount.Text = jobs.Count.ToString();
                lstJobs.DataSource = new BindingList<JobObject>(jobs);

                bs.ResetBindings(true);
                var id = Current.id;
                //Repository.FindJobsById(id);

                await wv1.EnsureCoreWebView2Async();
                await wv2.EnsureCoreWebView2Async();
            }
        }

        private List<JobObject> GetJobs(string folder)
        {
            try
            {
                List<JobObject> jobs = new List<JobObject>();

                var files = Directory.GetFiles(Folder, "*.html", SearchOption.TopDirectoryOnly).
                    Concat(Directory.GetFiles(Folder, "*.json", SearchOption.TopDirectoryOnly)).
                    ToList();

                jobs = files.Select(f => new JobObject(f)).ToList();
                return jobs.ToList();
            }
            catch (Exception exp)
            {
                Utilities.Logger.Error(exp.Message);
                throw;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (bs.Position < bs.Count)
                bs.Position += 1;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            bs.Position = 0;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            bs.Position = bs.Count - 1;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            bs.Position -= 1;
        }

        private async void bs_CurrentChanged(object sender, EventArgs e)
        {
            Current = (bs?.Current as JobObject);
            if (Current != null)
            {
                txtStatus.Text = Current?.HTMLFileName;
                if (lstJobs.Items.Count > 0)
                {
                    lstJobs.SelectedIndex = bs.Position;
                }
                //Current.id = Current.JobPostUrl.LocalPath.ToMd5Hash();
                await wv1.EnsureCoreWebView2Async();
                await wv2.EnsureCoreWebView2Async();
                if (!string.IsNullOrEmpty(Current?.HTML))
                {
                    wv1.NavigateToString(Current?.HTML?.Replace("\\n", ""));
                    RenderAsync();
                }
            }
        }

        private async void lstJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bs.Position = lstJobs.SelectedIndex;
                Current = bs.Current as JobObject;
                RenderAsync();
                var directory = Current.HTMLFileName.Replace(".json", "");
                if (Directory.Exists(directory))
                {
                    FileInfo jobfileinfo = new FileInfo(Current.HTMLFileName);
                    var justname = jobfileinfo.Name;
                    string documentFile = Path.Combine(directory, justname.Replace(".json", "_CoverLetter.docx"));
                    btnOpenCoverLetter.Enabled = File.Exists(documentFile) ? true : false;
                }

            }
            catch (Exception exp)
            {
                throw;
            }

            await wv1.EnsureCoreWebView2Async();
            await wv2.EnsureCoreWebView2Async();

            bs.EndEdit();

            RenderAsync();
            if (lstJobs.SelectedIndex >= 0)
            {
                bs.Position = lstJobs.SelectedIndex;
                bs.EndEdit();
            }
        }

        private void btnChrome_Click(object sender, EventArgs e)
        {
            var url = Current?.JobPostUrl?.ToString();
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }

        public void OpenChrome(string url)
        {
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (File.Exists(Current?.HTMLFileName))
            {
                Process.Start("explorer.exe", $"/select,\"{Current?.HTMLFileName}\"");
            }
        }

        private void btnOpenJson_Click(object sender, EventArgs e)
        {
            //if (File.Exists(Current?.JsonFileName))
            //{
            //    Process.Start("explorer.exe", $"/select,\"{Current?.JsonFileName}\"");
            //}
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtSearch.Text.Trim().Length > 0)
                {
                    var tmp = Jobs.Where(j => j.HTML.Contains(txtSearch.Text)).ToList();
                    bs.DataSource = new BindingList<JobObject>(tmp);
                    lstJobs.DataSource = new BindingList<JobObject>(tmp);
                }
                else
                {
                    bs.DataSource = new BindingList<JobObject>(Jobs);
                    lstJobs.DataSource = new BindingList<JobObject>(Jobs);
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportDataFile();
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            SaveDataFile();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadDataFile();
        }

        private void btnSaveToDb_Click(object sender, EventArgs e)
        {
            SaveToDB();
        }

        private void saveToDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToDB();
        }
        #endregion

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            DeleteJob();
        }

        private void mnuJobsFromDb_Click(object sender, EventArgs e)
        {
            var allJobs = Repository.GetAllJobs();
            if (allJobs.Success)
            {
                DbJobs = allJobs.Result;
                SetBindin(DbJobs);
                JobsFromDb = true;
            }
        }

        private void btnTuggle_Click(object sender, EventArgs e)
        {
            JobsFromDb = !JobsFromDb;
            if (JobsFromDb)
            {
                btnTuggle.Text = "From Files";
                var allJobs = Repository.GetAllJobs();
                if (allJobs.Success)
                {
                    DbJobs = allJobs.Result;
                    SetBindin(DbJobs);

                }
            }
            else
            {
                btnTuggle.Text = "From Database";
                //Jobs = GetJobs(Folder);
                SetBindin(Jobs);
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Current?.id ?? string.Empty);
        }

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            txtStatus.Text = "Please wait, Processing";
            this.UseWaitCursor = true;

            using (AICall aicall = new AICall())
            {
                JobObject job = ((JobObject)Current).DeepClone();

                Response<JobObject> response = await aicall.GetResponseAsync(job);
                if (response.Success)
                {
                    Jobs.FirstOrDefault(job => job.id == response.Result.id).CopyFrom(response.Result);
                    bs.EndEdit();
                    RenderAsync();
                    job.Save();
                    SetBindin(GetJobs(Folder));
                }
            }

            txtStatus.Text = $"Data file written.";
            this.UseWaitCursor = false;
        }

        private void btnLnkCompany_Click(object sender, EventArgs e)
        {
            OpenChrome(Current?.CompanyUrl?.ToString());
        }

        private void btnLinkCoLinkedin_Click(object sender, EventArgs e)
        {
            OpenChrome(Current?.CompanyLinkedIn?.ToString());
        }

        private void bntJonpost_Click(object sender, EventArgs e)
        {
            OpenChrome(Current?.JobPostUrl?.ToString());
        }

        private void btnSaveJob_Click(object sender, EventArgs e)
        {
            bs.EndEdit();
            Current.Save();
        }


        private void mnuSave_Click(object sender, EventArgs e)
        {
            Current.Save();
        }

        private void btnLoadJobs_Click(object sender, EventArgs e)
        {
            OpenLoadDialogAndLoadJobs();
        }

        private void btnRefreshFolder_Click(object sender, EventArgs e)
        {
            SetBindin(GetJobs(Folder));
        }

        public Response<string> CreateCoverLetter(JobObject job)
        {
            try
            {
                var directory = job.HTMLFileName.Replace(".json", "");
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    FileInfo jobfileinfo = new FileInfo(job.HTMLFileName);
                    var justname = jobfileinfo.Name;
                    string documentFile = Path.Combine(directory, justname.Replace(".json", "_CoverLetter.docx"));
                    File.Copy("Coverletter.docx", documentFile);
                    ReplaceText(documentFile, "COVERLETTER", job.Coverletter.Replace("\\n", "\\r\\n"));
                    ReplaceText(documentFile, "DATE", DateTime.Now.ToString("MMM, dd yyyy"));
                    ReplaceText(documentFile, "JOBTITLE", job.Title);
                    return new Response<string>(documentFile);
                }
                throw new Exception("Cover letter already exists for this job.");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error parsing response: " + ex.Message);
                Utilities.Logger.Error(ex, "Error creating cover letter");
                return new Response<string>(ex);
            }
        }

        public void ReplaceText(string filePath, string oldText, string newText)
        {
            using (var doc = WordprocessingDocument.Open(filePath, true))
            {
                var body = doc.MainDocumentPart.Document.Body;

                foreach (var text in body.Descendants<Text>())
                {
                    if (text.Text.Contains(oldText))
                        text.Text = text.Text.Replace(oldText, newText);
                }

                doc.MainDocumentPart.Document.Save();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnAttachCoverLetter_Click(object sender, EventArgs e)
        {
            var coverLetterFile = CreateCoverLetter(Current);

            if (coverLetterFile.Success)
            {
                Process.Start(new ProcessStartInfo(coverLetterFile.Result) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show("Error creating cover letter: " + coverLetterFile.ErrorMessage);
            }
        }

        private void btnOpenCoverLetter_Click(object sender, EventArgs e)
        {
            var directory = Current.HTMLFileName.Replace(".json", "");
            if (Directory.Exists(directory))
            {
                FileInfo jobfileinfo = new FileInfo(Current.HTMLFileName);
                var justname = jobfileinfo.Name;
                string documentFile = Path.Combine(directory, justname.Replace(".json", "_CoverLetter.docx"));


                Process.Start(new ProcessStartInfo(documentFile) { UseShellExecute = true });
            }
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            var directory = Current.HTMLFileName.Replace(".json", "");
            if (Directory.Exists(directory))
            {
                FileInfo jobfileinfo = new FileInfo(Current.HTMLFileName);
                var justname = jobfileinfo.Name;
                string documentFile = Path.Combine(directory, justname.Replace(".json", "_CoverLetter.docx"));

                Current.CoverletterFile = File.ReadAllBytes(documentFile).ToBase64();

                bs.EndEdit();
                Current.Save();
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Current.CreatedAt = new FileInfo(Current.HTMLFileName).CreationTime;
            bs.EndEdit();
            Current.Save();
        }

        private void chkApplied_CheckedChanged(object sender, EventArgs e)
        {
            if (chkApplied.Checked)
            {
                Current.AppliedAt = DateTime.Now;
                bs.EndEdit();
                Current.Save();
            }
        }

        private void btnSaveAllJson_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "All JSON File (*.json)|*.json" };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var datatosave = ((IEnumerable<JobObject>)bs.DataSource).ToList();
                File.WriteAllText(saveFileDialog.FileName, Newtonsoft.Json.JsonConvert.SerializeObject(datatosave));
            }
        }

        private void btnExperience_Click(object sender, EventArgs e)
        {
            if(Current.ProfessionalSummary != null)
            {
                Clipboard.SetText(Current.ProfessionalSummary);
                MessageBox.Show(Current.ProfessionalSummary);
            }
        }
    }
}
