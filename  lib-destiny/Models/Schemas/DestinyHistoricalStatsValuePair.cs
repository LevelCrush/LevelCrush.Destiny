using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class DestinyHistoricalStatsValuePair
{
    [JsonPropertyName("value")]
    public double Value { get; set; }
    
    [JsonPropertyName("displayValue")]
    public string DisplayValue { get; set; }
}