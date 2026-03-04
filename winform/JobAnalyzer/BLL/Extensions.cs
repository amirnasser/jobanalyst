using System.Text.RegularExpressions;

namespace JobAnalyzer.BLL;

public static class Extensions
{
    //public static string GetDataFile(this ListBox listbox)
    //{
    //    return (listbox.SelectedItem as ListboxItem).DataFile;
    //}

    //public static string GetJobPostFile(this ListBox listbox)
    //{
    //    return (listbox.SelectedItem as ListboxItem).File.FullName;
    //}

    public static string Cleanup(this string response)
    {
        try
        {
            if (response.Contains("```json"))
            {
                Regex regx = new Regex(@"```json([\s\S]*?)```", RegexOptions.Multiline);
                var match = regx.Match(response);
                if (match.Success)
                {
                    return match.Groups[1].Value.Replace("\\n", "").Replace("\\", "");
                }
                return "";
            }
            else 
            {
                return response.Replace("\\n", "").Replace("\\", "");
            }
        }
        catch (Exception exp)
        {
            throw;
        }
    }

    public static string ToShortname(this string filename)
    {
        var file = new FileInfo(filename);
        return file.Name.Replace(file.Extension, string.Empty);
    }
    public static string GetText(this string html)
    { 
        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(html);
        return doc.DocumentNode.InnerText;
    }

    public static string SetFileExtension(this string filename, string extension)
    {
        var file = new FileInfo(filename);
        return file.Name.Replace(file.Extension, extension);
    }
}
