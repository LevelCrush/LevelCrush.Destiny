using System.Collections.Concurrent;
using System.Text.Json.Serialization;
using Destiny.Models.Manifests;

namespace Destiny.Models.Schemas;

public class DestinyCharacterRecordsComponent
{
    [JsonPropertyName("featuredRecordHashes")]
    public uint[] FeaturedRecords { get; set; } = [];

    [JsonPropertyName("records")]
    public ConcurrentDictionary<string, DestinyRecordComponent> Records { get; set; } =
        new ConcurrentDictionary<string, DestinyRecordComponent>();

    [JsonPropertyName("recordCategoriesRootNodeHash")]
    public uint RecordCategoriesRootNodeHash { get; set; } = 0;

    [JsonPropertyName("recordsSealsRootNodeHash")]
    public uint RecordsSealsRootNodeHash { get; set; } = 0;
}