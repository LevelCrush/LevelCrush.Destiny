using System.Diagnostics;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Rasputin.MessageQueue.Models;

namespace Rasputin.MessageQueue.Queues;

public static class QueueMember
{

    private static readonly QueueBase<MessageMember> _queue;

    private const string TARGET_EXCHANGE = "rasputin.direct";
    private const string TARGET_QUEUE = "rasputin.members";
    private const string TARGET_ROUTING_KEY = TARGET_QUEUE; // since we are going direct, juse use the queue name


    static QueueMember()
    {
        _queue = new QueueBase<MessageMember>(TARGET_EXCHANGE, TARGET_QUEUE, TARGET_ROUTING_KEY);
    }
    
    
    public static void Connect()
    {
        _queue.Connect();
    }

    public static void Disconnect()
    {
       _queue.Disconnect();
    }
    
    public static void Publish(MessageMember message)
    {
        _queue.Publish(message);
    }


    public static string Subscribe(Func<MessageMember?, Task> processCallback)
    {
       return _queue.Subscribe(processCallback);
    }

    public static MessageMember? Pull()
    {
        return _queue.Pull();
    }
    
    
}