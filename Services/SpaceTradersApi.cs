using System;
using System.Threading.Tasks;

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

    public async Task<Agent> GetAgent()
    {
        var request = new RestRequest("my/agent", Method.Get);
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Agent>>(response.Content).data;
    }

    public void SetAccessTokenHeader(string token)
    {
        accessToken = token;
        restClient.DefaultParameters.RemoveParameter("Authorization", ParameterType.HttpHeader);
        restClient.AddDefaultHeader("Authorization", $"Bearer {accessToken}");
    }
}