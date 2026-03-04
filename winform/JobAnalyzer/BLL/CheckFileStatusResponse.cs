namespace JobAnalyzer.BLL;


public partial class AICall
{
    public class CheckFileStatusResponse
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string hash { get; set; }
        public string filename { get; set; }
        public string path { get; set; }
        public CheckFileStatusData data { get; set; }
        public CheckFileStatusMeta meta { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
    }
}

public class CheckFileStatusData
{
    public string status { get; set; }
    public string content { get; set; }
}

public class CheckFileStatusMeta
{
    public string name { get; set; }
    public string content_type { get; set; }
    public int size { get; set; }
    public CheckFileStatusData data { get; set; }
    public string collection_name { get; set; }
}