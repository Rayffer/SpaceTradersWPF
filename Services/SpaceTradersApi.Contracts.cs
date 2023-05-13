using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

using SpaceTradersWPF.Models;

namespace SpaceTradersWPF.Services;

internal partial class SpaceTradersApi
{
    private readonly string GetContractsResource = "/my/contracts";
    private readonly string GetContractResource = "/my/contracts/{0}";
    private readonly string PostAcceptContractResource = "/my/contracts/{0}/accept";
    private readonly string PostDeliverContractResource = "/my/contracts/{0}/deliver";
    private readonly string PostFulfillContractResource = "/my/contracts/{0}/fulfill";

    public async Task<Contract> GetContract(string contractId)
    {
        var request = new RestRequest(string.Format(GetContractResource, contractId));
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Contract>>(response.Content).Data;
    }

    public async Task<Contract[]> GetContracts()
    {
        var request = new RestRequest(GetContractsResource);
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Contract[]>>(response.Content).Data;
    }

    public async Task<(Contract contract, Agent agent)> AcceptContract(string contractId)
    {
        var request = new RestRequest(string.Format(PostAcceptContractResource, contractId), Method.Post);
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<(Contract contract, Agent agent)>>(response.Content).Data;
    }

    public async Task<(Contract contract, Agent agent)> DeliverContract(string contractId)
    {
        var request = new RestRequest(string.Format(PostDeliverContractResource, contractId), Method.Post);
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<(Contract contract, Agent agent)>>(response.Content).Data;
    }

    public async Task<(Contract contract, Agent agent)> FulfillContract(string contractId)
    {
        var request = new RestRequest(string.Format(PostFulfillContractResource, contractId), Method.Post);
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<(Contract contract, Agent agent)>>(response.Content).Data;
    }
}