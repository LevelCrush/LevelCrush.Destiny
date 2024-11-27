using System.Text.Json.Serialization;
using Destiny.Attributes;

namespace Destiny.Models.Manifests;

[DestinyDefinition("DestinyClassDefinition")]
public class DestinyClassDefinition
{
    [JsonPropertyName("classType")]
    public ushort ClassType { get; set; }
    
    [JsonPropertyName("displayProperties")]
    public DestinyDisplayProperties DisplayProperties { get; set; }
    
    [JsonPropertyName("hash")]
    public uint Hash { get; set; }
    
    [JsonPropertyName("index")]
    public uint Index { get; set; }
    
    [JsonPropertyName("redacted")]
    public bool Redacted { get; set; }
    
    [JsonPropertyName("blacklisted")]
    public bool Blacklisted { get; set; }
}