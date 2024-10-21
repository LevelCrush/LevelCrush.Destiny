using Destiny.Api;
using Destiny.Models.Enums;
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

    [Test]
    public async Task TestClanInfo()
    {
        // target clan = levelcrush
        var targetClan = 4356849;

        var info = await DestinyClan.Info(targetClan);
        Assert.That(info, Is.Not.Null);
        Assert.That(info.Detail.Name, Is.EqualTo("Level Crush"));
        Assert.That(info.Detail.ClanInfo.ClanCallsign, Is.EqualTo("LC"));
    }
    
    [Test]
    public async Task TestClanRoster()
    {
        // target clan = levelcrush
        var targetClan = 4356849;

        var roster = await DestinyClan.Roster(targetClan);
        Assert.That(roster, Is.Not.Null);
        Assert.That(roster.Results.Length, Is.GreaterThan(0));
    }

    [TestCase]
    public async Task TestClanFromMembership()
    {
        var targetMembership = 4611686018439874403;
        var targetMembershipType = BungieMembershipType.Xbox;

        var memberGroup = await DestinyClan.FromMembership(targetMembership, targetMembershipType);
        Assert.That(memberGroup, Is.Not.Null); 
        
        Assert.That(memberGroup.Results.Length, Is.GreaterThanOrEqualTo(1));
        Assert.That(memberGroup.Results.First().Group.Name, Is.EqualTo("Level Crush"));
    }

    [Test]
    public async Task TestActivityForCharacter()
    {
        var targetMembership = 4611686018439874403;
        var targetMembershipType = BungieMembershipType.Xbox;
        var targetCharacterId = 2305843009733485591;

        var activities = await DestinyActivity.ForCharacter(targetMembership, targetMembershipType, targetCharacterId);
        Assert.That(activities.Length, Is.GreaterThan(0));
        
        
    }

    [Test]
    public async Task TestActivityStatsForMember()
    {
        var targetMembership = 4611686018439874403;
        var targetMembershipType = BungieMembershipType.Xbox;
        var targetCharacterId = 2305843009733485591;

        var activities = await DestinyActivity.StatsForMember(targetMembership, targetMembershipType);
        Assert.That(activities, Is.Not.Null);
        
        
    }
}