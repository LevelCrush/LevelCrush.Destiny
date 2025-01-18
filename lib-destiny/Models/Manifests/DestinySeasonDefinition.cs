using System.Text.Json.Serialization;
using Destiny.Attributes;

namespace Destiny.Models.Manifests;

[DestinyDefinition("DestinySeasonDefinition")]
public class DestinySeasonDefinition
{
    
    [JsonPropertyName("displayProperties")]
    public DestinyDisplayProperties DisplayProperties { get; set; }
    
    [JsonPropertyName("seasonNumber")]
    public int SeasonNumber { get; set; }
    
    [JsonPropertyName("seasonPassHash")] 
    public uint SeasonPassHash { get; set; } = 0;

    [JsonPropertyName("startDate")] 
    public DateTime StartDate { get; set; } = DateTime.UnixEpoch;
    
    [JsonPropertyName("endDate")]
    public DateTime EndDate { get; set; } = DateTime.UnixEpoch;
    
    [JsonPropertyName("hash")]
    public uint Hash { get; set; }
    
    [JsonPropertyName("index")]
    public uint Index { get; set; }
    
    [JsonPropertyName("redacted")]
    public bool Redacted { get; set; }
    
    [JsonPropertyName("blacklisted")]
    public bool Blacklisted { get; set; }

    [JsonPropertyName("acts")] 
    public DestinySeasonAct[] Acts { get; set; } = [];
}