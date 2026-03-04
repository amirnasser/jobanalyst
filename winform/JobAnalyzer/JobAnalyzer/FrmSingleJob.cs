using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessagePack;
using Scriban;
using JobAnalyzer.BLL;

namespace JobAnalyzer
{
    public partial class FrmSingleJob : Form
    {
        public JobObject Job { get; set; }
        public FrmSingleJob(JobObject job)
        {
            InitializeComponent();
            Job = job;
        }

        private async void FrmSingleJob_Load(object sender, EventArgs e)
        {
            List<JobObject> SingleJob = new List<JobObject>();
            SingleJob.Add(Job);
            bs.DataSource = new BindingList<JobObject>(SingleJob);

            await wv1.EnsureCoreWebView2Async();
            await wv2.EnsureCoreWebView2Async();
            wv1.NavigateToString(Job.HTML?.Replace("\\n", ""));
            RenderAsync();
        }
        private async Task RenderAsync()
        {
            try
            {
                Template template = Template.Parse(File.ReadAllText("Template.html"));
                string result = template.Render(new { model = Job.AIResponse });
                wv2.NavigateToString(result);
            }
            catch (Exception exp)
            {
                Utilities.Logger.Error(exp, "Error rendering template");
            }
        }

    }
}
