using System.Threading.Tasks;

using SpaceTradersWPF.ApiModels;

namespace SpaceTradersWPF.Services;

internal partial interface ISpaceTradersApi
{
    Task<JumpGate> GetJumpGate(string waypointSymbol);
}
