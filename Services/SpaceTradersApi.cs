using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

using SpaceTradersWPF.Models;

namespace SpaceTradersWPF.Services;

internal partial class SpaceTradersApi : ISpaceTradersApi
{
    private readonly IRestClient restClient;
    private string accessToken;

    public SpaceTradersApi(IRestClient restClient)
    {
        this.restClient = restClient;
    }

    public void SetAccessTokenHeader(string token)
    {
        accessToken = token;
        restClient.DefaultParameters.RemoveParameter("Authorization", ParameterType.HttpHeader);
        restClient.AddDefaultHeader("Authorization", $"Bearer {accessToken}");
    }
}