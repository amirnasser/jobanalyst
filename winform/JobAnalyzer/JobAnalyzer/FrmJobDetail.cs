using System.ComponentModel;
using System.Diagnostics;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using JobAnalyzer.BLL;
using JobAnalyzer.Data;
using Scriban;
using Scriban.Runtime;

namespace JobAnalyzer
{
    public partial class FrmJobDetail : Form
    {
        #region Fields & Properties

        public static List<JobObject> Jobs = new();
        public static List<JobObject> DbJobs = new();
        public bool JobsFromDb { get; set; }
        public string Folder { get; set; } = string.Empty;
        public int JobCount => bs.List?.Count ?? 0;
        public const string CURRENT_FOLDER = "current_folder.txt";


        private JobObject? Current { get; set; }
        private FileSystemWatcher? fileSystemWatcher;

        /// <summary>Tracks all in-flight processing tasks so the wait cursor stays on until every one completes.</summary>
        private readonly List<Task> _processingTasks = new();
        private readonly Dictionary<string, CancellationTokenSource> _pendingFileEvents = new();
        private readonly TimeSpan _debounceDelay = TimeSpan.FromMilliseconds(500);

        /// <summary>Elapsed timer shown in the status bar during processing.</summary>
        private readonly System.Windows.Forms.Timer _elapsedTimer = new() { Interval = 1000 };
        private DateTime _timerStart;

        #endregion

        #region Constructor & Initialization

        public FrmJobDetail()
        {
            InitializeComponent();

            _elapsedTimer.Tick += ElapsedTimer_Tick;
            txtTimer.Text = "00:00:00";

            if (File.Exists(CURRENT_FOLDER))
            {
                Folder = File.ReadAllText(CURRENT_FOLDER).Trim();
                Jobs = GetJobs(Folder);
                CreateFileSystemWatcher();
            }
            else
            {
                OpenLoadDialogAndLoadJobs();
            }
        }

        private async void FrmJobDetail_Load(object sender, EventArgs e)
        {
            await SetBindingAsync();
        }

        #endregion

        #region FileSystemWatcher

        private void CreateFileSystemWatcher()
        {
            fileSystemWatcher?.Dispose();

            if (string.IsNullOrWhiteSpace(Folder) || !Directory.Exists(Folder))
                return;

            fileSystemWatcher = new FileSystemWatcher(Folder, "*.json")
            {
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.CreationTime,
                IncludeSubdirectories = false,
                SynchronizingObject = this,
                EnableRaisingEvents = true
            };

            fileSystemWatcher.Created += OnFileChanged;
            fileSystemWatcher.Changed += OnFileChanged;
            fileSystemWatcher.Deleted += OnFileChanged;
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            // Cancel any previous pending event for the same file (debounce)
            if (_pendingFileEvents.TryGetValue(e.FullPath, out var previousCts))
            {
                previousCts.Cancel();
                previousCts.Dispose();
                _pendingFileEvents.Remove(e.FullPath);
            }

            var cts = new CancellationTokenSource();
            _pendingFileEvents[e.FullPath] = cts;

            // Capture event args before the async call
            var changeType = e.ChangeType;
            var fullPath = e.FullPath;
            var name = e.Name;

            _ = HandleFileEventAsync(changeType, fullPath, name, cts.Token);
        }

        private async Task HandleFileEventAsync(WatcherChangeTypes changeType, string fullPath, string? name, CancellationToken token)
        {
            try
            {
                // Debounce: wait before processing (resets if another event fires for the same file)
                await Task.Delay(_debounceDelay, token).ConfigureAwait(true);

                if (token.IsCancellationRequested)
                    return;

                switch (changeType)
                {
                    case WatcherChangeTypes.Created:
                    case WatcherChangeTypes.Changed:
                        // Wait until the file is fully written and readable
                        if (!await WaitForFileReadyAsync(fullPath, token))
                        {
                            Utilities.Logger.Warning("File never became readable: {Path}", fullPath);
                            return;
                        }

                        var existing = Jobs.FirstOrDefault(j => j.DataFileName.Equals(fullPath));
                        if (existing != null)
                        {
                            existing.CopyFrom(new JobObject(Folder, fullPath));
                            bs.EndEdit();
                            bs.ResetBindings(false);
                        }
                        else
                        {
                            Jobs.Add(new JobObject(Folder, name));
                            bs.ResetBindings(true);
                        }
                        break;

                    case WatcherChangeTypes.Deleted:
                        var toRemove = Jobs.FirstOrDefault(j => j.DataFileName.Equals(fullPath));
                        if (toRemove != null)
                        {
                            Jobs.Remove(toRemove);
                            bs.DataSource = new BindingList<JobObject>(Jobs);
                            bs.ResetBindings(true);
                        }
                        break;
                }

                lblJobCount.Text = Jobs.Count.ToString();
            }
            catch (OperationCanceledException)
            {
                // Debounce cancelled — a newer event superseded this one
            }
            catch (Exception ex)
            {
                Utilities.Logger.Error(ex, "Error handling file system event for {Path}", fullPath);
            }
            finally
            {
                _pendingFileEvents.Remove(fullPath);
            }
        }

        /// <summary>
        /// Polls until the file can be opened exclusively (meaning no other process is writing to it).
        /// Returns false if the file never becomes available within the timeout.
        /// </summary>
        private static async Task<bool> WaitForFileReadyAsync(string filePath, CancellationToken token, int maxAttempts = 20, int delayMs = 300)
        {
            for (int i = 0; i < maxAttempts; i++)
            {
                token.ThrowIfCancellationRequested();

                try
                {
                    // Try to open with no sharing — if another process is still writing, this throws
                    using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
                    return true;
                }
                catch (IOException)
                {
                    // File is still locked by the writer
                    await Task.Delay(delayMs, token).ConfigureAwait(true);
                }
                catch (UnauthorizedAccessException)
                {
                    await Task.Delay(delayMs, token).ConfigureAwait(true);
                }
            }

            return false;
        }

        #endregion

        #region Data Loading

        private void OpenLoadDialogAndLoadJobs()
        {
            using var openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".json",
                Filter = "Json files (*.json)|*.json|All files (*.*)|*.*",
                CheckFileExists = false,
                FileName = "Select any job file in the folder to load all jobs from that folder"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Folder = Path.GetDirectoryName(openFileDialog.FileName) ?? string.Empty;
                File.WriteAllText(CURRENT_FOLDER, Folder);
                Jobs = GetJobs(Folder);
                CreateFileSystemWatcher();
            }
        }

        private List<JobObject> GetJobs(string folder)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(folder) || !Directory.Exists(folder))
                    return new List<JobObject>();

                var files = Directory.GetFiles(folder, "*.html", SearchOption.TopDirectoryOnly)
                    .Concat(Directory.GetFiles(folder, "*.json", SearchOption.TopDirectoryOnly))
                    .ToList();

                return files
                    .Select(f => new JobObject(folder, f))
                    .OrderBy(j => j.DataFileName)
                    .ToList();
            }
            catch (Exception exp)
            {
                Utilities.Logger.Error(exp, "Error loading jobs from folder {Folder}", folder);
                return new List<JobObject>();
            }
        }

        private async Task SetBindingAsync()
        {
            Jobs = GetJobs(Folder);
            bs.DataSource = new BindingList<JobObject>(Jobs);
            bs.ResetBindings(true);

            lblJobCount.Text = Jobs.Count.ToString();
            lstJobs.DataSource = bs;
        }

        #endregion

        #region Navigation

        private void btnFirst_Click(object sender, EventArgs e) => bs.Position = 0;

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (bs.Position > 0)
                bs.Position -= 1;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (bs.Position < bs.Count - 1)
                bs.Position += 1;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (bs.Count > 0)
                bs.Position = bs.Count - 1;
        }

        #endregion

        #region Selection & Rendering

        private async void bs_CurrentChanged(object sender, EventArgs e)
        {
            Current = bs?.Current as JobObject;
            if (Current == null)
                return;

            txtStatus.Text = Current.DataFileName ?? string.Empty;

            await wv1.EnsureCoreWebView2Async();
            await wv2.EnsureCoreWebView2Async();

            if (!string.IsNullOrEmpty(Current.HTML))
            {
                wv1.NavigateToString(Current.HTML.Replace("\\n", ""));
                RenderAsync();
            }
        }

        private async void lstJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstJobs.SelectedIndex < 0)
                return;

            try
            {
                bs.Position = lstJobs.SelectedIndex;
                Current = bs.Current as JobObject;
                RenderAsync();

                UpdateCoverLetterButtonState();
            }
            catch (Exception exp)
            {
                Utilities.Logger.Error(exp, "Error during list selection change");
                return;
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

        private void UpdateCoverLetterButtonState()
        {
            if (Current == null)
                return;

            var directory = Current.DataFileName?.Replace(".json", "");
            if (!string.IsNullOrEmpty(directory) && Directory.Exists(directory))
            {
                var justname = new FileInfo(Current.DataFileName).Name;
                string documentFile = Path.Combine(directory, justname.Replace(".json", "_CoverLetter.docx"));
                btnOpenCoverLetter.Enabled = File.Exists(documentFile);
            }
            else
            {
                btnOpenCoverLetter.Enabled = false;
            }
        }

        #endregion

        #region Search

        private void txtSearch_TextChanged(object sender, EventArgs e) { }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter)
                return;

            var searchText = txtSearch.Text.Trim();
            if (searchText.Length > 0)
            {
                var filtered = Jobs.Where(j => j.HTML != null && j.HTML.Contains(searchText)).ToList();
                bs.DataSource = new BindingList<JobObject>(filtered);
                lstJobs.DataSource = new BindingList<JobObject>(filtered);
            }
            else
            {
                bs.DataSource = new BindingList<JobObject>(Jobs);
                lstJobs.DataSource = new BindingList<JobObject>(Jobs);
            }
        }

        #endregion

        #region Processing

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            if (Current == null)
                return;

            var jobCopy = Current.DeepClone();
            var task = ProcessJobAsync(jobCopy);

            // Track the task
            _processingTasks.Add(task);
            UpdateProcessingState();

            try
            {
                await task;
            }
            finally
            {
                _processingTasks.Remove(task);
                UpdateProcessingState();
            }
        }

        private async Task ProcessJobAsync(JobObject job)
        {
            try
            {
                using var aicall = new AICall();
                var response = await aicall.UploadAndProcess(Folder, job);

                if (response.Success && response.Result != null)
                {
                    var existing = Jobs.FirstOrDefault(j => j.id == response.Result.id);
                    existing?.CopyFrom(response.Result);
                    bs.EndEdit();
                    bs.ResetBindings(true);
                    RenderAsync();
                    existing.Save(Folder);
                }
            }
            catch (Exception exp)
            {

                Utilities.Logger.Error(exp);
            }
        }

        /// <summary>
        /// Updates the wait cursor and status bar based on how many tasks are still in flight.
        /// </summary>
        private void UpdateProcessingState()
        {
            // Remove completed/faulted tasks that may still be in the list
            _processingTasks.RemoveAll(t => t.IsCompleted);

            int remaining = _processingTasks.Count;

            if (remaining > 0)
            {
                UseWaitCursor = true;
                txtStatus.Text = $"Processing {remaining} job(s)...";

                // Reset and start the elapsed timer
                _timerStart = DateTime.UtcNow;
                txtTimer.Text = "00:00:00";
                if (!_elapsedTimer.Enabled)
                    _elapsedTimer.Start();
            }
            else
            {
                UseWaitCursor = false;
                _elapsedTimer.Stop();
                txtStatus.Text = $"All processing complete. ({txtTimer.Text})";
            }
        }

        private void ElapsedTimer_Tick(object? sender, EventArgs e)
        {
            var elapsed = DateTime.UtcNow - _timerStart;
            txtTimer.Text = elapsed.ToString(@"hh\:mm\:ss");
        }

        #endregion

        #region Open in Browser

        private void btnChrome_Click(object sender, EventArgs e) =>
            OpenInBrowser(Current?.JobPostUrl?.ToString());

        private void btnLnkCompany_Click(object sender, EventArgs e) =>
            OpenInBrowser(Current?.CompanyUrl?.ToString());

        private void btnLinkCoLinkedin_Click(object sender, EventArgs e) =>
            OpenInBrowser(Current?.CompanyLinkedIn?.ToString());

        private void bntJonpost_Click(object sender, EventArgs e) =>
            OpenInBrowser(Current?.JobPostUrl?.ToString());

        public void OpenChrome(string url) => OpenInBrowser(url);

        private static void OpenInBrowser(string? url)
        {
            if (!string.IsNullOrEmpty(url))
                Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }

        #endregion

        #region File Operations

        private void btnOpen_Click(object sender, EventArgs e)
        {
            var currentFile = Path.Combine(Folder, Current.DataFileName);
            if (!string.IsNullOrEmpty(Current?.DataFileName) && File.Exists(currentFile))
                Process.Start("explorer.exe", $"/select,\"{currentFile}\"");
        }

        private void btnExport_Click(object sender, EventArgs e) => ExportDataFile();
        private void btnSaveAll_Click(object sender, EventArgs e) => SaveDataFile();
        private void btnLoad_Click(object sender, EventArgs e) => LoadDataFile();
        private void btnLoadJobs_Click(object sender, EventArgs e) => OpenLoadDialogAndLoadJobs();
        private void btnRefreshFolder_Click(object sender, EventArgs e) => SetBindingAsync();

        #endregion

        #region Save & Database

        private void btnSaveJob_Click(object sender, EventArgs e)
        {
            bs.EndEdit();
            Current?.Save(Folder);
        }

        private void mnuSave_Click(object sender, EventArgs e) => Current?.Save(Folder);

        private void btnSaveToDb_Click(object sender, EventArgs e) => SaveToDB();
        private void saveToDBToolStripMenuItem_Click(object sender, EventArgs e) => SaveToDB();

        private void btnSaveAllJson_Click(object sender, EventArgs e)
        {
            using var saveFileDialog = new SaveFileDialog { Filter = "All JSON File (*.json)|*.json" };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var data = ((IEnumerable<JobObject>)bs.DataSource).ToList();
                File.WriteAllText(saveFileDialog.FileName, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            }
        }

        #endregion

        #region Database Toggle

        private void mnuJobsFromDb_Click(object sender, EventArgs e)
        {
            var allJobs = Repository.GetAllJobs();
            if (allJobs.Success)
            {
                DbJobs = allJobs.Result;
                Jobs = DbJobs;
                SetBindingAsync();
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
                    Jobs = DbJobs;
                    SetBindingAsync();
                }
            }
            else
            {
                btnTuggle.Text = "From Database";
                SetBindingAsync();
            }
        }

        #endregion

        #region Delete & Sort

        private void mnuDelete_Click(object sender, EventArgs e) => DeleteJob();
        private void btnDelete_Click(object sender, EventArgs e) => DeleteJob();
        private void deleteTSM_Click(object sender, EventArgs e) => DeleteJob();

        private void mnuAcc_Click(object sender, EventArgs e)
        {
            Jobs.Sort((a, b) => string.Compare(a.DataFileName, b.DataFileName, StringComparison.Ordinal));
            bs.ResetBindings(true);
            lstJobs.DataSource = bs;
        }

        private void mnuDec_Click(object sender, EventArgs e)
        {
            Jobs.Sort((a, b) => string.Compare(b.DataFileName, a.DataFileName, StringComparison.Ordinal));
            bs.ResetBindings(true);
            lstJobs.DataSource = bs;
        }

        #endregion

        #region Cover Letter

        public Response<JobFiles> CreateCoverLetter(JobObject job)
        {
            try
            {
                var directory = job.DataFileName.Replace(".json", "");
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);


                var justname = new FileInfo(job.DataFileName).Name;
                string coverletterfile = Path.Combine(directory, justname.Replace(".json", ".docx"));
                string resumefile = Path.Combine(directory, "Amirhossein Nassergivchi.docx");

                File.Copy("Coverletter.docx", coverletterfile);
                ReplaceText(coverletterfile, "COVERLETTER", job.Coverletter.Replace("\\n", "\\r\\n"));
                ReplaceText(coverletterfile, "DATE", DateTime.Now.ToString("MMM, dd yyyy"));
                ReplaceText(coverletterfile, "JOBTITLE", job.Title);
                ReplaceText(coverletterfile, "[Your Name]", "Amirhossein Nassergivchi");

                if (job.ProfessionalSummary != null)
                {
                    File.Copy("Resume.docx", resumefile);
                    ReplaceText(resumefile, "[PROFESSIONALSUMMARY]", job.ProfessionalSummary.Replace("\\n", "\\r\\n"));
                }

                return new Response<JobFiles>(new JobFiles { Coverletter = coverletterfile, Resume = resumefile });
            }
            catch (Exception ex)
            {
                Utilities.Logger.Error(ex, "Error creating cover letter");
                return new Response<JobFiles>(ex);
            }
        }

        public void ReplaceText(string filePath, string oldText, string newText)
        {
            using var doc = WordprocessingDocument.Open(filePath, true);
            var body = doc.MainDocumentPart.Document.Body;

            foreach (var text in body.Descendants<Text>())
            {
                if (text.Text.Contains(oldText))
                    text.Text = text.Text.Replace(oldText, newText);
            }

            doc.MainDocumentPart.Document.Save();
        }

        private void btnAttachCoverLetter_Click(object sender, EventArgs e)
        {
            if (Current == null)
                return;

            var result = CreateCoverLetter(Current);
            if (result.Success)
            {
                Process.Start(new ProcessStartInfo(result.Result.Coverletter) { UseShellExecute = true });
                Process.Start(new ProcessStartInfo(result.Result.Resume) { UseShellExecute = true });
            }
            else
                MessageBox.Show("Error creating cover letter: " + result.ErrorMessage);
        }

        private void btnOpenCoverLetter_Click(object sender, EventArgs e)
        {
            if (Current == null)
                return;

            var directory = Current.DataFileName.Replace(".json", "");
            if (!Directory.Exists(directory))
                return;

            var justname = new FileInfo(Current.DataFileName).Name;
            string documentFile = Path.Combine(directory, justname.Replace(".json", "_CoverLetter.docx"));
            Process.Start(new ProcessStartInfo(documentFile) { UseShellExecute = true });
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            if (Current == null)
                return;

            var directory = Current.DataFileName.Replace(".json", "");
            if (!Directory.Exists(directory))
                return;

            var justname = new FileInfo(Current.DataFileName).Name;
            string documentFile = Path.Combine(directory, justname.Replace(".json", "_CoverLetter.docx"));

            Current.CoverletterFile = File.ReadAllBytes(documentFile).ToBase64();
            bs.EndEdit();
            Current.Save(Folder);
        }

        #endregion

        #region Miscellaneous

        private void label14_Click(object sender, EventArgs e) =>
            Clipboard.SetText(Current?.id ?? string.Empty);

        private void label10_Click(object sender, EventArgs e)
        {
            if (Current == null || string.IsNullOrEmpty(Current.DataFileName))
                return;

            Current.CreatedAt = new FileInfo(Current.DataFileName).CreationTime;
            bs.EndEdit();
            Current.Save(Folder);
        }

        private void chkApplied_CheckedChanged(object sender, EventArgs e)
        {
            //if (Current != null)
            //{
            //    Current.AppliedAt = DateTime.Now;
            //    Current.Applied = true;
            //    bs.EndEdit();
            //    Current.Save();
            //}
        }

        private void btnExperience_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Current?.ProfessionalSummary))
            {
                Clipboard.SetText(Current.ProfessionalSummary);
                MessageBox.Show(Current.ProfessionalSummary);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {

            var result = CreateCoverLetter(Current);
            if (result.Success)
            {
                Process.Start(new ProcessStartInfo(result.Result.Coverletter) { UseShellExecute = true });
                Process.Start(new ProcessStartInfo(result.Result.Resume) { UseShellExecute = true });
            }
            else
                MessageBox.Show("Error creating cover letter: " + result.ErrorMessage);
        }
        private void btnOpenJson_Click(object sender, EventArgs e) { }
        private void btnSearch_Click(object sender, EventArgs e) { }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }

        #endregion

        private void SaveToDB()
        {
            bs.EndEdit();
            var currentJobs = ((JobObject)bs.Current);

            //currentJobs.AIResponse.matched = currentJobs.Matched;

            var response = Repository.SaveJobObject(currentJobs);

            if (response.Success)
            {
                try
                {
                    if (!JobsFromDb)
                    {

                        bs.Remove(Current);
                    }
                    else
                    {
                        //Template template = Template.Parse(File.ReadAllText("Template.html"));
                        //string result = template.Render(new { model = Current.AIResponse });
                        //wv2.NavigateToString(result);
                    }

                    lblJobCount.Text = Jobs.Count.ToString();
                    bs.EndEdit();
                }
                catch (Exception exp)
                {
                    Utilities.Logger.Error(exp);
                }
            }
            else
            {
                Utilities.Logger.Error(response.Exception);
                //MessageBox.Show($"Failed to save job `{currentJobs.Shortname}` to database. Please check the log for details.");
            }
        }

        private void LoadDataFile()
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

        private void SaveDataFile()
        {
            bs.EndEdit();
            var datatosave = ((IEnumerable<JobObject>)bs.DataSource).ToList();

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

        private void ExportDataFile()
        {
            try
            {
                if (Current != null)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog() { DefaultExt = ".data", FileName = Current?.DataFileName.Replace(".html", ".data"), Filter = "All data files (*.data)|*.data|All files (*.*)|*.*" };
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

        private async Task RenderAsync()
        {
            try
            {
                var templateText = File.ReadAllText("Template.html");
                var template = Template.Parse(templateText);

                var context = new TemplateContext
                {
                    // IMPORTANT: keep C# property names EXACTLY as they are
                    MemberRenamer = member => member.Name
                };

                var script = new ScriptObject();
                script.Import(new { model = Current });
                context.PushGlobal(script);

                string result = template.Render(context);
                wv2.NavigateToString(result);
            }
            catch (Exception exp)
            {
                Utilities.Logger.Error(exp, "Error rendering template");
            }
        }

        private void DeleteJob()
        {
            if (Current != null && !JobsFromDb)
            {
                var result = MessageBox.Show($"Are you sure you want to delete job `{Current.DataFileName}`?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var fileToDelet = Path.Combine(Folder, Current?.DataFileName);
                        Jobs.Remove(Current);
                        bs.ResetBindings(true);
                        //lstJobs.Items.RemoveAt(bs.Position);
                        lblJobCount.Text = $"Jobs ({Jobs.Count})";
                        lblJobCount.Text = Jobs.Count.ToString();
                        if (!string.IsNullOrEmpty(fileToDelet) && File.Exists(fileToDelet))
                            File.Delete(fileToDelet);
                    }
                    catch (Exception exp)
                    {
                        Utilities.Logger.Error(exp);
                    }
                }
            }
            else
            {
                try
                {
                    var result = Repository.DeleteJob(Current.id);
                    if (result.Success)
                    {
                        bs.Remove(Current);

                        bs.EndEdit();
                        lblJobCount.Text = Jobs.Count.ToString();
                        return;
                    }
                    throw new Exception($"Failed to delete job `{Current.DataFileName}` from database. Please check the log for details.");
                }
                catch (Exception exp)
                {
                    Utilities.Logger.Error(exp);
                }
            }
        }
    }
}
