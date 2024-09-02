using Destiny.Api;

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
        var user = await Member.Search("Primal#8266");
        Assert.That(user, Is.Not.Null);
    }
}