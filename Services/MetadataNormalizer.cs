using MetaLens.Models;

namespace MetaLens.Services;

public class MetadataNormalizer
{
    public List<RawMetadata> Normalize(List<RawMetadata> input)
    {
        return input
            .Where(x => !string.IsNullOrWhiteSpace(x.Value))
            .Select(x => new RawMetadata(
                x.Key.Trim(),
                x.Value.Trim()
            ))
            .ToList();
    }
}