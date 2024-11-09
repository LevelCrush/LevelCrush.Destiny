// See https://aka.ms/new-console-template for more information
using System.CommandLine;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rasputin.MessageQueue;
using Rasputin.MessageQueue.Consumer;
using Rasputin.MessageQueue.Queues;


void Log(string message, int level = 0)
{
    Console.WriteLine(message);
}

void ConsumeMemberQueue()
{
    QueueMember.Subscribe(async (member) =>
    {
        if (member != null)
        {
            await ConsumerMember.Process(member);
        }
        else
        {
            Log("Received a null member. Deserialization may of gone wrong");
        }
    });
}

void ConsumeClanQueue()
{
    QueueClan.Subscribe(async (clan) =>
    {
        if (clan != null)
        {
            await ConsumerClan.Process(clan);
        }
        else
        {
            Log("Received a null clan. Deserialization may of gone wrong");
        }
    });
}

void ConsumeInstanceQueue()
{
    QueueInstance.Subscribe(async (instance) =>
    {
        if (instance != null)
        {
            await ConsumerInstance.Process(instance);
        }
        else
        {
            Log("Received a null instance. Deserialization may of gone wrong");
        }
    });
}

// setup any cli options that we  will need
var targetQueueOption =
    new Option<string>(name: "--queue", description: "The target queue that this consumer will run for");


var rootCommand = new RootCommand("Utilize Rasputin and consume in the background");
rootCommand.AddOption(targetQueueOption);
rootCommand.SetHandler((queue) =>
{

    var q = queue.Trim().ToLower();

    switch (q)
    {
        case "member":
            ConsumeMemberQueue();
            break;
        case "clan":
            ConsumeClanQueue();
            break;
        case "instance":
        default:
            ConsumeInstanceQueue();
            break;
    }

}, targetQueueOption);


// run the CLI commands 
await rootCommand.InvokeAsync(args);

// handle CTRL+C 
var exitEvent = new ManualResetEvent(false);
Console.CancelKeyPress += (sender, e) =>
{
    e.Cancel = true;
    exitEvent.Set();
};

// handle SIGINT
PosixSignalRegistration.Create(PosixSignal.SIGINT, context =>
{
    context.Cancel = true;
    exitEvent.Set();
});

// handle SIGTERM
PosixSignalRegistration.Create(PosixSignal.SIGTERM, context =>
{
    context.Cancel = true;
    exitEvent.Set();
});



// until we receive the exit event , keep waiting
exitEvent.WaitOne();

// close all connections
// this will also close all consumers and channels automatically
RasputinMessageQueue.Disconnect();

