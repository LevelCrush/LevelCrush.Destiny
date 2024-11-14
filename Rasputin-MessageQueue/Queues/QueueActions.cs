using System.Diagnostics;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Rasputin.MessageQueue.Models;

namespace Rasputin.MessageQueue.Queues;

public static class QueueActions
{

    private static readonly QueueBaseDirectJson<MessageAction> _queue;

    private const string TARGET_EXCHANGE = "rasputin.direct";
    private const string TARGET_QUEUE = "rasputin.actions";
    private const string TARGET_ROUTING_KEY = TARGET_QUEUE; // since we are going direct, juse use the queue name


    static QueueActions()
    {
        _queue = new QueueBaseDirectJson<MessageAction>(TARGET_EXCHANGE, TARGET_QUEUE, TARGET_ROUTING_KEY);
    }
    
    
    public static void Connect()
    {
        _queue.Connect();
    }

    public static void Disconnect()
    {
       _queue.Disconnect();
    }
    
    public static void Publish(MessageAction message)
    {
        _queue.Publish(message);
    }


    public static string Subscribe(Func<MessageAction?, Task> processCallback)
    {
       return _queue.Subscribe(processCallback);
    }

    public static MessageAction? Pull()
    {
        return _queue.Pull();
    }
    
    
}