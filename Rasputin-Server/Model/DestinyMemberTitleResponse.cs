using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Rasputin_Server.Model;

public class DestinyMemberTitleResponse
{
    [JsonPropertyName("title")]
    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("amount")]
    [Column("amount")]
    public int Amount { get; set; } = 0;
}