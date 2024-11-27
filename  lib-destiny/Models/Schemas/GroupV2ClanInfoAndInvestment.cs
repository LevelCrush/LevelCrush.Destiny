using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class GroupV2ClanInfoAndInvestment
{
    [JsonPropertyName("clanCallsign")]
    public string ClanCallsign { get; set; }
    
    [JsonPropertyName("clanBannerData")]
    public ClanBanner ClanBanner { get; set; }
}