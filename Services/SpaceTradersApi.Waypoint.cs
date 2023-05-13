﻿using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

using SpaceTradersWPF.Models;

namespace SpaceTradersWPF.Services;

internal partial class SpaceTradersApi
{
    private readonly string GetSystemWaypointsResource = "/systems/{systemSymbol}/waypoints?page={0}&limit={1}";
    private readonly string GetWaypointResource = "/systems/{0}/waypoints/{1}";

    public async Task<Waypoint> GetWaypoint(string waypointSymbol)
    {
        var systemSymbol = ExtractSystemSymbol(waypointSymbol);
        var request = new RestRequest(string.Format(GetWaypointResource, systemSymbol, waypointSymbol));
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Waypoint>>(response.Content).Data;
    }

    public async Task<Waypoint> GetWaypoints(string waypointSymbol, int pageNumber, int pageSize)
    {
        var lastHyphenIndex = waypointSymbol.LastIndexOf('-');
        var symbols = new string[] { waypointSymbol.Substring(0, lastHyphenIndex), waypointSymbol.Substring(lastHyphenIndex + 1) };
        var request = new RestRequest(string.Format(GetSystemWaypointsResource, symbols[0], pageNumber, pageSize));
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Waypoint>>(response.Content).Data;
    }
}