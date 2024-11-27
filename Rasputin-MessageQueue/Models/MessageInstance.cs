using System.Text.Json.Serialization;
using Rasputin.MessageQueue.Enums;

namespace Rasputin.MessageQueue.Models;

public class MessageInstance
{
    
    [JsonPropertyName("entities")] 
    public string[] Entities { get; set; } = [];
}