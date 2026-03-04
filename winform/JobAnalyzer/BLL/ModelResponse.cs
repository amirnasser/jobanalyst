namespace JobAnalyzer.BLL;

//public static class extensions
//{
//    public static string Cleanup(this string response)
//    {
//        try
//        {
//            Regex regx = new Regex(@"(```json)(.*)(```)", RegexOptions.Multiline);
//            var match = regx.Match(response);
//            if (match.Success)
//            {
//                return match.Groups[2].Value.Replace("\\n", "").Replace("\\", "");
//            }
//            return "";
//        }
//        catch (Exception exp)
//        {
//            throw;
//        }
//    }
//}

public class ModelResponse
{
    public List<Model> data { get; set; }
}
