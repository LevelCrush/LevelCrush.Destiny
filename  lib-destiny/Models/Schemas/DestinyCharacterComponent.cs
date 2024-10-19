using System.Text.Json.Serialization;
using Destiny.Models.Enums;

namespace Destiny.Models.Schemas;

public class DestinyCharacterComponent
{
    [JsonPropertyName("membershipId")]
    public long MembershipId { get; set; }

    [JsonPropertyName("membershipType")]
    public BungieMembershipType MembershipType { get; set; }

    [JsonPropertyName("characterId")]
    public long CharacterId { get; set; }

    [JsonPropertyName("dateLastPlayed")] 
    public DateTime LastPlayed { get; set; } = DateTime.UnixEpoch;

    [JsonPropertyName("minutesPlayedTotal")]
    public string MinutesPlayedSession { get; set; }

    [JsonPropertyName("light")]
    public int Light { get; set; }

    [JsonPropertyName("classHash")]
    public uint ClassHash { get; set; }

    [JsonPropertyName("classType")]
    public BungieClassType ClassType { get; set; }

    [JsonPropertyName("emblemPath")]
    public string EmblemPath { get; set; }

    [JsonPropertyName("emblemBackgroundPath")]
    public string EmblemBackgroundPath { get; set; }

    [JsonPropertyName("emblemHash")]
    public uint EmblemHash { get; set; }
}