// See https://aka.ms/new-console-template for more information
using System.CommandLine;
using System.Runtime.InteropServices;
using Rasputin.MessageQueue.Models;
using Rasputin.MessageQueue.Queues;

// setup any cli options that we  will need
var targetAction =
    new Option<string>(
        name: "--action", 
        description: "Run Target action",
        getDefaultValue: () => "");


var rootCommand = new RootCommand("Run specific actions for rasputin");
rootCommand.AddOption(targetAction);
rootCommand.SetHandler((job) =>
{
    var j = job.Trim().ToLower();
    Console.WriteLine($"Sending: {j} action");
    QueueActions.Publish(new MessageAction()
    {
        Action = j,
        Entities = []
    });
}, targetAction);

// run the CLI commands 
await rootCommand.InvokeAsync(args);