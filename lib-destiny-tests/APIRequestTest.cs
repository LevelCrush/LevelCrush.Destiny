using Destiny.Api;
using NuGet.Frameworks;

namespace Destiny.Tests;

public class APIRequestTest
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
    public async Task TestSearch()
    {
        var user = await DestinyMember.Search("Primal#8266");
        
        Assert.That(user, Is.Not.Null);
        Assert.That(user.GlobalDisplayName, Is.EqualTo("Primal"));
        Assert.That(user.GlobalDisplayNameCode, Is.EqualTo(8266));
    }

    [Test]
    public async Task TestProfile()
    {
        // issue request and query test user
        var user = await DestinyMember.Profile(4611686018439874403, 1);
        Assert.That(user, Is.Not.Null);
        Assert.That(user.Profile, Is.Not.Null);
        Assert.That(user.Profile.Data, Is.Not.Null);
       
        // compare to test user
        Assert.That(user.Profile.Data.UserInfo.GlobalDisplayName, Is.EqualTo("Primal"));
        Assert.That(user.Profile.Data.UserInfo.GlobalDisplayNameCode, Is.EqualTo(8266));
    }

    [Test]
    public async Task TestCarnageReport()
    {
        var targetInstance = 9472769153; // 12329004052
        var report = await DestinyInstance.CarnageReport(targetInstance );
        
        Assert.That(report, Is.Not.Null);
    }
}