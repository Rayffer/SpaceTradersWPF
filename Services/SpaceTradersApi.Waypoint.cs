using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.ApiModels.Responses;

namespace SpaceTradersWPF.Services;

internal partial class SpaceTradersApi
{
    private readonly string GetSystemWaypointsResource = "/systems/{0}/waypoints?page={1}&limit={2}";
    private readonly string GetWaypointResource = "/systems/{0}/waypoints/{1}";

    public async Task<Waypoint> GetWaypoint(string waypointSymbol)
    {
        var systemSymbol = ExtractSystemSymbol(waypointSymbol);
        var request = new RestRequest(string.Format(this.GetWaypointResource, systemSymbol, waypointSymbol));
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Waypoint>>(response.Content).Data;
    }

    public async Task<Waypoint[]> GetWaypoints(string waypointSymbol, int pageNumber, int pageSize)
    {
        var systemSymbol = waypointSymbol;
        if (waypointSymbol.Count(x => x.Equals("-")) > 1)
        {
            systemSymbol = ExtractSystemSymbol(waypointSymbol);
        }
        var request = new RestRequest(string.Format(this.GetSystemWaypointsResource, systemSymbol, pageNumber, pageSize));
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Waypoint[]>>(response.Content).Data;
    }
}
