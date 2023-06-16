using System.Threading.Tasks;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.ApiModels.Responses;

namespace SpaceTradersWPF.Services;

internal partial interface ISpaceTradersApi
{
    Task<AgentResponse> RegisterAgent(string symbol, string faction);

    Task<Agent> GetAgent();
}
