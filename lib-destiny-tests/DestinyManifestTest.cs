using Destiny.Api;
using Destiny.Models.Enums;
using Destiny.Models.Manifests;
using Destiny.Models.Responses;

namespace Destiny.Tests;

public class DestinyManifestTest
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
    public async Task TestManifest()
    {
        var manifest = await DestinyManifest.Get<DestinyActivityDefinition>();
        Assert.That(1, Is.EqualTo(1));
    }
}