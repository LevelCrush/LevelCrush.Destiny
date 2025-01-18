using Destiny.Models.Bodies;
using Destiny.Models.Enums;
using RestSharp;

namespace Destiny;

public static class BungieClient
{

    internal static Queue<long> _attempts = new Queue<long>();
    
    public static RestClient Client { get; }

    public static string ApiKey { get; set; }

    
    static BungieClient()
    {
        Client = new RestClient(new RestClientOptions()
        {
            FollowRedirects = true,
        });
        ApiKey = "";
    }

    public static ApiRequest<BaseRequest> Get(string endpoint)
    {
        return Get<BaseRequest>(endpoint);
    }

    public static ApiRequest<TRequestBody> Get<TRequestBody>(string endpoint) where TRequestBody : class
    {
        return new ApiRequest<TRequestBody>(endpoint, ApiKey, Client).Method(BungieRequestMethod.Get);
    }

    public static ApiRequest<BaseRequest> Post(string endpoint)
    {
        return Post<BaseRequest>(endpoint);
    }

    public static ApiRequest<TRequestBody> Post<TRequestBody>(string endpoint) where TRequestBody : class
    {
        return new ApiRequest<TRequestBody>(endpoint, ApiKey, Client).Method(BungieRequestMethod.Post);
    }
}