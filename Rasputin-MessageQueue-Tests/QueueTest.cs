using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NuGet.Frameworks;
using Rasputin.MessageQueue;
using Rasputin.MessageQueue.Enums;
using Rasputin.MessageQueue.Models;
using Rasputin.MessageQueue.Queues;

namespace Rasputin_MessageQueue_Tests;

public class Tests
{
    
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void TestPublishClan()
    {

        // connect to the rasputin queue
        QueueClan.Connect();
        
        
        // publish
        MessageClan clan = new MessageClan();
        clan.Task = MessageClanTask.Info;
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
}