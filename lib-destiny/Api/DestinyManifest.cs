using System.Collections.Concurrent;
using System.Reflection;
using Destiny.Attributes;
using Destiny.Converters;
using Destiny.Models.Enums;
using Destiny.Models.Responses;
using RestSharp;
using RestSharp.Extensions;

namespace Destiny.Api;

public static class DestinyManifest
{

    private static DestinyManifestResponse? _manifestResponse = null; 
    
    private static async Task Get()
    {
        var request = await BungieClient.Get("/Destiny2/Manifest").Send <
            DestinyManifestResponse > ();
        
        if (request != null && request.Response != null)
        {
            _manifestResponse = request.Response;
        }
    }
    
    public static async Task<ConcurrentDictionary<string, T>?> Get<T>() where T: class, new()
    {
        if (_manifestResponse == null)
        {
            await DestinyManifest.Get();
        }

        if (_manifestResponse == null)
        {
            return null;
        }

        var def = typeof(T).GetCustomAttribute(typeof(DestinyDefinitionAttribute), true);
        if (def == null)
        {
            return null;
        }

        string converted = ((DestinyDefinitionAttribute)def).DefinitionName;

        _manifestResponse.JsonWorldComponentContentPath.TryGetValue(_manifestResponse.Locale == null
                ? "en"
                : _manifestResponse.Locale, out var localeMap);

        string? contentPath = "";
        localeMap?.TryGetValue(converted, out  contentPath);


        if (string.IsNullOrEmpty(contentPath))
        {
            return null;
        }
        
        var endpoint = $"https://bungie.net{contentPath}";
        var request = new RestRequest(endpoint, Method.Get);
        var response = await BungieClient.Client.GetAsync<ConcurrentDictionary<string, T>>(request);
        
        return response;

    }
}