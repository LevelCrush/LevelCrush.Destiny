using System.Collections.Concurrent;
using System.Text.Json.Serialization;

namespace Destiny.Models.Manifests;

public class DestinyRecordTitleBlock
{
    [JsonPropertyName("hasTitle")]
    public bool HasTitle { get; set; }
    
    [JsonPropertyName("titlesByGender")]
    public ConcurrentDictionary<string, string> TitlesByGender { get; set; } = new ConcurrentDictionary<string, string>();
}