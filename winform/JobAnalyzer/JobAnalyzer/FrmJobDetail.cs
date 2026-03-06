using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using JobAnalyzer.BLL;
using JobAnalyzer.Data;
using Scriban;

namespace JobAnalyzer
{
    public partial class FrmJobDetail : Form
    {
        #region Form Property
        public static List<JobObject> Jobs;

        JobObject Current { get; set; }

        public string Folder { get; set; }
        #endregion

        #region Form Methods
        public FrmJobDetail(string path)
        {
            Folder = path;
            Jobs = GetJobs(Folder);


            InitializeComponent();
        }

        private async void FrmJobDetail_Load(object sender, EventArgs e)
        {
            if (Jobs != null)
            {
                bs.DataSource = new BindingList<JobObject>(Jobs);
                bs.Position = 0;

                lstJobs.DataSource = new BindingList<JobObject>(Jobs);
                lstJobs.ValueMember = "HTMLFileName";
                lstJobs.DisplayMember = "Shortname";

                bs.ResumeBinding();

                await wv1.EnsureCoreWebView2Async();
                await wv2.EnsureCoreWebView2Async();
            }
        }

        private List<JobObject> GetJobs(string folder)
        {
            try
            {
                List<JobObject> jobs = new List<JobObject>();
                List<FileInfo> files = Directory.GetFiles(folder, "*.html", SearchOption.TopDirectoryOnly)
                    .Concat(Directory.GetFiles(folder, "*.job.json", SearchOption.TopDirectoryOnly))
                    .Select(f => new FileInfo(f)).ToList();
                foreach (FileInfo file in files)
                {
                    if (file.Name.EndsWith(".html"))
                    {
                        jobs.Add(new JobObject(file.FullName));
                    }
                    else if (file.Name.EndsWith(".job.json"))
                    {
                        JobFile jobFile = JobFile.LoadFromFile(file.FullName);
                        jobs.Add(new JobObject(Folder, jobFile));
                    }
                }
                return jobs.OrderByDescending(o => o.Shortname).ToList();
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
                await wv1.EnsureCoreWebView2Async();
                await wv2.EnsureCoreWebView2Async();
                if(!string.IsNullOrEmpty(Current?.HTML))
                {
                    wv1.NavigateToString(Current?.HTML?.Replace("\\n", ""));
                    RenderAsync();
                }
            }
        }

        private void lstJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (File.Exists(Current?.HTMLFileName))
            {
                Process.Start("explorer.exe", $"/select,\"{Current?.HTMLFileName}\"");
            }
        }

        private void btnOpenJson_Click(object sender, EventArgs e)
        {
            if (File.Exists(Current?.JsonFileName))
            {
                Process.Start("explorer.exe", $"/select,\"{Current?.JsonFileName}\"");
            }
        }

        #endregion

        #region Other methods

        private async Task RenderAsync()
        {
            try
            {
                Template template = Template.Parse(File.ReadAllText("Template.html"));
                string result = template.Render(new { model = Current.AIResponse });
                wv2.NavigateToString(result);
            }
            catch (Exception exp)
            {
                Utilities.Logger.Error(exp, "Error rendering template");
            }
        }
        #endregion


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
            try
            {
                if (Current != null)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog() { DefaultExt = ".data", FileName = Current?.HTMLFileName.Replace(".html", ".data"), Filter = "All data files (*.data)|*.data|All files (*.*)|*.*" };
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Current.ExportDataFile(saveFileDialog.FileName);
                        txtStatus.Text = $"File `{saveFileDialog.FileName}` saved successfully.";
                    }
                }
            }
            catch (Exception exp)
            {
                Utilities.Logger.Error(exp.Message);
            }
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            bs.EndEdit();
            var datatosave = ((IEnumerable<JobObject>)bs.DataSource).ToList();
            foreach (var job in datatosave)
            {
                job.Text = job.HTML.GetText();
                job.HTMLFileName = new FileInfo(job.HTMLFileName).Name;

                
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog() { DefaultExt = ".data", FileName = $"Jobs_{DateTime.Now:yyyyMMddHHmmss}.data", Filter = "All data files (*.data)|*.data|All files (*.*)|*.*" };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    JobObject.SaveAllToFile(saveFileDialog.FileName, datatosave);
                    txtStatus.Text = $"All data save to file `{saveFileDialog.FileName}` saved successfully.";
                }
                catch (Exception exp)
                {
                    Utilities.Logger.Error(exp.Message);
                    MessageBox.Show($"Something went wrong please check the log file");
                }
            }

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { DefaultExt = ".data", Filter = "All data files (*.data)|*.data|All files (*.*)|*.*" };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Response<List<JobObject>> loadedJobs = JobObject.LoadAllFromFile(openFileDialog.FileName);
                    if (loadedJobs.Success)
                    {
                        Jobs = loadedJobs.Result;
                        bs.DataSource = new BindingList<JobObject>(Jobs);
                        lstJobs.DataSource = new BindingList<JobObject>(Jobs);
                        txtStatus.Text = $"All data loaded from file `{openFileDialog.FileName}` successfully.";
                    }
                    else
                    {
                        MessageBox.Show($"Failed to load data from file `{openFileDialog.FileName}`. Please check the log for details.");
                    }
                }
                catch (Exception exp)
                {
                    Utilities.Logger.Error(exp.Message);
                    MessageBox.Show($"Something went wrong please check the log file");
                }
            }
        }
    }
}
