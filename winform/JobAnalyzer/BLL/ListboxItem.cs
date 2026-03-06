namespace JobAnalyzer.BLL;

public class ListboxItem
{
    public ListboxItem(FileInfo fileinfo)
    {
        File = fileinfo;
    }
    public FileInfo File { get; set; }

    public string DataFile => File.FullName.EndsWith(".html") ? File.FullName.Replace(".html", ".json") : File.FullName.Replace(".job.json", ".json");
    public string ShortName => File.FullName.Replace(".html", "");

    public override string ToString()
    {
        return File.Name.Replace(File.Extension, "");
    }
}
