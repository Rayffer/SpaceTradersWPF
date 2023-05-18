using System.Threading.Tasks;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.ApiTypes;

namespace SpaceTradersWPF.Services;

internal interface ISpaceTradersApi
{
    void SetAccessTokenHeader(string token);

    AgentResponse RegisterAgent(string symbol, string faction);

    Task<Agent> GetAgent();

    Task<Contract> GetContract(string contractId);

    Task<Contract[]> GetContracts();

    Task<(Contract contract, Agent agent)> PostAcceptContract(string contractId);

    Task<(Contract contract, Agent agent)> PostDeliverContract(string contractId);

    Task<(Contract contract, Agent agent)> PostFulfillContract(string contractId);

    Task<JumpGate> GetJumpGate(string waypointSymbol);

    Task<Market> GetMarket(string waypointSymbol);

    Task<Shipyard> GetShipyard(string waypointSymbol);

    Task<Waypoint> GetWaypoint(string symbol);

    Task<Waypoint> GetWaypoints(string waypointSymbol, int pageNumber, int pageSize);

    Task<ApiModels.System> GetSystem(string waypointSymbol);

    Task<ApiModels.System[]> GetSystems(int pageNumber, int PageSize);

    Task<Faction> GetFaction(string factionSymbol);

    Task<Faction[]> GetFactions(int pageNumber, int PageSize);

    Task<Ship> GetShip(string shipSymbol);

    Task<(Agent agent, Ship ship, ShipyardTransaction transaction)> PostShipPurchase(ShipTypes shipTypes, string waypointSymbol);

    Task<ShipNavigationInformation> PostShipOrbit(string shipSymbol);

    Task<ShipNavigationInformation> PostShipRefine(string shipSymbol, TradeSymbols tradeSymbol);

    Task<ShipNavigationInformation> PostShipCreateChart(string shipSymbol);

    Task<ShipNavigationInformation> PostShipDock(string shipSymbol);

    Task<(ShipCooldown cooldown, Survey survey)> PostShipCreateSurvey(string shipSymbol);

    Task<(ShipCooldown cooldown, Extraction extraction, ShipCargo cargo)> PostShipExtractResources(string shipSymbol);

    Task<ShipNavigationInformation> PostShipJettisonCargo(string shipSymbol);

    Task<(ShipCooldown cooldown, ShipNavigationInformation navigationInformation)> PostShipJump(string shipSymbol);

    Task<(ShipFuel fuel, ShipNavigationInformation navigationInformation)> PostShipNavigate(string shipSymbol);

    Task<ShipNavigationInformation> PatchShipNavigation(string shipSymbol);

    Task<(ShipFuel fuel, ShipNavigationInformation navigationInformation)> PostShipWarp(string shipSymbol);

    Task<(Agent agent, ShipCargo cargo, MarketTransaction transaction)> PostShipSellCargo(string shipSymbol);

    Task<(ShipCooldown cooldown, ApiModels.System[] systems)> PostShipScanSystems(string shipSymbol);

    Task<(ShipCooldown cooldown, Waypoint[] waypoints)> PostShipScanWaypoints(string shipSymbol);

    Task<(ShipCooldown cooldown, Ship[] ships)> PostShipScanShips(string shipSymbol);

    Task<(Agent agent, ShipFuel fuel)> PostShipRefuel(string shipSymbol);

    Task<(Agent agent, ShipCargo cargo, MarketTransaction transaction)> PostShipPurchaseCargo(string shipSymbol);

    Task<ShipCargo> PostShipTransferCargo(string shipSymbol);

    Task<ShipCargo> GetShipCargo(string shipSymbol);

    Task<ShipCooldown> GetShipCooldown(string shipSymbol);

    Task<ShipNavigationInformation> GetShipNavigationInformation(string shipSymbol);

    Task<Ship[]> GetShips(int pageNumber, int PageSize);
}