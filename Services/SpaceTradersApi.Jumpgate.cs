using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

using SpaceTradersWPF.Models;

namespace SpaceTradersWPF.Services;

internal partial class SpaceTradersApi
{
    private readonly string GetJumpGateWaypointResource = "/systems/{0}/waypoints/{1}/market";

    public async Task<JumpGate> GetJumpGate(string waypointSymbol)
    {
        var systemSymbol = ExtractSystemSymbol(waypointSymbol);
        var request = new RestRequest(string.Format(GetJumpGateWaypointResource, systemSymbol, waypointSymbol));
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<JumpGate>>(response.Content).Data;
    }
}