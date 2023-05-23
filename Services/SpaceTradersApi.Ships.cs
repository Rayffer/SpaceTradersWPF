using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.ApiModels.Requests;
using SpaceTradersWPF.ApiModels.Responses;
using SpaceTradersWPF.Extensions;
using SpaceTradersWPF.Types;

namespace SpaceTradersWPF.Services;

internal partial class SpaceTradersApi
{
    private const string GetShipsResource = "/my/ships?page={0}&limit={1}";
    private const string GetShipResource = "/my/ships/{0}";
    private const string GetShipCargoResource = "/my/ships/{0}/cargo";
    private const string GetShipCooldownResource = "/my/ships/{0}/cooldown";
    private const string GetShipNavigationResource = "/my/ships/{0}/nav";
    private const string PostPurchaseShipResource = "/my/ships";
    private const string PostShipOrbitResource = "/my/ships/{0}/orbit";
    private const string PostShipRefineResource = "/my/ships/{0}/refine";
    private const string PostShipChartResource = "/my/ships/{0}/chart";
    private const string PostShipDockResource = "/my/ships/{0}/dock";
    private const string PostShipSurveyResource = "/my/ships/{0}/survey";
    private const string PostShipExtractResource = "/my/ships/{0}/extract";
    private const string PostShipJettisonResource = "/my/ships/{0}/jettison";
    private const string PostShipJumpResource = "/my/ships/{0}/jump";
    private const string PostShipNavigateResource = "/my/ships/{0}/navigate";
    private const string PostShipWarpResource = "/my/ships/{0}/warp";
    private const string PostShipSellCargoResource = "/my/ships/{0}/sell";
    private const string PostShipScanSystemsResource = "/my/ships/{0}/scan/systems";
    private const string PostShipScanWaypointsResource = "/my/ships/{0}/scan/waypoints";
    private const string PostShipScanShipsResource = "/my/ships/{0}/scan/ships";
    private const string PostShipRefuelResource = "/my/ships/{0}/refuel";
    private const string PostShipPurchaseCargoResource = "/my/ships/{0}/purchase";
    private const string PostShipTransferCargoResource = "/my/ships/{0}/transfer";
    private const string PatchShipNavigationInformationResource = "/my/ships/{0}/nav";

    public async Task<Ship> GetShip(string shipSymbol)
    {
        var request = new RestRequest(string.Format(GetShipResource, shipSymbol));
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Ship>>(response.Content).Data;
    }

    public async Task<ShipPurchaseResponse> PostShipPurchase(ShipTypes shipTypes, string waypointSymbol)
    {
        var request = new RestRequest(string.Format(PostPurchaseShipResource), Method.Post);
        request.AddBody(new ShipPurchaseRequestModel
        {
            ShipType = shipTypes.ToApiString(),
            WaypointSymbol = waypointSymbol
        });
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<ShipPurchaseResponse>>(response.Content).Data;
    }

    public async Task<ShipCargo> GetShipCargo(string shipSymbol)
    {
        var request = new RestRequest(string.Format(GetShipCargoResource, shipSymbol));
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<ShipCargo>>(response.Content).Data;
    }

    public async Task<ShipCooldown> GetShipCooldown(string shipSymbol)
    {
        var request = new RestRequest(string.Format(GetShipCooldownResource, shipSymbol));
        var response = await this.restClient.ExecuteAsync(request);

        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            return null;
        }
        return JsonConvert.DeserializeObject<ApiResponse<ShipCooldown>>(response.Content).Data;
    }

    public async Task<ShipNavigationInformation> GetShipNavigationInformation(string shipSymbol)
    {
        var request = new RestRequest(string.Format(GetShipNavigationResource, shipSymbol));
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<ShipNavigationInformation>>(response.Content).Data;
    }

    public async Task<Ship[]> GetShips(int pageNumber, int PageSize)
    {
        var request = new RestRequest(string.Format(GetShipsResource, pageNumber, PageSize));
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<Ship[]>>(response.Content).Data;
    }

    public async Task<ShipNavigationInformation> PostShipOrbit(string shipSymbol)
    {
        var request = new RestRequest(string.Format(PostShipOrbitResource, shipSymbol), Method.Post);
        var response = await this.restClient.ExecuteAsync(request);

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
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<ShipNavigationInformation>>(response.Content).Data;
    }

    public async Task<ShipNavigationInformation> PostShipCreateChart(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public async Task<ShipNavigationInformation> PostShipDock(string shipSymbol)
    {
        var request = new RestRequest(string.Format(PostShipDockResource, shipSymbol), Method.Post);
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<ShipNavigationInformation>>(response.Content).Data;
    }

    public async Task<SurveyResponse> PostShipCreateSurvey(string shipSymbol)
    {
        var request = new RestRequest(string.Format(PostShipSurveyResource, shipSymbol), Method.Post);
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<SurveyResponse>>(response.Content).Data;
    }

    public async Task<ExtractionResponse> PostShipExtractResources(string shipSymbol, string waypointSymbol)
    {
        var request = new RestRequest(string.Format(PostShipExtractResource, shipSymbol), Method.Post);
        var survey = this.waypointSurveyService.GetSurvey(waypointSymbol);
        if (survey is not null)
        {
            request.AddBody(new ShipExtractRequestModel
            {
                Survey = survey
            });
        }

        var response = await this.restClient.ExecuteAsync(request);
        return JsonConvert.DeserializeObject<ApiResponse<ExtractionResponse>>(response.Content).Data;
    }

    public async Task<ShipNavigationInformation> PostShipJettisonCargo(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public async Task<JumpResponse> PostShipJump(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public async Task<NavigationResponse> PostShipNavigate(string shipSymbol, string waypointSymbol)
    {
        var request = new RestRequest(string.Format(PostShipNavigateResource, shipSymbol), Method.Post);
        request.AddBody(new ShipNavigationRequestModel
        {
            WaypointSymbol = waypointSymbol
        });
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<NavigationResponse>>(response.Content).Data;
    }

    public async Task<NavigationResponse> PostShipWarp(string shipSymbol, string waypointSymbol)
    {
        throw new NotImplementedException();
    }

    public async Task<CargoTransactionResponse> PostShipSellCargo(string shipSymbol, ShipSellCargoRequest shipSellCargoRequest)
    {
        var request = new RestRequest(string.Format(PostShipSellCargoResource, shipSymbol), Method.Post);
        request.AddBody(shipSellCargoRequest);
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<CargoTransactionResponse>>(response.Content).Data;
    }

    public async Task<ScanSystemsResponse> PostShipScanSystems(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public async Task<ScanWaypointsResponse> PostShipScanWaypoints(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public async Task<ScanShipsResponse> PostShipScanShips(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public async Task<RefuelResponse> PostShipRefuel(string shipSymbol)
    {
        var request = new RestRequest(string.Format(PostShipRefuelResource, shipSymbol), Method.Post);
        var response = await this.restClient.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<ApiResponse<RefuelResponse>>(response.Content).Data;
    }

    public async Task<CargoTransactionResponse> PostShipPurchaseCargo(string shipSymbol)
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
