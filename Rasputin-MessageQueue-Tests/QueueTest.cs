using Destiny;
using Destiny.Api;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NuGet.Frameworks;
using Rasputin.MessageQueue;
using Rasputin.MessageQueue.Enums;
using Rasputin.MessageQueue.Models;
using Rasputin.MessageQueue.Queues;

namespace Rasputin_MessageQueue_Tests;

public class Tests
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
    public async Task TestConsumer()
    {
        QueueClan.Connect();


        var messagesAck = 0;
        var consumerTag = QueueClan.Subscribe(async clan =>
        {
            Assert.That(clan, Is.Not.Null);
            TestContext.WriteLine($"Message Received:{++messagesAck}");
        });
        
        
        QueueClan.Disconnect();
        RasputinMessageQueue.Disconnect();
    }

    [Test]
    public void TestPublishClan()
    {

        // connect to the rasputin queue
        QueueClan.Connect();
        
        
        // publish
        MessageClan clan = new MessageClan();
        clan.Task = MessageClanTask.Crawl;
        clan.Entities = new[]
        {
           "4356849", // levelcrush
            "4250497", // level stomp
        };

        QueueClan.Publish(clan);
        
        
        // release
        QueueClan.Disconnect();
        RasputinMessageQueue.Disconnect();

        Assert.Pass();
    }
    
    
    [Test]
    public async Task TestPublishMember()
    {

        // connect to the rasputin queue
        QueueMember.Connect();
        
        
      //  var user = await DestinyMember.Profile(4611686018439874403, 1);
        QueueMember.Publish(new MessageMember()
        {
            Task = MessageMemberTask.Info,
            Entities = ["4611686018439874403"]
        });
        
        
        QueueMember.Disconnect();
        RasputinMessageQueue.Disconnect();

        Assert.Pass();
    }
}