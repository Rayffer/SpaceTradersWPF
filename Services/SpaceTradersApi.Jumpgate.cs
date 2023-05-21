using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.ApiModels.Responses;

namespace SpaceTradersWPF.Services;

internal partial class SpaceTradersApi
{
    private readonly string GetJumpGateWaypointResource = "/systems/{0}/waypoints/{1}/jumpgate";

    public async Task<JumpGate> GetJumpGate(string waypointSymbol)
    {
        var systemSymbol = ExtractSystemSymbol(waypointSymbol);
        var request = new RestRequest(string.Format(this.GetJumpGateWaypointResource, systemSymbol, waypointSymbol));
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<JumpGate>>(response.Content).Data;
    }
}
