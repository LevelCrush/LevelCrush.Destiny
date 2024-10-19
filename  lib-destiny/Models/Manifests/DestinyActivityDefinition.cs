using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Destiny.Attributes;

namespace Destiny.Models.Manifests;

// type alias
using DestinyActivityDefinitionMap = ConcurrentDictionary<string, DestinyActivityDefinition>;

[DestinyDefinition("DestinyActivityDefinition")]
public class DestinyActivityDefinition
{
    
    [JsonPropertyName("matchmaking")]
    public DestinyMatchmakingProperties Matchmaking { get; set; } = new DestinyMatchmakingProperties();

    [JsonPropertyName("displayProperties")]
    public DestinyDisplayProperties DisplayProperties { get; set; } = new DestinyDisplayProperties();
    
    [JsonPropertyName("originalDisplayProperties")]
    public DestinyDisplayProperties OriginalDisplayProperties { get; set; } = new DestinyDisplayProperties();

    [JsonPropertyName("selectionScreenDisplayProperties")]
    public DestinyDisplayProperties SelectionScreenDisplayProperties { get; set; } = new DestinyDisplayProperties();

    [JsonPropertyName("activityTypeHash")]
    public uint ActivityTypeHash { get; set; }
    
    [JsonPropertyName("destinationHash")]
    public uint DestinationHash { get; set; }
    
    [JsonPropertyName("placeHash")]
    public uint PlaceHash { get; set; }
    
    [JsonPropertyName("pgcrImage")]
    public string PgcrImage { get; set; }
    
    [JsonPropertyName("isPvP")]
    public bool IsPvP { get; set; }
    
    [JsonPropertyName("isPlaylist")]
    public bool IsPlaylist { get; set; }
    
    [JsonPropertyName("directActivityModeHash")]
    public uint DirectActivityModeHash { get; set; }
    
    [JsonPropertyName("directActivityModeType")]
    public uint DirectActivityModeType { get; set; }
    
    [JsonPropertyName("hash")]
    public uint Hash { get; set; }
    
    [JsonPropertyName("index")]
    public uint Index { get; set; }
    
    [JsonPropertyName("redacted")]
    public bool Redacted { get; set; }
    
    [JsonPropertyName("blacklisted")]
    public bool Blacklisted { get; set; }
}