using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace JobAnalyzer.BLL;

public class JobFile
{
    public JobFile()
    {
        
    }
    public string Html { get; set; }
    public string Url { get; set; }
    public string Text { get; set; }
    public string timestamp { get; set; }

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
}
