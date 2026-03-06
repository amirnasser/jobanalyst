using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MessagePack;
using Newtonsoft.Json;

namespace JobAnalyzer.BLL;

public enum JobType
{
    None,
    Fulltime,
    Parttime,
    Contract
}

public enum JobLocation
{
    None,
    Remote,
    Hybrid,
    InOffice
}

[MessagePackObject]
public class JobObject
{
    public JobObject(string htmlFileName)
    {
        if (File.Exists(htmlFileName))
        {
            this.HTMLFileName = htmlFileName;
            this.HtmlJobPostFileContent = File.Exists(HTMLFileName) ? File.ReadAllBytes(HTMLFileName) : null;            
            this.DataFileContent = !string.IsNullOrEmpty(JsonFileName) && File.Exists(JsonFileName) ? File.ReadAllBytes(JsonFileName) : null;
            this.HTML = Encoding.UTF8.GetString(HtmlJobPostFileContent).Replace("\\n", "");
            this.JobPostUrl = !string.IsNullOrEmpty(Utilities.FindJobPostUrl(HTML)) ? new Uri(Utilities.FindJobPostUrl(HTML)) : null;

            if (DataFileContent != null)
            {
                try
                {
                    string json = ASCIIEncoding.UTF8.GetString(this.DataFileContent);
                    if (!string.IsNullOrEmpty(json) && !json.Equals("null"))
                    {
                        this.AIResponse = JsonConvert.DeserializeObject<AIResponse>(json);
                        this.Reason = AIResponse.reason;
                        this.JobRequirements = JsonConvert.SerializeObject(AIResponse.job_requirements);
                        this.MissingRequirements = JsonConvert.SerializeObject(AIResponse.missing_requirements);
                        this.CompanyName = AIResponse.company;
                        this.Coverletter = AIResponse.coverletter;
                    }
                }
                catch (Exception exp)
                {
                    Utilities.Logger.Error(exp.Message);
                }
            }
        }
    }

    public JobObject(string rootPath, JobFile jobfile)
    {
        this.HtmlJobPostFileContent = Encoding.UTF8.GetBytes(jobfile.Html);
        this.HTMLFileName = Path.Combine(rootPath, $"{jobfile.timestamp}.html");
        this.JobPostUrl = new Uri(jobfile.Url);
        this.DataFileContent = !string.IsNullOrEmpty(JsonFileName) && File.Exists(JsonFileName) ? File.ReadAllBytes(JsonFileName) : null;
        this.HTML = jobfile.Html.HtmlCleanup();

        if (DataFileContent != null)
        {
            try
            {
                string json = ASCIIEncoding.UTF8.GetString(this.DataFileContent);
                if (!string.IsNullOrEmpty(json) && !json.Equals("null"))
                {
                    this.AIResponse = JsonConvert.DeserializeObject<AIResponse>(json);
                    this.Reason = AIResponse.reason;
                    this.JobRequirements = JsonConvert.SerializeObject(AIResponse.job_requirements);
                    this.MissingRequirements = JsonConvert.SerializeObject(AIResponse.missing_requirements);
                    this.CompanyName = AIResponse.company;
                    this.Coverletter = AIResponse.coverletter;
                }
            }
            catch (Exception exp)
            {
                Utilities.Logger.Error(exp.Message);
            }
        }
    }

    public JobObject()
    {

    }

    public void ExportDataFile(string filename)
    {
        try
        {
            byte[] bytes = MessagePackSerializer.Serialize(this);
            File.WriteAllBytes(filename, bytes);
        }
        catch (Exception exp)
        {

            throw;
        }
    }

    public JobObject ImportDataFile(string filename)
    {
        byte[] bytes = File.ReadAllBytes(filename);
        return MessagePackSerializer.Deserialize<JobObject>(bytes);
    }

    public override string ToString()
    {
        return HTMLFileName.Replace(".html", "").Replace(".job.json", ".job");
    }

    [Key(0)]
    public string id { get; set; }
    [Column(name: "company", TypeName = "varchar(50)")]
    [Key(1)]
    public string? CompanyName { get; set; }
    [Key(2)]
    public Uri? CompanyUrl { get; set; }
    [Key(3)]
    public Uri? CompanyLinkedIn { get; set; }
    [Key(4)]
    public string? JobPost { get; set; }
    [Key(5)]
    public AIResponse AIResponse { get; set; }
    [Key(6)]
    public Uri? JobPostUrl { get; set; } //=> new Uri(Utilities.FindJobPostUrl(HTML));
    [Key(7)]
    public string? Coverletter { get; set; }
    [Key(8)]
    public string? MissingRequirements { get; set; }
    [Key(9)]
    public string? Reason { get; set; }
    [Key(10)]
    public bool Matched { get; set; } = false;
    [Key(11)]
    public string? JobRequirements { get; set; }
    [Key(12)]
    public string? JobTitle => AIResponse?.jobtitle;
    [Key(22)]
    public bool Applied { get; set; } = false;
    [Key(23)]
    public byte[]? HtmlJobPostFileContent { get; set; }
    [Key(24)]
    public byte[]? DataFileContent { get; set; }
    [Key(29)]
    public byte[]? TextFileContent => string.IsNullOrEmpty(Text) ? null : Encoding.UTF8.GetBytes(Text);
    [Key(25)]
    public JobType? JobType { get; set; }
    [Key(26)]
    public JobLocation? JobLocation { get; set; }
    [Key(13)]
    public string? SalaryRange { get; set; } = null;

    [Key(14)]
    public string? When { get; set; } = null;
    [Key(15)]
    public string? Where { get; set; } = null;


    [JsonIgnore]
    [Key(17)]
    public string Shortname => HTMLFileName.ToShortname();
    [IgnoreMember]
    public string HTML { get; set; }
    [IgnoreMember]
    public string Text { get; set; }//=> HTML.GetText();
    [Key(16)]
    public string? HTMLFileName { get; set; }
    [Key(18)]
    public string JsonFileName => HTMLFileName?.Replace(".html", ".json");
    [Key(19)]
    public string TextFileName => File.Exists(HTMLFileName.SetFileExtension(".txt")) ? HTMLFileName?.SetFileExtension(".txt") : null ;

    public static Response SaveAllToFile(string filename, List<JobObject> jobs)
    {
        try
        {
            jobs = jobs.Where(job=>job.JobPostUrl != null).ToList();
            //Before saving set HtmlFileName, DataFieName, TextFilename, Text
            jobs.ForEach(static job =>
            {
                try
                {
                    job.id = job.JobPostUrl != null ? job.JobPostUrl.ToString().ToMd5Hash() : Guid.NewGuid().ToString();
                    job.HTMLFileName = job.AIResponse != null  && !string.IsNullOrEmpty(job.AIResponse?.company) && !string.IsNullOrEmpty(job.AIResponse?.jobtitle) ? $"{job.CompanyName}_{job.JobTitle.Replace(" ", "")}" : job.HTMLFileName ?? $"{job.Shortname}.html";
                    job.DataFileContent = !string.IsNullOrEmpty(job.JsonFileName) && File.Exists(job.JsonFileName) ? File.ReadAllBytes(job.JsonFileName) : null;
                    job.Text = string.Empty;




                }
                catch (Exception exp)
                {
                    Utilities.Logger.Error(exp.Message);
                }
            });

            byte[] bytes = MessagePackSerializer.Serialize(jobs);
            File.WriteAllBytes(filename, bytes);
            return new Response();
        }
        catch (Exception exp)
        {
            Utilities.Logger.Error(exp.Message);
            return new Response(exp);
        }
    }

    public static Response<List<JobObject>> LoadAllFromFile(string filename)
    {
        try
        {
            List <JobObject> jobs = MessagePackSerializer.Deserialize<List<JobObject>>(File.ReadAllBytes(filename));
            //foreach (var job in jobs)
            //{
            //    try
            //    {
            //        job.HTMLFileName = job.AIResponse == null ? $"{job.CompanyName}_{job.JobTitle.Replace(" ", "")}" : job.HTMLFileName ?? $"{job.Shortname}.html";
            //    }
            //    catch (Exception exp)
            //    {

            //    }
            //}
            return new Response<List<JobObject>>(jobs);
        }
        catch (Exception exp)
        {
            Utilities.Logger.Error(exp.Message);
            return new Response<List<JobObject>>(exp);
        }
    }
}
