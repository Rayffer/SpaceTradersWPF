using System.Threading.Tasks;

using SpaceTradersWPF.Models;

namespace SpaceTradersWPF.Services;

internal interface ISpaceTradersApi
{
    void SetAccessTokenHeader(string token);

    AgentResponse RegisterAgent(string symbol, string faction);

    Task<Agent> GetAgent();

    Task<Contract> GetContract(string contractId);

    Task<Contract[]> GetContracts();

    Task<(Contract contract, Agent agent)> AcceptContract(string contractId);

    Task<(Contract contract, Agent agent)> DeliverContract(string contractId);

    Task<(Contract contract, Agent agent)> FulfillContract(string contractId);

    Task<JumpGate> GetJumpGate(string waypointSymbol);

    Task<Market> GetMarket(string waypointSymbol);

    Task<Shipyard> GetShipyard(string waypointSymbol);

    Task<Waypoint> GetWaypoint(string symbol);

    Task<Waypoint> GetWaypoints(string waypointSymbol, int pageNumber, int pageSize);

    Task<Models.System> GetSystem(string waypointSymbol);

    Task<Models.System[]> GetSystems(int pageNumber, int PageSize);
}