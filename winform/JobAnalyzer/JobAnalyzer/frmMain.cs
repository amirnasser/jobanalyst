using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using JobAnalyzer.BLL;
using JobAnalyzer.Data;
using MahApps.Metro.IconPacks;
using MessagePack;

//using MahApps.Metro.IconPacks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Scriban;

namespace JobAnalyzer
{
    public partial class frmMain : Form
    {
        private static readonly Serilog.ILogger _logger;
        static frmMain()
        {
            try
            {
                // Initialize Serilog to write rolling daily files under a "logs" folder in the application base directory.
                var logsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                Directory.CreateDirectory(logsFolder);

                _logger = Utilities.Logger;
                _logger.Information("Logger initialized for frmMain.");
            }
            catch (Exception initEx)
            {
                // Avoid throwing from static ctor; swallow and continue (app will still run without file logging).
                try
                {
                    // If logger init failed, write to Debug output at least.
                    Debug.WriteLine($"Failed to initialize logger: {initEx}");
                }
                catch { }
            }
        }
        public JobObject CurrentJob { get; set; } = new JobObject();
        public List<JobObject> JobList = new List<JobObject>();
        private List<ListboxItem> Files = new List<ListboxItem>();
        private string filespath = "";
        private string tmpFilename;
        private ModelResponse modelResponse;
        AIResponse aiResponse;
        public string Html = "";

        OpenFileDialog openFileDialog = new OpenFileDialog()
        {
            CheckFileExists = false,
            FileName = "Select a file or folder",
        };

        public frmMain()
        {
            InitializeComponent();
            _logger?.Debug("frmMain instance constructed.");
        }

        private void OpenFolder()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = new FileInfo(openFileDialog.FileName).Directory.FullName;

                if (Directory.Exists(path))
                {
                    LoadFilesFromDirectory(path);
                    File.WriteAllText("current_folder.txt", path);

                    _logger?.Information("Opened folder: {Path}", path);
                }
                else
                {
                    MessageBox.Show("The selected path is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _logger?.Warning("OpenFolder: selected path is invalid: {Path}", path);
                }

            }
        }

        private void RefreshFiles()
        {
            LoadFilesFromDirectory(filespath);
            _logger?.Debug("RefreshFiles called for path {Path}", filespath);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFolder();
        }

        private async void MonitorFolder(string path)
        {
            FileSystemWatcher watcher = new FileSystemWatcher(path, "*.*");
            
            watcher.Created += (s, e) =>
            {
                if (e.FullPath.EndsWith(".html", StringComparison.OrdinalIgnoreCase) ||
                    e.FullPath.EndsWith(".job.json", StringComparison.OrdinalIgnoreCase))
                {

                    Thread.Sleep(10000);

                    Utilities.Logger.Information($"File `{e.FullPath}` downloaded and start procecessing... ");
                    JobObject job = new JobObject(e.FullPath);
                    using (AICall aicall = new AICall())
                    {
                        aicall.GetResponseAsync(job);
                        Utilities.Logger.Information($"Processing of file `{e.FullPath}` done. ");
                    }
                }
            };
            watcher.EnableRaisingEvents = true;
            _logger?.Information("Started monitoring folder: {Path}", path);
        }

        private BLL.Response<List<JobObject>> LoadFilesFromDirectory(string directoryPath)
        {
            try
            {
                filespath = directoryPath;
                MonitorFolder(directoryPath);
                var files = Directory.GetFiles(directoryPath, "*.html", SearchOption.TopDirectoryOnly).
                    Concat(Directory.GetFiles(directoryPath, "*.job.json")).ToArray();

                List<ListboxItem> fileList = new List<ListboxItem>();

                List<FileInfo> list = files.Select(l => new FileInfo(l)).OrderByDescending(f => f.Name).ToList();
                var jobs = files.Select(f => new JobObject(f)).ToList();
                //foreach (FileInfo file in list)
                return new BLL.Response<List<JobObject>>(jobs);
                //{
                //    lstJobPosts.Items.Add(new ListboxItem(file));
                //}
                //txtStatus.Text = directoryPath;
                //_logger?.Information("Loaded {Count} html files from {Path}", list.Count, directoryPath);
                //Files = lstJobPosts.Items.Cast<ListboxItem>().ToList();
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error loading files: {exp.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger?.Error(exp, "Error loading files from directory {Path}", directoryPath);
                return new Response<List<JobObject>>(exp);
            }
        }

        private async void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstJobPosts.SelectedItem != null)
            {
                var job = lstJobPosts.SelectedItem as JobObject;
                //if (job != null && job.File.FullName.EndsWith(".job.json"))
                //{
                //    var JobFile = JsonConvert.DeserializeObject<BLL.JobFile>(File.ReadAllText(job.File.FullName));
                //    this.Html = JobFile.Html.HtmlCleanup();
                //}
                //else
                //{
                //    this.Html = File.ReadAllText(job.File.FullName);
                //}
                //var textFile = job.HTML;
                //if (!File.Exists(textFile))
                //{
                //    File.WriteAllText(textFile, this.Html.GetText());
                //}

                //if (string.IsNullOrEmpty(this.Html))
                //{
                //    wv1.NavigateToString("<html><body><h1>No content to display</h1></body></html>");
                //    //_logger?.Debug("Selected file {File} contained no HTML content.", job.File.FullName);
                //}
                //else
                //{
                    try
                    {
                        wv1.NavigateToString(job.HTML);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error displaying HTML content: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //_logger?.Error(ex, "Error rendering HTML content for file {File}", job.File.FullName);
                    }
                //}

                renderJobResultDetail(job);


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

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            createCoverLetter();
        }

        private async void createCoverLetter()
        {
            try
            {
                tmpFilename = Path.GetTempFileName() + ".docx";
                File.Copy("Coverletter.docx", tmpFilename);
                ReplaceText(tmpFilename, "COVERLETTER", aiResponse.coverletter.Replace("\\n", "\\r\\n"));
                ReplaceText(tmpFilename, "DATE", DateTime.Now.ToString("MMM, dd yyyy"));
                ReplaceText(tmpFilename, "JOBTITLE", aiResponse.jobtitle);
                Process.Start(new ProcessStartInfo(tmpFilename) { UseShellExecute = true });
                _logger?.Information("Created cover letter: {TempFile}", tmpFilename);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error parsing response: " + ex.Message);
                _logger?.Error(ex, "Error creating cover letter");
            }
        }

        private async void frm1_Load(object sender, EventArgs e)
        {
            try
            {

                await wv1.EnsureCoreWebView2Async();
                await wv2.EnsureCoreWebView2Async();

                if (File.Exists("current_folder.txt"))
                {
                    Response<List<JobObject>> jobs = LoadFilesFromDirectory(File.ReadAllText("current_folder.txt"));
                    if(jobs.Success)
                        bs.DataSource = new BindingList<JobObject>(jobs.Result);

                    lstJobPosts.DataSource = bs;
                    lstJobPosts.DisplayMember = "FileName";
                    
                }
                else 
                { 
                    OpenFolder(); 
                }


                //GetIconBitmap(PackIconMaterialDesignKind.ImportExportSharp, 16).Save("import.png", ImageFormat.Png);

                //var response = await RestCall.GetVersion();
                //txtStatus.Text = $"API Version: {response.version}";
                //_logger?.Information("API Version retrieved: {Version}", response.version);



                //btnOpen.Image = GetIconBitmap(PackIconMaterialDesignKind.FolderOpenOutline, 16);
                //btnCoverLetter.Image = GetIconBitmap(PackIconMaterialDesignKind.Newspaper, 16);
                //btnProcess.Image = GetIconBitmap(PackIconMaterialDesignKind.PlayArrow, 16);


                //if (cmbModels.Items.Contains("gpt-oss:20b"))
                //{
                //    cmbModels.SelectedItem = "gpt-oss:20b";
                //    _logger?.Debug("Default model gpt-oss:20b selected.");
                //}


            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "Error in frm1_Load");
                MessageBox.Show("Error initializing UI: " + ex.Message);
            }
        }

        public static Bitmap GetIconBitmap(PackIconMaterialDesignKind kind, int size = 24)
        {
            var icon = new PackIconMaterialDesign
            {
                Kind = kind,
                Width = size,
                Height = size,
                Foreground = new SolidColorBrush(Colors.Black)
            };

            // Wrap icon in a container with a background
            var container = new System.Windows.Controls.Grid
            {
                Width = size,
                Height = size,
                Background = new SolidColorBrush(Colors.Transparent) // important
            };

            container.Children.Add(icon);

            // Force layout
            container.Measure(new System.Windows.Size(size, size));
            container.Arrange(new System.Windows.Rect(0, 0, size, size));
            container.UpdateLayout();

            // Render
            var dpi = 96d;
            var rtb = new RenderTargetBitmap(
                size, size,
                dpi, dpi,
                PixelFormats.Pbgra32);

            rtb.Render(container);

            // Encode to PNG
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                return new Bitmap(ms);
            }
        }

        private async Task GetModelsAsync()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = httpClient.GetAsync("http://localhost:6000/models").Result;
                    modelResponse = JsonConvert.DeserializeObject<ModelResponse>(response.Content.ReadAsStringAsync().Result);
                    _logger?.Information("Fetched models from local endpoint.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching models: " + ex.Message);
                _logger?.Error(ex, "Error fetching models");
            }
        }

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            processDataVisually();
        }

        private async void processDataVisually()
        {
            if (lstJobPosts.SelectedItem != null)
            {
                //this.Enabled = false;
                txtStatus.Text = "Please wait, Processing";
                this.UseWaitCursor = true;
                var file = lstJobPosts.SelectedItem as ListboxItem;

                JobObject job = (JobObject)bs.Current;
                //if (file != null)
                //{
                //    if (file.File.Name.EndsWith(".html"))
                //    {
                //        job = new JobObject(file.File.FullName);
                //    }
                //    else if (file.File.Name.EndsWith(".job.json"))
                //    {
                //        JobFile jobFile = JsonConvert.DeserializeObject<JobFile>(File.ReadAllText(file.File.FullName));
                //        job = new JobObject(filespath, jobFile);
                //    }
                //    else
                //    {
                //        job = new JobObject();
                //    }

                //    using (AICall aicall = new AICall())
                //    {
                //        var response = await aicall.GetResponseAsync(job);
                //        if (response != null)
                //        {
                //            Render(response);
                //        }
                //    }
                //}
                Render(job.AIResponse);
                txtStatus.Text = $"Data file written.";
                this.UseWaitCursor = false;
                //this.Enabled = true;
                //var response = await ProcessJobPostAsync((lstJobPosts.SelectedItem as ListboxItem).File.FullName);

                //if (response != null)
                //{
                //    Render(response);

                //    txtStatus.Text = $"Data file written.";
                //    this.UseWaitCursor = false;
                //    this.Enabled = true;
                //    _logger?.Information("Processed file and rendered results.");
                //}
            }
        }

        private string Render(AIResponse aiResponse)
        {
            try
            {
                Template template = Template.Parse(File.ReadAllText("Template.html"));
                string result = template.Render(new { model = aiResponse });

                wv2.NavigateToString(result);
                return result;
            }
            catch (Exception exp)
            {
                _logger?.Error(exp, "Error rendering template");
                //throw exp;
                return exp.Message;
            }
        }

        private string RenderJson(string json)
        {
            try
            {
                Template template = Template.Parse($"<html><body><pre>{json}</pre></body></html>");
                string result = template.Render(json);

                wv2.NavigateToString(result);
                return result;
            }
            catch (Exception exp)
            {
                _logger?.Error(exp, "Error rendering template");
                throw exp;
                return exp.Message;
            }
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {

        }

        public string PrettyJson(string json)
        {
            try
            {
                JToken parsed = JToken.Parse(json);
                return parsed.ToString(Formatting.Indented);
            }
            catch
            {
                return json; // return original if invalid JSON
            }
        }

        private void renderJobResultDetail(JobObject job)
        {
            Render(job.AIResponse);
            //if (lstJobPosts.SelectedItem != null)
            //{
            //    if (File.Exists(jsonfile))
            //    {
            //        var json = File.ReadAllText(jsonfile);
            //        rchJson.Text = json;

            //        try
            //        {
            //            aiResponse = JsonConvert.DeserializeObject<AIResponse>(json);
            //            var dir = new FileInfo(jsonfile).Name.Replace(".json", "");
            //            if (Directory.Exists(Path.Combine(filespath, dir)))
            //            {
            //                aiResponse.applied = true;
            //            }
            //            Render(aiResponse);

            //            if (string.IsNullOrEmpty(aiResponse.reason) &&
            //                string.IsNullOrEmpty(aiResponse.coverletter))
            //            {
            //                aiResponse.pre = this.PrettyJson(json);
            //                Render(aiResponse);
            //            }

            //        }
            //        catch (Exception exp)
            //        {

            //        }

            //        _logger?.Information("Rendered job result detail from {JsonFile}", jsonfile);
            //    }
            //    else
            //    {
            //        rchJson.Text = "";
            //        Template template = Template.Parse("<html></html>");
            //        var result = template.Render(new { });
            //        wv2.NavigateToString(result);
            //        _logger?.Debug("No JSON file found for {JsonFile}", jsonfile);
            //    }
            //}
        }

        private async void btnProcessFile_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshFiles();
        }

        private async Task<AIResponse?> ProcessJobPostAsync(string filename)
        {
            try
            {
                var response = await RestCall.Analyze(filename);
                var dataFile = filename.Replace(".html", ".json");
                if (response != null)
                {
                    File.WriteAllText(dataFile, JsonConvert.SerializeObject(response));
                    _logger?.Information("Wrote data file {DataFile}", dataFile);
                }
                return response;
            }
            catch (Exception exp)
            {
                _logger?.Error(exp.Message);
                _logger?.Error(exp.StackTrace);
                _logger?.Error(exp, "Error processing job post {Filename}", filename);
                return null;
            }
        }

        private void bntCreateFolder_Click(object sender, EventArgs e)
        {
            if (lstJobPosts.SelectedItem != null)
            {
                JobObject current = bs.Current as JobObject;

                var newfolder = Path.Combine(filespath, lstJobPosts.SelectedItem.ToString());

                if (!Directory.Exists(newfolder))
                {
                    Directory.CreateDirectory(newfolder);
                }

                var jsonfile = Path.Combine(filespath, lstJobPosts.SelectedItem.ToString().Replace(".html", ".json"));
                if (File.Exists(jsonfile))
                {
                    //File.Copy(jsonfile, Path.Combine(newfolder, lstJobPosts.SelectedItem.ToString().Replace(".html", ".json")), true);
                    //File.Copy(Path.Combine(filespath, lstJobPosts.SelectedItem.ToString()), Path.Combine(newfolder, lstJobPosts.SelectedItem.ToString()), true);
                    File.WriteAllText(Path.Combine(newfolder, $"{new DirectoryInfo(newfolder).Name}_processed.html"), Render(current.AIResponse));
                    _logger?.Information("Created folder {Folder} and copied processed files.", newfolder);
                }

                Clipboard.SetText(newfolder);

                Process.Start("explorer.exe", newfolder);
            }
        }

        private void delete()
        {
            if (lstJobPosts.SelectedItem != null)
            {
                File.Delete((lstJobPosts.SelectedItem as ListboxItem).File.FullName);
                File.Delete((lstJobPosts.SelectedItem as ListboxItem).File.FullName.Replace(".html", ".json"));
                if (Directory.Exists(Path.Combine(filespath, (lstJobPosts.SelectedItem as ListboxItem).ToString())))
                {
                    Directory.Delete(Path.Combine(filespath, (lstJobPosts.SelectedItem as ListboxItem).ToString()));
                }
                RefreshFiles();
                _logger?.Information("Deleted selected job post and associated files.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void btnOpenInBrowser_Click(object sender, EventArgs e)
        {
            OpenInBrowser();
        }

        private void OpenInBrowser()
        {
            if (lstJobPosts.SelectedItem != null)
            {
                var url = FindJobPostUrl(lstJobPosts.SelectedItem as ListboxItem);
                Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
                _logger?.Information("Opening job post url in browser: {Url}", url);
            }
        }

        private string FindJobPostUrl(ListboxItem item)
        {
            var html = File.ReadAllText(item.File.FullName);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            return doc.DocumentNode.SelectSingleNode("//body/p/a").Attributes[0].Value;
        }

        private void mnuProcess_Click(object sender, EventArgs e)
        {
            processDataVisually();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void lstJobPosts_DoubleClick(object sender, EventArgs e)
        {
            processDataVisually();
        }

        List<string> exp;

        private async void btnProcessAll_Click(object sender, EventArgs e)
        {
            if (lstJobPosts.Items.Count >= 0)
            {

                List<FileInfo> files = Directory.GetFiles(filespath, "*.html", SearchOption.TopDirectoryOnly).Select(f => new FileInfo(f)).ToList();
                List<FileInfo> processed = Directory.GetFiles(filespath, "*.json", SearchOption.TopDirectoryOnly).Select(f => new FileInfo(f)).ToList();


                if (files.Count > processed.Count)
                    exp = files
                        .Select(f => new { name = f.Name.Replace(".html", "") })
                        .Except(processed.Select(p => new { name = p.Name.Replace(".json", "") }))
                        .Select(l => $"{l.name}.html")
                        .ToList();
                else
                    exp = files.Select(f => f.Name).ToList();


                txtStatus.Text = $"File processing: " + string.Join(",", exp.ToArray());

                //Parallel.ForEach(exp, async e =>
                //{
                //    try
                //    {
                //        using (AICall aicall = new AICall())
                //        {
                //            await new AICall().GetResponseAsync(Path.Combine(filespath, e));
                //        }

                //    }
                //    catch (Exception exp)
                //    {
                //        _logger.Error(exp.Message);
                //    }
                //});

                foreach (var file in exp)
                {
                    try
                    {
                        using (AICall aicall = new AICall())
                        {
                            await new AICall().GetResponseAsync(Path.Combine(filespath, file));
                        }
                    }
                    catch (Exception exp)
                    {
                        _logger.Error(exp.Message);
                    }
                }
                _logger?.Information("ProcessAll completed for {Count} items", exp?.Count ?? 0);
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (lstJobPosts.SelectedItem != null)
            {
                CheckAndInsertData(lstJobPosts.SelectedItem as ListboxItem);
            }
        }

        private void CheckAndInsertData(ListboxItem item)
        {
            var url = FindJobPostUrl(item);
            var job = Repository.FindJobsByUrl(new Uri(url).AbsolutePath);
            if (job == null)
            {
                // there is no job
                _logger?.Debug("No job found in repository for URL {Url}", url);
            }
            else
            {
                _logger?.Debug("Job found in repository for URL {Url}", url);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void lsvData_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void bgw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int i = 0;

            foreach (string file in exp)
            {
                var item = await ProcessJobPostAsync(Path.Combine(filespath, file));
            }
            _logger?.Information("Background worker completed processing of {Count} files", exp?.Count ?? 0);
        }

        private void bgw_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (progress.Value < progress.Maximum)
                progress.Value += 1;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim().Length > 0)
            {
                lstJobPosts.Items.Clear();
                foreach (var item in Files.Where(f => f.File.Name.Contains(txtSearch.Text)))
                {
                    lstJobPosts.Items.Add(item);
                }
            }
            else
            {
                lstJobPosts.Items.Clear();
                foreach (var item in Files)
                {
                    lstJobPosts.Items.Add(item);
                }
            }
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            _logger?.Information("Saved current JobObject to repository.");
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (lstJobPosts.SelectedItems.Count > 0)
            {
                JobObject job = new JobObject(((ListboxItem)lstJobPosts.SelectedItem).File.FullName);
                SaveFileDialog saveFileDialog = new SaveFileDialog() { DefaultExt = ".data", FileName = lstJobPosts.SelectedItem.ToString(), Filter = "All data files (*.data)|*.data|All files (*.*)|*.*" };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    JobObject jobj = new JobObject(((ListboxItem)lstJobPosts.SelectedItem).File.FullName);
                    jobj.ExportDataFile(saveFileDialog.FileName);
                }
            }
        }

        private void btnShowJob_Click(object sender, EventArgs e)
        {
            FrmJobDetail frmJobDetail = new FrmJobDetail(filespath);
            frmJobDetail.Show();
        }

        private void btnLoadJob_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { DefaultExt = ".data", Filter = "All data files (*.data)|*.data|All files (*.*)|*.*" };
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                byte[] bytes = File.ReadAllBytes(openFileDialog.FileName);
                var job = MessagePackSerializer.Deserialize<JobObject>(bytes);
                FrmSingleJob frm = new FrmSingleJob(job) { Text = openFileDialog.FileName };
                frm.Show();
            }
        }

        public JobObject ImportDataFile(string filename)
        {
            byte[] bytes = File.ReadAllBytes(filename);
            return MessagePackSerializer.Deserialize<JobObject>(bytes);
        }

        private void btnHtmlCleanUp_Click(object sender, EventArgs e)
        {
            var htmlfile = (lstJobPosts.SelectedItem as ListboxItem).File.FullName;
            var html = File.ReadAllText(htmlfile);
            HtmlCleanerAndBeautifier cleaner = new HtmlCleanerAndBeautifier();
            var cleaned = cleaner.CleanupAndBeautifyAsync(html).Result;
        }
    }
}

