using System.Xml;
using Microsoft.Extensions.Logging;

namespace Rasputin.MessageQueue.Consumer;

public static class LoggerGlobal
{
    private static  ILoggerFactory _factory;
    private static  ILogger _default;
    static LoggerGlobal()
    {
        _factory = LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Information));
        _default = _factory.CreateLogger("Global");
    }
    
    public static ILogger Create(string name) 
    {
        return _factory.CreateLogger(name);
    }

    public static ILogger Raw()
    {
        return _default;
    }
    
    public static void Close()
    {
        _factory.Dispose();
    }

    public static void Write(string message, LogLevel level = LogLevel.Information)
    {
        _default.Log(level, message);
    }
}