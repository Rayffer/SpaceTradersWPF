﻿using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.ApiModels.Requests;
using SpaceTradersWPF.ApiModels.Responses;

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
        var request = new RestRequest(string.Format(this.GetContractResource, contractId));
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Contract>>(response.Content).Data;
    }

    public async Task<Contract[]> GetContracts()
    {
        var request = new RestRequest(this.GetContractsResource);
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Contract[]>>(response.Content).Data;
    }

    public async Task<ContractResponse> PostAcceptContract(string contractId)
    {
        var request = new RestRequest(string.Format(this.PostAcceptContractResource, contractId), Method.Post);
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<ContractResponse>>(response.Content).Data;
    }

    public async Task<DeliverContractResponse> PostDeliverContract(string contractId, ContractDeliverRequest contractDeliverRequest)
    {
        var request = new RestRequest(string.Format(this.PostDeliverContractResource, contractId), Method.Post);
        request.AddJsonBody(contractDeliverRequest);

        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<DeliverContractResponse>>(response.Content).Data;
    }

    public async Task<ContractResponse> PostFulfillContract(string contractId)
    {
        var request = new RestRequest(string.Format(this.PostFulfillContractResource, contractId), Method.Post);
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<ContractResponse>>(response.Content).Data;
    }
}
