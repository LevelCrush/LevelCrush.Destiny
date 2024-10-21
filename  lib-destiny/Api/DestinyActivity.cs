using Destiny.Models.Enums;
using Destiny.Models.Responses;
using Destiny.Models.Schemas;

namespace Destiny.Api;

public class DestinyActivity
{

    public static async Task<DestinyHistoricalStatsAccountResult?> StatsForMember(long membershipId,
        BungieMembershipType membershipType)
    {
        var req = await BungieClient.Get("/Destiny2/{membershipType}/Account/{membershipId}/Stats/")
            .Param(DestinyRouteParam.PlatformMembershipID, membershipId.ToString())
            .Param(DestinyRouteParam.PlatformMembershipType, ((int)membershipType).ToString())
            .Send<DestinyHistoricalStatsAccountResult>();

        return req != null ? req.Response : null;
    }

    public static async Task<DestinyActivityHistoryResults?> CharacterActivityPage(long membershipId,
        BungieMembershipType membershipType, long characterId, uint page, uint resultsPerPage = 250)
    {

        var req = await BungieClient
            .Get("/Destiny2/{membershipType}/Account/{membershipId}/Character/{characterId}/Stats/Activities")
            .Param(DestinyRouteParam.PlatformMembershipID, membershipId.ToString())
            .Param(DestinyRouteParam.PlatformMembershipType, ((int)membershipType).ToString())
            .Param(DestinyRouteParam.Character, characterId.ToString())
            .Query("page", page.ToString())
            .Query("count", resultsPerPage.ToString())
            .Send<DestinyActivityHistoryResults>();

        return req != null ? req.Response : null;
    }
    
    
    public static async Task<DestinyHistoricalStatsPeriodGroup[]> ForCharacter(long membershipId,
        BungieMembershipType membershipType, long characterId, long timestampStart = 0)
    {
        
        List<DestinyHistoricalStatsPeriodGroup> statActivities= new List<DestinyHistoricalStatsPeriodGroup>();

        uint page = 0;
        DestinyActivityHistoryResults? activityPage = null;
        while((activityPage = await  CharacterActivityPage(membershipId,membershipType,characterId,page)) != null)
        {
            uint foundActivities = 0;
            foreach (var activity in activityPage.Activities)
            {
                var timestamp = ((DateTimeOffset)activity.Period).ToUnixTimeSeconds();
                if (timestamp > timestampStart)
                {
                    statActivities.Add(activity);
                    foundActivities += 1;
                }
            }

            if (foundActivities == 0)
            {
                // no more activities have been added on this go. We are done
                break;
            }
           
            page += 1;
            
        }

    
        // convert to flat array at the end
        return statActivities.ToArray();    
    }
}