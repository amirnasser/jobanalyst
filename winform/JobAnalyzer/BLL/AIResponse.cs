using MessagePack;
using Microsoft.EntityFrameworkCore;

namespace JobAnalyzer.BLL;


[MessagePackObject]

public class AIResponse
{
    [Key(0)]
    //[PrimaryKey("Id")]
    public int Id { get; set; }
    [Key(1)]    
    public string? company { get; set; }
    [Key(2)]
    public string? coverletter { get; set; }
    //public string? filename { get; set; } = null;
    //public string? joburl { get; set; }
    [Key(3)]
    public List<string>? missing_requirements { get; set; }
    [Key(4)]
    public string? reason { get; set; } 
    [Key(5)]
    public bool matched { get; set; } = false;
    [Key(6)]
    public List<string>? job_requirements { get; set; } 
    [Key(7)]
    public string? jobtitle { get; set; }
    [Key(8)]
    public bool? applied { get; set; } = false;
    [Key(9)]
    public string? professional_summary { get; set; }
    [Key(10)]
    public string? pre { get; set; } = null;
    [Key(11)]
    public string suggested_resume { get; set; } = null;
}
