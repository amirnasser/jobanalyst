using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JobAnalyzer.BLL;


[MessagePackObject]
public class JobObject
{
    public static Response<JobObject> LoadFromFile(string fullName)
    {
        try
        {
            if (File.Exists(fullName))
            {
                var fileContent = File.ReadAllText(fullName);
                JobObject job = JsonConvert.DeserializeObject<JobObject>(fileContent);
                return new Response<JobObject>(job);
            }
            throw new FileNotFoundException(fullName);
        }
        catch (Exception exp)
        {
            Utilities.Logger.Error(exp);
            return new Response<JobObject>(exp);
        }
    }
    public JobObject(string file)
    {
        try
        {
            FileInfo fileinfo = new FileInfo(file);

            if (File.Exists(file) && file.EndsWith(".json"))
            {
                var fileContent = File.ReadAllText(file);
                var job = JsonConvert.DeserializeObject<JobObject>(fileContent);
                this.CopyFrom(job);
                this.HTMLFileName = file;
                this.LastModified = fileinfo.LastWriteTime;
                this.CreatedAt = fileinfo.CreationTime;
            }
            else if (File.Exists(file) && file.EndsWith(".html"))
            {             
                this.HTMLFileName = file;
                this.HTML = File.ReadAllText(file);                                
                this.Text = this.HTML.GetText();
                this.LastModified = fileinfo.LastWriteTime;
                this.CreatedAt = fileinfo.CreationTime;

                if (File.Exists(file.Replace(".html", ".json")))
                {
                    try
                    {
                        AIResponse aiResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<AIResponse>(File.ReadAllText(file.Replace(".html", ".json")));
                        if (aiResponse != null)
                        {
                            this.Reason = aiResponse.reason;
                            this.Coverletter = aiResponse.coverletter;
                            this.CompanyName = aiResponse.company;
                            this.Matched = aiResponse.matched;
                            this.JobRequirements = aiResponse.job_requirements;
                            this.MissingRequirements = aiResponse.missing_requirements;
                            this.Title = aiResponse.jobtitle;
                        }
                    }
                    catch (Exception exp)
                    {
                        Utilities.Logger.Error(exp);
                    }
                }
            }
            else
            {
                throw new FileNotFoundException(file);
            }
        }
        catch (Exception exp)
        {
            Utilities.Logger.Error(exp);
        }
    }

    public JobObject(string rootPath, JobFile jobfile)
    {
        this.HTMLFileName = Path.Combine(rootPath, $"{jobfile.timestamp}.html");
        this.JobPostUrl = new Uri(jobfile.Url);
        this.HTML = jobfile.Html.HtmlCleanup();
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
        try
        {
            return string.IsNullOrEmpty(HTMLFileName) ? "" : HTMLFileName.ToShortname();
        }
        catch (Exception exp)
        {
            return HTMLFileName;
            
        }
    }

    [Key(20)]
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    [Key(27)]
    public DateTime? ProcessedAt { get; set; }
    [Key(28)]
    public DateTime? AppliedAt { get; set; }
    [Key(21)]
    public  string Notes { get; set; }

    [Key(0)]
    public string id => JobPostUrl != null ? JobPostUrl.LocalPath.ToString().ToMd5Hash() : string.Empty;
    [Column(name: "company", TypeName = "varchar(50)")]
    [Key(1)]
    public string? CompanyName { get; set; }
    [Key(2)]
    public Uri? CompanyUrl { get; set; }
    [Key(3)]
    public Uri? CompanyLinkedIn { get; set; }
    [Key(6)]
    public Uri? JobPostUrl { get; set; } = null;
    [Key(7)]
    public string? Coverletter { get; set; } = null;
    [Key(8)]
    public List<string> MissingRequirements { get; set; } = new List<string>();
    [Key(11)]
    public List<string> JobRequirements { get; set; } = new List<string>();
    [Key(9)]
    public string? Reason { get; set; }
    [Key(10)]
    public bool Matched { get; set; } = false;

    [Key(12)]
    public string? Title { get; set; } 
    [Key(22)]
    public bool Applied { get; set; } = false;
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
    [IgnoreMember]
    public string HTML { get; set; }
    [IgnoreMember]
    public string? Text { get; set; }
    [Key(16)]
    public string? HTMLFileName { get; set; }
    [Key(30)]
    public DateTime LastModified { get; set; }
    [Key(31)]
    public string? ProfessionalSummary { get; set; }

    [Key(44)]
    public string CoverletterFile { get; set; } = null;
    [Key(45)]
    public string SuggestedResume { get; set; } = null;

    public static Response SaveAllToFile(string filename, List<JobObject> jobs)
    {
        byte[] bytes = MessagePackSerializer.Serialize(jobs);
        File.WriteAllBytes(filename, bytes);
        return new Response();

        //try
        //{
        //    jobs = jobs.Where(job => job.JobPostUrl != null).ToList();
        //    //Before saving set HtmlFileName, DataFieName, TextFilename, Text
        //    jobs.ForEach(static job =>
        //    {
        //        try
        //        {
        //            job.id = job.JobPostUrl != null ? job.JobPostUrl.ToString().ToMd5Hash() : Guid.NewGuid().ToString();
        //            job.HTMLFileName = job.AIResponse != null && !string.IsNullOrEmpty(job.AIResponse?.company) && !string.IsNullOrEmpty(job.AIResponse?.jobtitle) ? $"{job.CompanyName}_{job.JobTitle.Replace(" ", "")}" : job.HTMLFileName ?? $"{job.Shortname}.html";
        //            job.DataFileContent = !string.IsNullOrEmpty(job.JsonFileName) && File.Exists(job.JsonFileName) ? File.ReadAllBytes(job.JsonFileName) : null;
        //            job.Text = string.Empty;




        //        }
        //        catch (Exception exp)
        //        {
        //            Utilities.Logger.Error(exp.Message);
        //        }
        //    });

        //    byte[] bytes = MessagePackSerializer.Serialize(jobs);
        //    File.WriteAllBytes(filename, bytes);
        //    return new Response();
        //}
        //catch (Exception exp)
        //{
        //    Utilities.Logger.Error(exp.Message);
        //    return new Response(exp);
        //}
    }
    public static Response<List<JobObject>> LoadAllFromFile(string filename)
    {
        try
        {
            List<JobObject> jobs = MessagePackSerializer.Deserialize<List<JobObject>>(File.ReadAllBytes(filename));
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
    public void CopyFrom(JobObject result)
    {
        // copy all properties from result to this object except HTML, HtmlJobPostFileContent, DataFileContent, Text, TextFileContent, HTMLFileName, JsonFileName, TextFileName
        this.CompanyName = result.CompanyName;
        this.CompanyUrl = result.CompanyUrl;
        this.CompanyLinkedIn = result.CompanyLinkedIn;
        this.HTML = result.HTML.HtmlCleanup();
        this.JobPostUrl = result.JobPostUrl ;
        this.Coverletter = result.Coverletter;
        this.MissingRequirements = result.MissingRequirements;
        this.JobRequirements = result.JobRequirements;
        this.Reason = result.Reason;
        this.Matched = result.Matched;
        this.Title = result.Title;
        this.Applied = result.Applied;
        this.JobType = result.JobType;
        this.JobLocation = result.JobLocation;
        this.Where = result.Where;
        this.When = result.When;
        this.Notes = result.Notes;
        this.SalaryRange = result.SalaryRange;
        this.Text = result.Text;
        this.HTMLFileName = result.HTMLFileName;
        this.AppliedAt = result.AppliedAt;
        this.CreatedAt = result.CreatedAt;
        this.ProfessionalSummary = result.ProfessionalSummary;
        this.SuggestedResume = result.SuggestedResume;
    }
    public void Save()
    {
        try
        {
            File.WriteAllText(this.HTMLFileName, JsonConvert.SerializeObject(this, new StringEnumConverter()));
        }
        catch (Exception exp)
        {
            Utilities.Logger.Error(exp);
        }
    }    

}
