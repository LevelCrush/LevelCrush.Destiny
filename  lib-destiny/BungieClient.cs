using Destiny.Models.Bodies;
using Destiny.Models.Enums;
using RestSharp;

namespace Destiny;

public static class BungieClient
{
    static BungieClient()
    {
        Client = new RestClient(new RestClientOptions()
        {
            FollowRedirects = true,
        });
        ApiKey = "";

        // expliclity set this
       // Client.Options.FollowRedirects = true;
        
    }

    public static RestClient Client { get; }

    public static string ApiKey { get; set; }

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