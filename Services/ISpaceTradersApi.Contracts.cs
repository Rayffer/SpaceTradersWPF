using System.Threading.Tasks;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.ApiModels.Requests;
using SpaceTradersWPF.ApiModels.Responses;

namespace SpaceTradersWPF.Services;

internal partial interface ISpaceTradersApi
{
    Task<Contract> GetContract(string contractId);

    Task<Contract[]> GetContracts();

    Task<ContractResponse> PostAcceptContract(string contractId);

    Task<DeliverContractResponse> PostDeliverContract(string contractId, ContractDeliverRequest contractDeliverRequest);

    Task<ContractResponse> PostFulfillContract(string contractId);
}
