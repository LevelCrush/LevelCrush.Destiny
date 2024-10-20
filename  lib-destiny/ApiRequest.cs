using System.Collections.Concurrent;
using System.Net;
using System.Text;
using System.Web;
using Destiny.Converters;
using Destiny.Models.Enums;
using Destiny.Models.Responses;
using RestSharp;

namespace Destiny;

public class ApiRequest<TRequestBody> where TRequestBody : class
{
    private readonly string _apiKey;
    private readonly RestClient _client;
    private readonly ConcurrentBag<DestinyComponentType> _components;

    private readonly string _endPoint;
    private readonly ConcurrentDictionary<DestinyRouteParam, string> _params;
    private readonly ConcurrentDictionary<string, string> _queries;

    private readonly ushort _retriesMax;
    private TRequestBody? _body;

    private BungieRequestBodyType _bodyType;
    private BungieRequestMethod _method;

    private ushort _retries;

    public ApiRequest(string endPoint, string apiKey, RestClient client) : this(endPoint, apiKey, 3, client)
    {
    }

    public ApiRequest(string endPoint, string apiKey, ushort retriesMax, RestClient client)
    {
        _endPoint = endPoint;
        _apiKey = apiKey;
        _client = client;

        _components = [];
        _params = new ConcurrentDictionary<DestinyRouteParam, string>();
        _queries = new ConcurrentDictionary<string, string>();
        _bodyType = BungieRequestBodyType.JSON;

        _retries = 0;
        _retriesMax = retriesMax;

        _body = null;
    }


    /// <summary>
    ///     Sets either a GET or a POST method
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public ApiRequest<TRequestBody> Method(BungieRequestMethod method)
    {
        _method = method;
        return this;
    }

    /// <summary>
    ///     Sets the body of the request to bungie
    /// </summary>
    /// <param name="body">The body object that will be serialized</param>
    /// <param name="bodyType">The type of body. Default is JSON</param>
    /// <returns></returns>
    public ApiRequest<TRequestBody> Body(TRequestBody? body,
        BungieRequestBodyType bodyType = BungieRequestBodyType.JSON)
    {
        _body = body;
        _bodyType = bodyType;
        return this;
    }

    /// <summary>
    ///     Adds a component to the request. not all request require components. Consult bungie documentation
    /// </summary>
    /// <param name="componentType"></param>
    /// <returns></returns>
    public ApiRequest<TRequestBody> Component(DestinyComponentType componentType)
    {
        _components.Add(componentType);
        return this;
    }

    /// <summary>
    ///     Add a route param value into the request.
    ///     Route params are taken from the doc and when looking at routes they contain things like {mType} {membershipId}
    /// </summary>
    /// <param name="routeParam"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public ApiRequest<TRequestBody> Param(DestinyRouteParam routeParam, string value)
    {
        _params.AddOrUpdate(routeParam, value, (k, v) => value);
        return this;
    }

    /// <summary>
    ///     inserts a query variable into the request
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public ApiRequest<TRequestBody> Query(string key, string value)
    {
        _queries.AddOrUpdate(key, value, (k, v) => value);
        return this;
    }

    private async Task<BungieResponse<TResponse>?> SendOnce<TResponse>() where TResponse : class
    {
        var paramConverter = new DestinyRouteParamConverter();
        var endpointBuilder = new StringBuilder(_endPoint);
        foreach (var (paramKey, paramValue) in _params)
        {
            var conversion = paramConverter.ConvertTo(paramKey, typeof(string));
            if (conversion != null)
            {
                endpointBuilder.Replace((string)conversion, HttpUtility.UrlEncode(paramValue));
            }
        }

        var endpoint = endpointBuilder.ToString();
        if (!endpoint.Contains("http"))
        {
            endpoint = $"https://www.bungie.net/Platform{endpoint}";
        }

        var req = new RestRequest(endpoint, _method switch
        {
            BungieRequestMethod.Get => RestSharp.Method.Get,
            BungieRequestMethod.Post => RestSharp.Method.Post,
            _ => RestSharp.Method.Get
        });

        req.AddHeader("X-API-KEY", _apiKey);

        if (!_components.IsEmpty)
        {
            var flatten = string.Join(",", _components);
            req.AddQueryParameter("components", flatten);
        }

        if (!_queries.IsEmpty)
        {
            foreach (var (queryKey, queryValue) in _queries)
            {
                var fieldKey = HttpUtility.UrlEncode(queryKey);
                req.AddQueryParameter(fieldKey, queryValue);
            }
        }


        if (_body != null)
        {
            if (_bodyType == BungieRequestBodyType.JSON)
            {
                req.AddJsonBody(_body);
            }
            else
            {
                req.AddBody(_body);
            }
        }


        var res = await _client.ExecuteAsync<BungieResponse<TResponse>>(req);
        // our request **should** be auto followed but sometimes restsharp does not auto follow for some reason
        // this handles the redirects that Bungie has in place for some of their endpoints like stats.
        // there could be some optimization before this where if we know the endpoint coming in before hand we can just swap it out on other similiar endpoint calls
        if (res.Headers != null && res.StatusCode == HttpStatusCode.MovedPermanently) 
        {
            var redirect = res.Headers.First(headerParameter => headerParameter.Name == "Location").Value
                .Replace("http://", "https://");
            
            req.Resource = redirect;
            
            res = await _client.ExecuteAsync<BungieResponse<TResponse>>(req);
        }
        return res.IsSuccessful ? res.Data : null;
    }

    public async Task<BungieResponse<TResponse>?> Send<TResponse>() where TResponse : class
    {
        _retries = 0;
        do
        {
            var res = await SendOnce<TResponse>();

            if (res == null)
            {
                throw new Exception($"Failed to capture/deserialize response to '{_endPoint}'");
            }

            if (res.IsThrottled() && _retries < _retriesMax)
            {
                _retries++;
                await Task.Delay(TimeSpan.FromSeconds(res.ThrottleSeconds + 2));
            }
            else
            {
                return res;
            }
        } while (_retries < _retriesMax);

        return null;
    }
}