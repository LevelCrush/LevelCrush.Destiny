using Destiny.Api;
using Destiny.Helpers;

namespace Destiny.Tests;

public class HelperTest
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
    public async Task TestRaidReportGenerate()
    {
        
        var user = await DestinyMember.Search("Primal#8266"); 
        Assert.That(user, Is.Not.Null);
        
        var raidReportUrl = RaidReport.GenerateUrl(user);
        Assert.That(raidReportUrl, Is.EqualTo("https://raid.report/xb/4611686018439874403"));
        
    }   
}