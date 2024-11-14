using System.Text.Json.Serialization;

namespace Rasputin.MessageQueue.Models;

public class MessageAction
{
    [JsonPropertyName("action")]
    public string Action { get; set; } = string.Empty;

    [JsonPropertyName("entities")] 
    public string[] Entities { get; set; } = [];
}