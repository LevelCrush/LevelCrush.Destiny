using System.Collections.Concurrent;
using Destiny.Models.Enums;
using Destiny.Models.Requests;
using Destiny.Models.Responses;
using Destiny.Models.Schemas;

namespace Destiny.Api;

public static class DestinyMember
{


    public static async Task<UserInfoCard?> MembershipById(long membershipId)
    {
        // this seems like a weird design choice...
        // does it make
        var membershpiIdString = membershipId.ToString();
        
        var req = await BungieClient.Get("/User/GetMembershipsById/{membershipId}/0")
            .Param(DestinyRouteParam.PlatformMembershipID, membershipId.ToString())
            .Send<UserMembershipData>();

        if (req.Response != null)
        {
            if (req.Response.Memberships.Count == 1)
            {
                return req.Response.Memberships.First();
            }
            else
            {
                foreach (var card in req.Response.Memberships)
                {
                    if (card.MembershipId == membershipId)
                    {
                        
                    }
                }
            }
        }

        return null;
    }
    
    
    /// <summary>
    ///     Explicitly get the profile
    /// </summary>
    /// <param name="membershipId"></param>
    /// <param name="membershipType"></param>
    /// <returns></returns>
    public static async Task<DestinyProfileResponse?> Profile(long membershipId, int membershipType)
    {
        var req = await BungieClient.Get("/Destiny2/{membershipType}/Profile/{membershipId}/")
            .Param(DestinyRouteParam.PlatformMembershipID, membershipId.ToString())
            .Param(DestinyRouteParam.PlatformMembershipType, membershipType.ToString())
            .Component(DestinyComponentType.Profiles)
            .Component(DestinyComponentType.Characters)
            .Component(DestinyComponentType.Records)
            .Send<DestinyProfileResponse>();

        return req != null ? req.Response : null;
    }


    /// <summary>
    ///     Searches the Bungie Databases for a Destiny2 user with the specified display name+code
    ///     This is an api function and will **always** call the Bungie API.
    /// </summary>
    /// <param name="bungieName"></param>
    /// <returns></returns>
    public static async Task<UserInfoCard?> Search(string bungieName)
    {
        var targetCode = "";
        var targetName = "";
        var splitString = bungieName.Split("#");

        if (splitString.Length > 1)
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