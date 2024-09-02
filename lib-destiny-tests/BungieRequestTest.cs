using System.Collections.Concurrent;
using Destiny.Models.Enums;
using Destiny.Models.Requests;
using Destiny.Models.Schemas;

namespace Destiny.Tests;

public class BungieRequestTest
{
    private static DestinyConfig Config { get; set; }

    [SetUp]
    public void Setup()
    {
        //   var config = new ConfigurationBu
        Config = DestinyConfig.Load();

        BungieClient.ApiKey = Config.ApiKey;
    }

    [Test]
    public async Task TestSend()
    {
        var req = await BungieClient.Post<UserExactSearchRequest>(
                "/Destiny2/SearchDestinyPlayerByBungieName/{membershipType}/")
            .Param(DestinyRouteParam.PlatformMembershipType, "All")
            .Body(new UserExactSearchRequest
            {
                DisplayName = "Primal",
                Code = 8266
            })
            .Send<ConcurrentQueue<UserInfoCard>>();


        Assert.That(req.Response.Count, Is.GreaterThanOrEqualTo(1));
    }
}