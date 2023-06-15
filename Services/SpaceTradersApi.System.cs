using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.ApiModels.Responses;

namespace SpaceTradersWPF.Services;

internal partial class SpaceTradersApi
{
    private readonly string GetSystemsResource = "/systems?page={0}&limit={1}";
    private readonly string GetSystemResource = "/systems/{0}";

    public async Task<SolarSystem> GetSystem(string waypointSymbol)
    {
        var systemSymbol = ExtractSystemSymbol(waypointSymbol);
        var request = new RestRequest(string.Format(this.GetSystemResource, systemSymbol));
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<ApiModels.SolarSystem>>(response.Content).Data;
    }

    public async Task<SolarSystem[]> GetSystems(int pageNumber, int PageSize)
    {
        var request = new RestRequest(string.Format(this.GetSystemsResource, pageNumber, PageSize));
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<ApiModels.SolarSystem[]>>(response.Content).Data;
    }
}
