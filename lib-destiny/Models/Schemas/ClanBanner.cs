using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class ClanBanner
{
    [JsonPropertyName("decalId")]
    public uint DecalId { get; set; }
    
    [JsonPropertyName("decalColorId")]
    public uint DecalColorId { get; set; }
    
    [JsonPropertyName("decalBackgroundColorId")]
    public uint DecalBackgroundColorId { get; set; }
    
    [JsonPropertyName("gonfalonId")]
    public uint GonfalonId { get; set; }
    
    [JsonPropertyName("gonfalonColorId")]
    public uint GonfalonColorId { get; set; }
    
    [JsonPropertyName("gonfalonDetailId")]
    public uint GofalonDetailId { get; set; }
    
    [JsonPropertyName("gonfalonDetailColorId")]
    public uint GofalonDetailColorId { get; set; }
}