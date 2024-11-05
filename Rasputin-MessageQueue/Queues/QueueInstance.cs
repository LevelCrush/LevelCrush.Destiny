using System.Diagnostics;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Rasputin.MessageQueue.Models;

namespace Rasputin.MessageQueue.Queues;

public static class QueueInstance
{


    private static readonly QueueBase<MessageInstance> _queue;

    private const string TARGET_EXCHANGE = "rasputin.direct";
    private const string TARGET_QUEUE = "rasputin.instances";
    private const string TARGET_ROUTING_KEY = TARGET_QUEUE; // since we are going direct, juse use the queue name

    static QueueInstance()
    {
        _queue = new QueueBase<MessageInstance>(TARGET_EXCHANGE, TARGET_QUEUE, TARGET_ROUTING_KEY);
            
    }
    
    public static void Connect()
    {
        _queue.Connect();
    }


    public static void Disconnect()
    {
        _queue.Disconnect();
    }
    
    public static void Publish(MessageInstance message)
    {
        _queue.Publish(message);
    }


    public static string Subscribe(Func<MessageInstance?, Task> processCallback)
    {
        return _queue.Subscribe(processCallback);
    }

    public static MessageInstance? Pull()
    {
        return _queue.Pull();
    }
    
    
}