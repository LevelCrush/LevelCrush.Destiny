using System.Text.Json.Serialization;
using Destiny.Attributes;

namespace Destiny.Models.Manifests;

[DestinyDefinition("DestinyRecordDefinition")]
public class DestinyRecordDefinition
{
    [JsonPropertyName("displayProperties")]
    public DestinyDisplayProperties DisplayProperties { get; set; }
    
    [JsonPropertyName("scope")]
    public int Scope { get; set; }
    
    [JsonPropertyName("titleInfo")]
    public DestinyRecordTitleBlock TitleInfo { get; set; } = new DestinyRecordTitleBlock();

    [JsonPropertyName("forTitleGilding")] 
    public bool ForTitleGilding { get; set; } = false;
    
    [JsonPropertyName("hash")]
    public uint Hash { get; set; }
    
    [JsonPropertyName("index")]
    public uint Index { get; set; }
    
    [JsonPropertyName("redacted")]
    public bool Redacted { get; set; }
    
    [JsonPropertyName("blacklisted")]
    public bool Blacklisted { get; set; }
}