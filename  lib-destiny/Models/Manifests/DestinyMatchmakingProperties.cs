using System.Text.Json.Serialization;

namespace Destiny.Models.Manifests;

public class DestinyMatchmakingProperties
{
    [JsonPropertyName("isMatchmade")] 
    public bool IsMatchmade { get; set; } = false;

    [JsonPropertyName("minParty")] 
    public uint MinParty { get; set; } = 1;

    [JsonPropertyName("maxParty")] 
    public uint MaxParty { get; set; } = 6;

    [JsonPropertyName("maxPlayers")] 
    public uint MaxPlayers { get; set; } = 12;
    
    [JsonPropertyName("requiresGuardianOath")]
    public bool RequiresGuardianOath { get; set; } = true;
}