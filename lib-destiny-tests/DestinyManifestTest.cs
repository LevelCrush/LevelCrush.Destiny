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
        Assert.That(manifest, Is.Not.Null);
        
        // at the moment the only way I can think of to just test this is to just check for a constant activity that we know about ahead of time
        // there is probably a better way though, but for now this is good enough

        // from inspecting the request, this id matches the Daily Heroic Story mission activity key
        var targetActivity = "129918239";
        Assert.That(manifest.ContainsKey(targetActivity), Is.True);

    }
}