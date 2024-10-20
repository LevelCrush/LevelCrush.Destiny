using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class DestinyPostGameCarnageReportData
{
    [JsonPropertyName("period")] 
    public DateTime Period { get; set; } = DateTime.UnixEpoch;
    
    [JsonPropertyName("startingPhaseIndex")]
    public int? StartingPhaseIndex { get; set; }
    
    [JsonPropertyName("activityWasStartedFromBeginning")]
    public bool? ActivityWasStartedFromBeginning { get; set; }
    
    [JsonPropertyName("activityDetails")]
    public DestinyHistoricalStatsActivity ActivityDetails { get; set; }

    [JsonPropertyName("entries")] 
    public DestinyPostGameCarnageReportEntry[] Entries { get; set; } = [];
    
    [JsonPropertyName("teams")]
    public DestinyPostGameReportTeamEntry[] Teams { get; set; } = [];
}