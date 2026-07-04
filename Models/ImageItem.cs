namespace MetaLens.Models;
using System.IO;

public class ImageItem
{
    public string FilePath { get; }

    public string FileName => Path.GetFileName(FilePath);

    public Dictionary<string, List<KeyValuePair<string, string>>> Metadata { get; set; } = new();

    public ImageItem(string path)
    {
        FilePath = path;
    }
}