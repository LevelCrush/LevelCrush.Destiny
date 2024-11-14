// See https://aka.ms/new-console-template for more information
using System.CommandLine;
using System.Runtime.InteropServices;
using System.Text.Json;
using Destiny;
using Rasputin_Redis;
using Rasputin.MessageQueue;
using Rasputin.MessageQueue.Consumer;
using Rasputin.MessageQueue.Queues;


// load api configuration information
var destinyApiConfig = DestinyConfig.Load();
BungieClient.ApiKey = destinyApiConfig.ApiKey;

var redis = RasputinRedis.Connect();

// setup any cli options that we  will need
var targetQueueOption =
    new Option<string>(
        name: "--queue", 
        description: "The target queue that this consumer will run for",
        getDefaultValue: () => "");


var rootCommand = new RootCommand("Utilize Rasputin and consume in the background");
rootCommand.AddOption(targetQueueOption);
rootCommand.SetHandler((queue) =>
{

    var q = queue.Trim().ToLower();

    switch (q)
    {
        case "member":
            LoggerGlobal.Write("Running consumer for member data");
            ConsumeMemberQueue();
            break;
        case "clan":
            LoggerGlobal.Write("Running consumer for clan data");
            ConsumeClanQueue();
            break;
        case "db":
        case "database":
            LoggerGlobal.Write("Running consumer for db sync data");
            ConsumeDbQueue();
            break;
        case "actions":
            LoggerGlobal.Write("Running consumer for actions data");
            ConsumeActionQueue();
            break;
        case "instance":
        default:
            LoggerGlobal.Write("Running consumer for instance data");
            ConsumeInstanceQueue();
            break;
    }

}, targetQueueOption);


// run the CLI commands 
await rootCommand.InvokeAsync(args);

// handle CTRL+C 
LoggerGlobal.Write("Setting up CTLR+C hook");
var exitEvent = new ManualResetEvent(false);
Console.CancelKeyPress += (sender, e) =>
{
    e.Cancel = true;
    exitEvent.Set();
};

LoggerGlobal.Write("Setting up POSIX signal handlers");

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


LoggerGlobal.Write("Waiting to consume");
    
// until we receive the exit event , keep waiting
exitEvent.WaitOne();

LoggerGlobal.Write("Closing out and releasing");

// close all connections
// this will also close all consumers and channels automatically
RasputinMessageQueue.Disconnect();
await RasputinRedis.Close();

LoggerGlobal.Write("Done");

LoggerGlobal.Close();
return;

void ConsumeDbQueue()
{
    QueueDBSync.Subscribe(async (message) =>
    {
        try
        {
            if (message != null)
            {
                LoggerGlobal.Write($"DB sync message received. Type: {message.Task}");
                await ConsumerDbSync.Process(message);
            }
            else
            {
                LoggerGlobal.Write("Received a null db message. Deserialization may of gone wrong");
            }
        }
        catch (Exception e)
        {
            LoggerGlobal.Write($"An exception has occurred. {e.Message}");
        }
    });
}

void ConsumeActionQueue()
{
    QueueActions.Subscribe(async (message) =>
    {
        try
        {
            if (message != null)
            {
                LoggerGlobal.Write($"DB sync message received. Type: {message.Action}");
                await ConsumerActions.Process(message);
            }
            else
            {
                LoggerGlobal.Write("Received a null db message. Deserialization may of gone wrong");
            }
        }
        catch (Exception e)
        {
            LoggerGlobal.Write($"An exception has occurred. {e.Message}");
        }
    });
}

void ConsumeInstanceQueue()
{
    QueueInstance.Subscribe(async (message) =>
    {
        try
        {
            if (message != null)
            {
                LoggerGlobal.Write(
                    $"Instance Message received. Processing: {JsonSerializer.Serialize(message.Entities)}");
                await ConsumerInstance.Process(message);
            }
            else
            {
                LoggerGlobal.Write("Received a null instance. Deserialization may of gone wrong");
            }
        }
        catch (Exception e)
        {
            LoggerGlobal.Write($"An exception has occurred. {e.Message}");
        }
    });
}

void ConsumeClanQueue()
{
    QueueClan.Subscribe(async (message) =>
    {
        try
        {
            if (message != null)
            {
                LoggerGlobal.Write($"Clan Message received. Processing: {JsonSerializer.Serialize(message.Entities)}");
                await ConsumerClan.Process(message);
            }
            else
            {
                LoggerGlobal.Write("Received a null clan. Deserialization may of gone wrong");
            }
        }
        catch (Exception e)
        {
            LoggerGlobal.Write($"An exception has occurred. {e.Message}");
        }
    });
}

void ConsumeMemberQueue()
{
    QueueMember.Subscribe(async (message) =>
    {
        try
        {
            if (message != null)
            {
                LoggerGlobal.Write(
                    $"Member Message received. Processing: {JsonSerializer.Serialize(message.Entities)}");
                await ConsumerMember.Process(message);
            }
            else
            {
                LoggerGlobal.Write("Received a null member. Deserialization may of gone wrong");
            }
        }
        catch (Exception e)
        {
            LoggerGlobal.Write($"An exception has occurred. {e.Message}");
        }
    });
}

