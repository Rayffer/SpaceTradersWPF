using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

using SpaceTradersWPF.Extensions;
using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.ApiModels.Requests;
using SpaceTradersWPF.Types;

namespace SpaceTradersWPF.Services;

internal partial class SpaceTradersApi
{
    private readonly string GetShipsResource = "/my/ships?page={0}&limit={1}";
    private readonly string GetShipResource = "/my/ships/{0}";
    private readonly string GetShipCargoResource = "/my/ships/{0}/cargo";
    private readonly string GetShipCooldownResource = "/my/ships/{0}/cooldown";
    private readonly string GetShipNavigationResource = "/my/ships/{0}/nav";
    private readonly string PostPurchaseShipResource = "/my/ships";
    private readonly string PostShipOrbitResource = "/my/ships/{0}/orbit";
    private readonly string PostShipRefineResource = "/my/ships/{0}/refine";
    private readonly string PostShipChartResource = "/my/ships/{0}/chart";
    private readonly string PostShipDockResource = "/my/ships/{0}/dock";
    private readonly string PostShipSurveyResource = "/my/ships/{0}/survey";
    private readonly string PostShipExtractResource = "/my/ships/{0}/extract";
    private readonly string PostShipJettisonResource = "/my/ships/{0}/jettison";
    private readonly string PostShipJumpResource = "/my/ships/{0}/jump";
    private readonly string PostShipNavigateResource = "/my/ships/{0}/navigate";
    private readonly string PostShipWarpResource = "/my/ships/{0}/warp";
    private readonly string PostShipSellCargoResource = "/my/ships/{0}/sell";
    private readonly string PostShipScanSystemsResource = "/my/ships/{0}/scan/systems";
    private readonly string PostShipScanWaypointsResource = "/my/ships/{0}/scan/waypoints";
    private readonly string PostShipScanShipsResource = "/my/ships/{0}/scan/ships";
    private readonly string PostShipRefuelResource = "/my/ships/{0}/refuel";
    private readonly string PostShipPurchaseCargoResource = "/my/ships/{0}/purchase";
    private readonly string PostShipTransferCargoResource = "/my/ships/{0}/transfer";
    private readonly string PatchShipNavigationInformationResource = "/my/ships/{0}/nav";

    public async Task<Ship> GetShip(string shipSymbol)
    {
        var request = new RestRequest(string.Format(GetShipResource, shipSymbol));
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Ship>>(response.Content).Data;
    }

    public async Task<(Agent agent, Ship ship, ShipyardTransaction transaction)> PostShipPurchase(ShipTypes shipTypes, string waypointSymbol)
    {
        var request = new RestRequest(string.Format(PostPurchaseShipResource), Method.Post);
        request.AddBody(new ShipPurchaseRequestModel
        {
            ShipType = shipTypes.ToApiString(),
            WaypointSymbol = waypointSymbol
        });
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<(Agent agent, Ship ship, ShipyardTransaction transaction)>>(response.Content).Data;
    }

    public async Task<ShipCargo> GetShipCargo(string shipSymbol)
    {
        var request = new RestRequest(string.Format(GetShipCargoResource, shipSymbol));
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<ShipCargo>>(response.Content).Data;
    }

    public async Task<ShipCooldown> GetShipCooldown(string shipSymbol)
    {
        var request = new RestRequest(string.Format(GetShipCooldownResource, shipSymbol));
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<ShipCooldown>>(response.Content).Data;
    }

    public async Task<ShipNavigationInformation> GetShipNavigationInformation(string shipSymbol)
    {
        var request = new RestRequest(string.Format(GetShipNavigationResource, shipSymbol));
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<ShipNavigationInformation>>(response.Content).Data;
    }

    public async Task<Ship[]> GetShips(int pageNumber, int PageSize)
    {
        var request = new RestRequest(string.Format(GetShipsResource, pageNumber, PageSize));
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Ship[]>>(response.Content).Data;
    }

    public async Task<ShipNavigationInformation> PostShipOrbit(string shipSymbol)
    {
        var request = new RestRequest(string.Format(PostShipOrbitResource, shipSymbol), Method.Post);
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<ShipNavigationInformation>>(response.Content).Data;
    }

    public async Task<ShipNavigationInformation> PostShipRefine(string shipSymbol, TradeSymbols tradeSymbol)
    {
        switch (tradeSymbol)
        {
            default:
                throw new ArgumentException("Invalid trade symbol specified, only Iron, Copper, Silver, Gold, Aluminum, Platinum, Uranite, Meritium and fuel are allowed", nameof(tradeSymbol));
            case TradeSymbols.Iron:
            case TradeSymbols.Copper:
            case TradeSymbols.Aluminum:
            case TradeSymbols.Silver:
            case TradeSymbols.Gold:
            case TradeSymbols.Platinum:
            case TradeSymbols.Uranite:
            case TradeSymbols.Meritium:
            case TradeSymbols.Fuel:
                break;
        }
        var request = new RestRequest(string.Format(PostShipRefineResource, shipSymbol), Method.Post);
        request.AddBody(new ShipRefineRequestModel
        {
            Produce = tradeSymbol.ToApiString()
        });
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<ShipNavigationInformation>>(response.Content).Data;
    }

    public async Task<ShipNavigationInformation> PostShipCreateChart(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public async Task<ShipNavigationInformation> PostShipDock(string shipSymbol)
    {
        var request = new RestRequest(string.Format(PostShipDockResource, shipSymbol), Method.Post);
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<ShipNavigationInformation>>(response.Content).Data;
    }

    public async Task<(ShipCooldown cooldown, Survey survey)> PostShipCreateSurvey(string shipSymbol)
    {
        var request = new RestRequest(string.Format(PostShipSurveyResource, shipSymbol), Method.Post);
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<(ShipCooldown cooldown, Survey survey)>>(response.Content).Data;
    }

    public async Task<(ShipCooldown cooldown, Extraction extraction, ShipCargo cargo)> PostShipExtractResources(string shipSymbol)
    {
        var request = new RestRequest(string.Format(PostShipExtractResource, shipSymbol), Method.Post);
        var response = await restClient.ExecuteAsync(request);

        // TODO: Add json body for survey

        return JsonConvert.DeserializeObject<ApiResponse<(ShipCooldown cooldown, Extraction extraction, ShipCargo cargo)>>(response.Content).Data;
    }

    public async Task<ShipNavigationInformation> PostShipJettisonCargo(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public async Task<(ShipCooldown cooldown, ShipNavigationInformation navigationInformation)> PostShipJump(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public async Task<(ShipFuel fuel, ShipNavigationInformation navigationInformation)> PostShipNavigate(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public async Task<(ShipFuel fuel, ShipNavigationInformation navigationInformation)> PostShipWarp(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public async Task<(Agent agent, ShipCargo cargo, MarketTransaction transaction)> PostShipSellCargo(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public async Task<(ShipCooldown cooldown, ApiModels.System[] systems)> PostShipScanSystems(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public async Task<(ShipCooldown cooldown, Waypoint[] waypoints)> PostShipScanWaypoints(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public async Task<(ShipCooldown cooldown, Ship[] ships)> PostShipScanShips(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public async Task<(Agent agent, ShipFuel fuel)> PostShipRefuel(string shipSymbol)
    {
        var request = new RestRequest(string.Format(PostShipRefuelResource, shipSymbol), Method.Post);
        var response = await restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<(Agent agent, ShipFuel fuel)>>(response.Content).Data;
    }

    public async Task<(Agent agent, ShipCargo cargo, MarketTransaction transaction)> PostShipPurchaseCargo(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public async Task<ShipCargo> PostShipTransferCargo(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public async Task<ShipNavigationInformation> PatchShipNavigation(string shipSymbol)
    {
        throw new NotImplementedException();
    }
}
