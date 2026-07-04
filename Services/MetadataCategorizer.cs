using MetaLens.Models;

namespace MetaLens.Services;

public class MetadataCategorizer
{
    private readonly Dictionary<string, string> _map;

    public MetadataCategorizer(Dictionary<string, string> map)
    {
        _map = map;
    }

    public Dictionary<string, List<RawMetadata>> Categorize(List<RawMetadata> items)
    {
        var result = new Dictionary<string, List<RawMetadata>>();

        foreach (var item in items)
        {
            var category = _map.TryGetValue(item.Key, out var cat)
                ? cat
                : "Other";

            if (!result.ContainsKey(category))
                result[category] = new List<RawMetadata>();

            result[category].Add(item);
        }

        return result;
    }
}