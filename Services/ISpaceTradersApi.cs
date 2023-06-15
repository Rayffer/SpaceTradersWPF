using System.Threading.Tasks;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.ApiModels.Requests;
using SpaceTradersWPF.ApiModels.Responses;
using SpaceTradersWPF.Types;

namespace SpaceTradersWPF.Services;

internal interface ISpaceTradersApi
{
    void ClearAccessToken();

    void SetAccessTokenHeader(string token);

    Task<AgentResponse> RegisterAgent(string symbol, string faction);

    Task<Agent> GetAgent();

    Task<Contract> GetContract(string contractId);

    Task<Contract[]> GetContracts();

    Task<ContractResponse> PostAcceptContract(string contractId);

    Task<DeliverContractResponse> PostDeliverContract(string contractId, ContractDeliverRequest contractDeliverRequest);

    Task<ContractResponse> PostFulfillContract(string contractId);

    Task<JumpGate> GetJumpGate(string waypointSymbol);

    Task<Market> GetMarket(string waypointSymbol);

    Task<Shipyard> GetShipyard(string waypointSymbol);

    Task<Waypoint> GetWaypoint(string symbol);

    Task<Waypoint[]> GetWaypoints(string waypointSymbol, int pageNumber, int pageSize);

    Task<SolarSystem> GetSystem(string waypointSymbol);

    Task<SolarSystem[]> GetSystems(int pageNumber, int pageSize);

    Task<Faction> GetFaction(string factionSymbol);

    Task<Faction[]> GetFactions(int pageNumber, int pageSize);

    Task<Ship> GetShip(string shipSymbol);

    Task<ShipPurchaseResponse> PostShipPurchase(ShipTypes shipTypes, string waypointSymbol);

    Task<ShipNavigationInformation> PostShipOrbit(string shipSymbol);

    Task<ShipNavigationInformation> PostShipRefine(string shipSymbol, TradeSymbols tradeSymbol);

    Task<ChartResponse> PostShipCreateChart(string shipSymbol);

    Task<ShipNavigationInformation> PostShipDock(string shipSymbol);

    Task<SurveyResponse> PostShipCreateSurvey(string shipSymbol);

    Task<ExtractionResponse> PostShipExtractResources(string shipSymbol, string waypointSymbol);

    Task<CargoJettisonResponse> PostShipJettisonCargo(string shipSymbol, PostShipJettisonCargoRequest shipJettisonCargoRequest);

    Task<JumpResponse> PostShipJump(string shipSymbol, string systemSymbol);

    Task<NavigationResponse> PostShipNavigate(string shipSymbol, string waypointSymbol);

    Task<ShipNavigationInformation> PatchShipNavigation(string shipSymbol, PatchShipNavigationRequestModel shipNavigationRequestModel);

    Task<WarpResponse> PostShipWarp(string shipSymbol, string waypointSymbol);

    Task<CargoTransactionResponse> PostShipSellCargo(string shipSymbol, PostShipSellCargoRequest shipSellCargoRequest);

    Task<ScanSystemsResponse> PostShipScanSystems(string shipSymbol);

    Task<ScanWaypointsResponse> PostShipScanWaypoints(string shipSymbol);

    Task<ScanShipsResponse> PostShipScanShips(string shipSymbol);

    Task<RefuelResponse> PostShipRefuel(string shipSymbol);

    Task<CargoTransactionResponse> PostShipPurchaseCargo(string shipSymbol, PostShipPurchaseCargoRequest shipPurchaseCargoRequest);

    Task<ShipCargo> PostShipTransferCargo(string shipSymbol, PostShipTransferCargoRequest shipTransferCargoRequest);

    Task<ShipCargo> GetShipCargo(string shipSymbol);

    Task<ShipCooldown> GetShipCooldown(string shipSymbol);

    Task<ShipNavigationInformation> GetShipNavigationInformation(string shipSymbol);

    Task<Ship[]> GetShips(int pageNumber, int pageSize);
}
