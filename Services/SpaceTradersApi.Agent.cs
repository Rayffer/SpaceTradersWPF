using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.ApiModels.Responses;

namespace SpaceTradersWPF.Services;

internal partial class SpaceTradersApi
{
    private readonly string GetAgentResource = "my/agent";

    public async Task<AgentResponse> RegisterAgent(string symbol, string faction)
    {
        var request = new RestRequest("register", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        var body = JsonConvert.SerializeObject(new { symbol, faction });
        request.AddParameter("application/json", body, ParameterType.RequestBody);
        var response = await this.restClient.ExecuteAsync(request);
        Console.WriteLine(response.Content);

        return JsonConvert.DeserializeObject<ApiResponse<AgentResponse>>(response.Content).Data;
    }

    public async Task<Agent> GetAgent()
    {
        var request = new RestRequest(this.GetAgentResource, Method.Get);
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Agent>>(response.Content).Data;
    }
}
