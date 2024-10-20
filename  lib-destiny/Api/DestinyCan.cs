using Destiny.Models.Enums;
using Destiny.Models.Responses;
using Destiny.Models.Schemas;

namespace Destiny.Api;

public class DestinyCan
{
    public static async Task<GetGroupsForMemberResponse?> FromMembership(long membershipId,
        BungieMembershipType membershipType)
    {
        var req = await BungieClient.Get("/GroupV2/User/{membershipType}/{membershipId}/0/1")
            .Param(DestinyRouteParam.PlatformMembershipID, membershipId.ToString())
            .Param(DestinyRouteParam.PlatformMembershipType, ((int)membershipType).ToString())
            .Send<GetGroupsForMemberResponse>();

        return req != null ? req.Response : null;
    }

    public static async Task<DestinyGroupResponse?> Info(long groupId)
    {
        var req = await BungieClient.Get("/GroupV2/{groupId}/")
            .Param(DestinyRouteParam.GroupID, groupId.ToString())
            .Send<DestinyGroupResponse>();

        return req != null ? req.Response : null;
    }

    public static async Task<DestinySearchResultOfGroupMember?> Roster(long groupId)
    {
        var req = await BungieClient.Get("/GroupV2/{groupId}/Members")
            .Param(DestinyRouteParam.GroupID, groupId.ToString())
            .Send<DestinySearchResultOfGroupMember>();
        
        return req != null ? req.Response : null;
    }
}