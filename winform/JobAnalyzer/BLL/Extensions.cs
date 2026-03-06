using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Wordprocessing;

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

    public static string HtmlCleanup(this string html)
    {
        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(html);
        //remove svg element from doc
        // Select all <svg> nodes
        var svgNodes = doc.DocumentNode.SelectNodes("//svg");

        if (svgNodes != null)
        {
            foreach (var svg in svgNodes.ToList())
            {
                svg.Remove();
            }
        }
        return doc.DocumentNode.OuterHtml;
    }

    public static string ToShortname(this string filename)
    {
        var file = new FileInfo(filename);
        return string.IsNullOrEmpty(file.Extension) ? file.Name : file.Name.Replace(file.Extension, string.Empty);
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
        
        if(string.IsNullOrEmpty(file.Extension))
            return file.Name;

        return file.Name.Replace(file.Extension, extension);
    }
    public static string ToMd5Hash(this string input)
    {
        if (input == null)
            return null;

        using (var md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }
    }

}
