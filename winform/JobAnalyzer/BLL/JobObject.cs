using MessagePack;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
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
            this.HtmlJobPostFile = File.Exists(HTMLFileName) ? File.ReadAllBytes(HTMLFileName) : null;
            this.DataFile = !string.IsNullOrEmpty(JsonFileName) && File.Exists(JsonFileName) ? File.ReadAllBytes(JsonFileName) : null;

            if (DataFile != null)
            {
                try
                {
                    string json = ASCIIEncoding.UTF8.GetString(this.DataFile);
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

    [Key(0)]
    public int id { get; set; }
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
    public Uri? JobPostUrl => new Uri(Utilities.FindJobPostUrl(HTML));
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
    public string? JobTitle { get; set; }
    [Key(22)]
    public bool Applied { get; set; } = false;
    [Key(23)]
    public byte[]? HtmlJobPostFile { get; set; }
    [Key(24)]
    public byte[]? DataFile { get; set; }
    [Key(29)]
    public byte[]? TextFile => Encoding.UTF8.GetBytes(Text);
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
    public string HTML => Encoding.UTF8.GetString(HtmlJobPostFile).Replace("\\n", "");
    [IgnoreMember]
    public string Text => HTML.GetText();
    [Key(16)]
    public string? HTMLFileName { get; set; }
    [Key(18)]
    public string JsonFileName => HTMLFileName.Replace(".html", ".json");
    [Key(19)]
    public string TextFileName => File.Exists(HTMLFileName.SetFileExtension(".txt")) ? HTMLFileName?.SetFileExtension(".txt") : null ;


}
