using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class DestinyHistoricalPlayer
{
    [JsonPropertyName("destinyUserInfo")]
    public UserInfoCard User { get; set; }

    [JsonPropertyName("characterClass")] 
    public string CharacterClass { get; set; } = string.Empty;

    [JsonPropertyName("classHash")] 
    public uint ClassHash { get; set; } = 0;

    [JsonPropertyName("characterLevel")] 
    public int CharacterLevel { get; set; } = 0;
    
    [JsonPropertyName("lightLevel")]
    public int LightLevel { get; set; } = 0;
    
    [JsonPropertyName("bungieNetUserInfo")]
    public UserInfoCard BungieNetUserInfo { get; set; } = new UserInfoCard();
    
    [JsonPropertyName("clanName")]
    public string ClanName { get; set; } = string.Empty;
    
    [JsonPropertyName("clanTag")]
    public string ClanTag { get; set; } = string.Empty;

    [JsonPropertyName("emblemHash")] 
    public uint EmblemHash { get; set; } = 0;
}