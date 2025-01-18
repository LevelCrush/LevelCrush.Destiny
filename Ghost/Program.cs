// See https://aka.ms/new-console-template for more information

// load api configuration information

using System.CommandLine;
using Destiny;
using Destiny.Api;
using Destiny.Models.Manifests;
using Destiny.Models.Schemas;
using Ghost;

var destinyApiConfig = DestinyConfig.Load();
BungieClient.ApiKey = destinyApiConfig.ApiKey;

// setup any cli options that we  will need
var targetTask =
    new Option<string>(
        name: "--task", 
        description: "The target task to run",
        getDefaultValue: () => "");


var rootCommand = new RootCommand("Run a specific task");
rootCommand.AddOption(targetTask);
rootCommand.SetHandler((task) =>
{
    var t = task.Trim().ToLower();
    switch (t)
    {
        case "title-dump":
            LoggerGlobal.Write("Starting to dump titles");
            TitleDump().Wait();
            LoggerGlobal.Write("Done dumping titles");
            break;
        default:
            LoggerGlobal.Write($"Unknown task: {t}");
            break;
    }
}, targetTask);

await rootCommand.InvokeAsync(args);


async Task TitleDump()
{
    LoggerGlobal.Write("querying Destiny Manifest and records");
    var req = await DestinyManifest.Get<DestinyRecordDefinition>();

    var httpClient = new HttpClient();

    var titles = new Dictionary<string, int>();
    foreach (var (key, definition) in req)
    {
        // we only care about records that have a title
        if (definition.TitleInfo.HasTitle)
        {
            definition.TitleInfo.TitlesByGender.TryGetValue("Male", out var title);

            if (Directory.Exists("./dump"))
            {
                Directory.CreateDirectory("./dump");
            }
            
            var dirPath = $"./dump/{title ?? definition.Hash.ToString()}";

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            

            if (definition.DisplayProperties.Icon.Length > 0)
            {
                
                var extension = Path.GetExtension(definition.DisplayProperties.Icon);
                var filePath = $"./{dirPath}/{definition.Hash.ToString()}{extension}";
                var urlPath = $"https://bungie.net{definition.DisplayProperties.Icon}";
                LoggerGlobal.Write($"Downloading {urlPath} to {filePath}");
                await using (var stream = await httpClient.GetStreamAsync(urlPath))
                {
                    await using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        await stream.CopyToAsync(fs);
                    }
                    
                }
            }

            if (titles.ContainsKey(title))
            {
                titles[title]++;
            }
            else
            {
                titles.TryAdd(title, 1);
            }
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
    
    httpClient.Dispose();
    
    LoggerGlobal.Write($"Downloaded {titles.Count} unique titles");
}


LoggerGlobal.Write("Done.");