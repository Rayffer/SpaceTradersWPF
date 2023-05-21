using System;
using System.Threading.Tasks;

using RestSharp;

using SpaceTradersWPF.Mappers;

namespace SpaceTradersWPF.Services;

internal partial class SpaceTradersApi : ISpaceTradersApi
{
    private readonly IRestClient restClient;
    private readonly ISpaceTradersApiMapper spaceTradersMapper;
    private string accessToken;

    public SpaceTradersApi(IRestClient restClient,
        ISpaceTradersApiMapper spaceTradersMapper)
    {
        this.restClient = restClient;
        this.spaceTradersMapper = spaceTradersMapper;
    }

    public void SetAccessTokenHeader(string token)
    {
        this.accessToken = token;
        this.restClient.DefaultParameters.RemoveParameter("Authorization", ParameterType.HttpHeader);
        this.restClient.AddDefaultHeader("Authorization", $"Bearer {this.accessToken}");
        this.restClient.AddDefaultHeader("Content-Type", "application/json");
        this.restClient.AddDefaultHeader("Accept", "application/json");
    }

    private static string ExtractSystemSymbol(string waypointSymbol)
    {
        var lastHyphenIndex = waypointSymbol.LastIndexOf('-');
        return waypointSymbol[..lastHyphenIndex];
    }

    private async Task<RestResponse> PerformRequest(RestRequest request)
    {
        var response = default(RestResponse);
        try
        {
            return await this.restClient.ExecuteAsync(request);
        }
        catch (Exception e)
        {
        }
        return response;
    }
}
