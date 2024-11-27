using System.Diagnostics;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Rasputin.MessageQueue.Models;

namespace Rasputin.MessageQueue.Queues;

public static class QueueDBSync
{
    
    private static readonly QueueBaseDirectJson<MessageDbSync> _queue;

    private const string TARGET_EXCHANGE = "rasputin.direct";
    private const string TARGET_QUEUE = "rasputin.db_sync";
    private const string TARGET_ROUTING_KEY = TARGET_QUEUE; // since we are going direct, juse use the queue name

    static QueueDBSync()
    {
        _queue = new QueueBaseDirectJson<MessageDbSync>(TARGET_EXCHANGE, TARGET_QUEUE, TARGET_ROUTING_KEY);
            
    }
    
    public static void Connect()
    {
        _queue.Connect();
    }


    public static void Disconnect()
    {
        _queue.Disconnect();
    }
    
    public static void Publish(MessageDbSync message)
    {
        _queue.Publish(message);
    }


    public static string Subscribe(Func<MessageDbSync?, Task> processCallback)
    {
        return _queue.Subscribe(processCallback);
    }

    public static MessageDbSync? Pull()
    {
        return _queue.Pull();
    }
    
    
}