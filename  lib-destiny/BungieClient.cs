using Destiny.Models.Bodies;
using Destiny.Models.Enums;
using RestSharp;

namespace Destiny;

public static class BungieClient
{
    static BungieClient()
    {
        Client = new RestClient();
        ApiKey = "";
    }

    public static RestClient Client { get; }

    public static string ApiKey { get; set; }

    public static BungieRequest<BaseRequest> Get(string endpoint)
    {
        return Get<BaseRequest>(endpoint);
    }

    public static BungieRequest<TRequestBody> Get<TRequestBody>(string endpoint) where TRequestBody : class
    {
        return new BungieRequest<TRequestBody>(endpoint, ApiKey, Client).Method(BungieRequestMethod.Get);
    }

    public static BungieRequest<BaseRequest> Post(string endpoint)
    {
        return Post<BaseRequest>(endpoint);
    }

    public static BungieRequest<TRequestBody> Post<TRequestBody>(string endpoint) where TRequestBody : class
    {
        return new BungieRequest<TRequestBody>(endpoint, ApiKey, Client).Method(BungieRequestMethod.Post);
    }
}