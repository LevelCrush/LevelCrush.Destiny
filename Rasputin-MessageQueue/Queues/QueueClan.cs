using System.Diagnostics;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Rasputin.MessageQueue.Models;

namespace Rasputin.MessageQueue.Queues;

public static class QueueClan
{

    private static readonly QueueBaseDirectJson<MessageClan> _queue;

    private const string TARGET_EXCHANGE = "rasputin.direct";
    private const string TARGET_QUEUE = "rasputin.clans";
    private const string TARGET_ROUTING_KEY = TARGET_QUEUE; // since we are going direct, juse use the queue name

    static QueueClan()
    {
        _queue = new QueueBaseDirectJson<MessageClan>(TARGET_EXCHANGE, TARGET_QUEUE, TARGET_ROUTING_KEY);
    }
    
    public static void Connect()
    {
        _queue.Connect();
    }

    public static void Disconnect()
    {
        _queue.Disconnect();
    }
    
    public static void Publish(MessageClan clanMessage)
    {
        _queue.Publish(clanMessage);
    }


    public static string Subscribe(Func<MessageClan?, Task> processCallback)
    {
        return _queue.Subscribe(processCallback);
    }

    public static MessageClan? Pull()
    {
        return _queue.Pull();
    }
    
    
}