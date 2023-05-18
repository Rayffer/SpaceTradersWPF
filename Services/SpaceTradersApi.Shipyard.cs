using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

using SpaceTradersWPF.ApiModels;

namespace SpaceTradersWPF.Services;

internal partial class SpaceTradersApi
{
    private readonly string GetShipyardWaypointResource = "/systems/{0}/waypoints/{1}/shipyard";

    public async Task<Shipyard> GetShipyard(string waypointSymbol)
    {
        var systemSymbol = ExtractSystemSymbol(waypointSymbol);
        var request = new RestRequest(string.Format(GetShipyardWaypointResource, systemSymbol, waypointSymbol));
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Shipyard>>(response.Content).Data;
    }
}