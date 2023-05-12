using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

using SpaceTradersWPF.Models;

namespace SpaceTradersWPF.Services;

internal partial class SpaceTradersApi : ISpaceTradersApi
{
    private string Waypoint = "/systems/{0}/waypoints/{1}";

    public async Task<Waypoint> GetWaypoint(string waypointSymbol)
    {
        var lastHyphenIndex = waypointSymbol.LastIndexOf('-');
        var symbols = new string[] { waypointSymbol.Substring(0, lastHyphenIndex), waypointSymbol.Substring(lastHyphenIndex + 1) };
        var request = new RestRequest(string.Format(Waypoint, symbols[0], waypointSymbol));
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Waypoint>>(response.Content).data;
    }
}