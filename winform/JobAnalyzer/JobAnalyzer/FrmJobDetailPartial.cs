using System.ComponentModel;
using JobAnalyzer.BLL;
using JobAnalyzer.Data;
using Scriban;
using Scriban.Runtime;

namespace JobAnalyzer;

public partial class FrmJobDetail
{
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
        //foreach (var job in datatosave)
        //{
        //    job.Text = job.HTML.GetText();
        //    job.HTMLFileName = new FileInfo(job.HTMLFileName).Name;
        //}

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
            var result = MessageBox.Show($"Are you sure you want to delete job `{Current.HTMLFileName}`?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    bs.Remove(Current);
                    lstJobs.Items.RemoveAt(bs.Position);
                    lblJobCount.Text = $"Jobs ({Jobs.Count})";
                    bs.EndEdit();
                    lblJobCount.Text = Jobs.Count.ToString();
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
                throw new Exception($"Failed to delete job `{Current.HTMLFileName}` from database. Please check the log for details.");
            }
            catch (Exception exp)
            {
                Utilities.Logger.Error(exp);
            }
        }
    }
}
