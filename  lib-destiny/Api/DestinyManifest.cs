using System.Collections.Concurrent;
using Destiny.Converters;
using Destiny.Models.Enums;
using Destiny.Models.Responses;
using RestSharp;

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
    
    public static async Task<T?> Get<T>(DestinyDefinition definition) where T: class
    {
        if (_manifestResponse == null)
        {
            await DestinyManifest.Get();
        }

        if (_manifestResponse == null)
        {
            return null;
        }

        var defConverter = new DestinyDefinitionConverter();
        var def = defConverter.ConvertTo(definition, typeof(string));
        if (def == null)
        {
            return null;
        }

        string converted = (string)def;

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
        var response = await BungieClient.Client.GetAsync<T>(request);
        
        return response;

    }
}