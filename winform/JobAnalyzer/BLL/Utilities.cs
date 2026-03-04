using Serilog;

namespace JobAnalyzer.BLL;


public static class Utilities
{

    public static string Normalize(string s) => Uri.UnescapeDataString(s.Replace("–", "-").Replace("—", "-").ToLower());

    public static readonly ILogger Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(
                path: Path.Combine("Log", "jobanalyzer-.log"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
            )
            .CreateLogger();

    public static Uri? FindLinkedInJobUrl(string file)
    {
        try
        {
            if (!File.Exists(file)) { throw new FileNotFoundException(file); }

            var html = File.ReadAllText(file);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            return new Uri(Normalize(Uri.UnescapeDataString(doc.DocumentNode.SelectSingleNode("//body/p/a").Attributes[0].Value)));
        }
        catch (Exception exp)
        {
            Utilities.Logger.Error(exp.Message);
            return null;
        }
    }

    public static string FindJobPostUrl(string html)
    {        
        var doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(html);

        return doc.DocumentNode.SelectSingleNode("//body/p/a").Attributes[0].Value;
    }


}
