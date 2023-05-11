using System;

using Newtonsoft.Json;

using RestSharp;

using SpaceTradersWPF.Models;

namespace SpaceTradersWPF.Services;

internal class SpaceTradersApi : ISpaceTradersApi
{
    private readonly IRestClient restClient;
    private string accessToken;

    public SpaceTradersApi(IRestClient restClient)
    {
        this.restClient = restClient;
    }

    public AgentResponse RegisterAgent(string symbol, string faction)
    {
        var request = new RestRequest("register", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        var body = JsonConvert.SerializeObject(new { symbol, faction });
        request.AddParameter("application/json", body, ParameterType.RequestBody);
        var response = restClient.ExecuteAsync(request).Result;
        Console.WriteLine(response.Content);

        return JsonConvert.DeserializeObject<AgentResponse>(response.Content);
    }

    public void SetAccessTokenHeader(string token)
    {
        accessToken = token;
        restClient.DefaultParameters.RemoveParameter("Authorization", ParameterType.HttpHeader);
        restClient.AddDefaultHeader("Authorization", $"Bearer {accessToken}");
    }
}