using Destiny.Models.Enums;
using Destiny.Models.Schemas;

namespace Destiny.Api;

public class DestinyInstance
{
    public static async Task<DestinyPostGameCarnageReportData?> CarnageReport(long instanceId)
    {
        // interally RestSharp/Built in C# Http handler is not following redirecto http location. the endpoint is no longer hosted on the normal bungie endpoint and has been moved to a different domain (stats.bungie.net)
        // https://stats.bungie.net/Platform
        var request = await BungieClient.Get("/Destiny2/Stats/PostGameCarnageReport/{activityId}/")
            .Param(DestinyRouteParam.Activity, instanceId.ToString())
            .Send<DestinyPostGameCarnageReportData>();

        return request != null ? request.Response : null;
    }
}