using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAnalyzer.BLL
{
    public class JobFile
    {
        public JobFile()
        {
            
        }
        public string Html { get; set; }
        public string Url { get; set; }
        public string Job { get; set; }
        public string timestamp { get; set; }

        public static JobFile LoadFromFile(string fullName)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<JobFile>(File.ReadAllText(fullName));
        }
    }
}
