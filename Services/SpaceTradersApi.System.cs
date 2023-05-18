﻿using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

using SpaceTradersWPF.ApiModels.Responses;

namespace SpaceTradersWPF.Services;

internal partial class SpaceTradersApi
{
    private readonly string GetSystemsResource = "/systems?page={0}&limit={1}";
    private readonly string GetSystemResource = "/systems/{0}";

    public async Task<ApiModels.System> GetSystem(string waypointSymbol)
    {
        var systemSymbol = ExtractSystemSymbol(waypointSymbol);
        var request = new RestRequest(string.Format(GetSystemResource, systemSymbol));
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<ApiModels.System>>(response.Content).Data;
    }

    public async Task<ApiModels.System[]> GetSystems(int pageNumber, int PageSize)
    {
        var request = new RestRequest(string.Format(GetSystemsResource, pageNumber, PageSize));
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<ApiModels.System[]>>(response.Content).Data;
    }
}
