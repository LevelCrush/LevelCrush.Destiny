using System.Collections.Concurrent;
using Destiny.Models.Enums;
using Destiny.Models.Requests;
using Destiny.Models.Schemas;

namespace Destiny.Api;

public static class DestinyMember
{
    public static async Task<UserInfoCard?> Search(string bungieName)
    {
        var targetCode = "";
        var targetName = "";
        var splitString = bungieName.Split("#");

        if (splitString.Length >= 1)
        {
            targetCode = splitString.Last();
            targetName = bungieName.Replace($"#{targetCode}", "");
        }
        else
        {
            targetName = bungieName;
        }

        short targetCodeParsed = 0;
        short.TryParse(targetCode, out targetCodeParsed);

        var req = await BungieClient.Post<UserExactSearchRequest>(
                "/Destiny2/SearchDestinyPlayerByBungieName/{membershipType}/")
            .Param(DestinyRouteParam.PlatformMembershipType, "All")
            .Body(new UserExactSearchRequest
            {
                DisplayName = targetName,
                Code = targetCodeParsed
            })
            .Send<ConcurrentQueue<UserInfoCard>>();

        if (req != null && req.Response != null)
        {
            UserInfoCard? targetUserCard = null;
            foreach (var userCard in req.Response)
            {
                targetUserCard = userCard;
                if (userCard.MembershipType == userCard.CrossSaveOverride)
                {
                    break;
                }
            }

            return targetUserCard;
        }

        return null;
    }
}