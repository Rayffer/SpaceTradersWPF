using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

using SpaceTradersWPF.Models;

namespace SpaceTradersWPF.Services;

internal partial class SpaceTradersApi : ISpaceTradersApi
{
    private readonly string GetSystemsResource = "/systems?page={0}&limit={1}";
    private readonly string GetSystemResource = "/systems/{0}";

    public async Task<Models.System> GetSystem(string waypointSymbol)
    {
        var systemSymbol = ExtractSystemSymbol(waypointSymbol);
        var request = new RestRequest(string.Format(GetSystemResource, systemSymbol));
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Models.System>>(response.Content).Data;
    }

    public async Task<Models.System[]> GetSystems(int pageNumber, int PageSize)
    {
        var request = new RestRequest(string.Format(GetSystemsResource, pageNumber, PageSize));
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Models.System[]>>(response.Content).Data;
    }
}