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
    public async Task TestActivityDefinition()
    {
        var definition = await DestinyManifest.Get<DestinyActivityDefinition>();
        Assert.That(definition, Is.Not.Null);
        
        // at the moment the only way I can think of to just test this is to just check for a constant activity that we know about ahead of time
        // there is probably a better way though, but for now this is good enough

        // from inspecting the request, this id matches the Daily Heroic Story mission activity key
        var targetActivity = "129918239";
        Assert.That(definition.ContainsKey(targetActivity), Is.True);

    }
    
    [Test]
    public async Task TestClassDefinition()
    {
        var definition = await DestinyManifest.Get<DestinyClassDefinition>();
        Assert.That(definition, Is.Not.Null);
        
        // we know there are 3 classes
        // for now this is ok. But we should expand this to be more explicit eventually
       Assert.That(definition.Count, Is.EqualTo(3));
    }
    
    [Test]
    public async Task TestActivityTypeDefinition()
    {
        var definition = await DestinyManifest.Get<DestinyActivityTypeDefinition>();
        Assert.That(definition, Is.Not.Null);
        
        // target key is Momentum Control. A type we know will be present
        // there is probably a better way, but for now this works
        var targetKey = "3610972626";
        Assert.That(definition.ContainsKey(targetKey), Is.True);
        
    }
    
    [Test]
    public async Task TestSeasonDefinition()
    {
        var definition = await DestinyManifest.Get<DestinySeasonDefinition>();
        Assert.That(definition, Is.Not.Null);
        
        // target key is Season 24, Episode Echos. A type we know will be present
        // there is probably a better way, but for now this works
        var targetKey = "2758726572";
        Assert.That(definition.ContainsKey(targetKey), Is.True);
        
    }
    
    [Test]
    public async Task TestRecordDefinition()
    {
        var definition = await DestinyManifest.Get<DestinyRecordDefinition>();
        Assert.That(definition, Is.Not.Null);
        
        // target key is for Solar Rush record/triumph. A type we know will be present
        // there is probably a better way, but for now this works
        var targetKey = "3893443260";
        Assert.That(definition.ContainsKey(targetKey), Is.True);
        
    }
}