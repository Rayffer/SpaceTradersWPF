using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.ApiModels.Responses;

namespace SpaceTradersWPF.Services;

internal partial class SpaceTradersApi
{
    private readonly string GetFactionResource = "/factions/{0}";
    private readonly string GetFactionsResource = "/factions?page={0}&limit={1}";

    public async Task<Faction> GetFaction(string factionSymbol)
    {
        var request = new RestRequest(string.Format(GetFactionResource, factionSymbol));
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Faction>>(response.Content).Data;
    }

    public async Task<Faction[]> GetFactions(int pageNumber, int PageSize)
    {
        var request = new RestRequest(string.Format(GetFactionsResource, pageNumber, PageSize));
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Faction[]>>(response.Content).Data;
    }
}
