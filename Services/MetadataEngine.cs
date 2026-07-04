using MetadataExtractor;
using MetaLens.Models;

namespace MetaLens.Services;

public class MetadataEngine
{
    public List<RawMetadata> Extract(string filePath)
    {
        var result = new List<RawMetadata>();

        var directories = ImageMetadataReader.ReadMetadata(filePath);

        foreach (var dir in directories)
        {
            foreach (var tag in dir.Tags)
            {
                if (string.IsNullOrWhiteSpace(tag.Name))
                    continue;

                result.Add(new RawMetadata(tag.Name, tag.Description ?? ""));
            }
        }

        return result;
    }
}