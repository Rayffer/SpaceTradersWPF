using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

using SpaceTradersWPF.Models;

namespace SpaceTradersWPF.Services;

internal partial class SpaceTradersApi
{
    private readonly string GetMarketWaypointResource = "/systems/{0}/waypoints/{1}/market";

    public async Task<Market> GetMarket(string waypointSymbol)
    {
        var systemSymbol = ExtractSystemSymbol(waypointSymbol);
        var request = new RestRequest(string.Format(GetMarketWaypointResource, systemSymbol, waypointSymbol));
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Market>>(response.Content).Data;
    }
}