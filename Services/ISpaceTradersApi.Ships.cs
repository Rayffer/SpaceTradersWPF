using System.Threading.Tasks;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.ApiModels.Requests;
using SpaceTradersWPF.ApiModels.Responses;
using SpaceTradersWPF.Types;

namespace SpaceTradersWPF.Services;

internal partial interface ISpaceTradersApi
{
    Task<ShipPurchaseResponse> PostShipPurchase(ShipTypes shipTypes, string waypointSymbol);

    Task<ShipNavigationInformation> PostShipOrbit(string shipSymbol);

    Task<ShipRefineResponse> PostShipRefine(string shipSymbol, string oreToRefine);

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

    Task<Ship> GetShip(string shipSymbol);

    Task<Contract> PostShipNegotiateContract(string shipSymbol);
}
