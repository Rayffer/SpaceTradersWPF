using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.RateLimiting;
using System.Threading.Tasks;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.ApiModels.Requests;
using SpaceTradersWPF.ApiModels.Responses;
using SpaceTradersWPF.Types;

namespace SpaceTradersWPF.Services;

internal class SpaceTradersApiRateLimitedProxy : ISpaceTradersApi, IDisposable
{
    private readonly ISpaceTradersApi spaceTradersApi;
    private TokenBucketRateLimiter instantLimitBucket;
    private TokenBucketRateLimiter burstLimitBucket;

    public SpaceTradersApiRateLimitedProxy([Unity.Dependency("SpaceTradersApi")] ISpaceTradersApi spaceTradersApi)
    {
        this.instantLimitBucket = new TokenBucketRateLimiter(new TokenBucketRateLimiterOptions
        {
            ReplenishmentPeriod = TimeSpan.FromSeconds(1.5),
            TokensPerPeriod = 2,
            TokenLimit = 2
        });

        this.burstLimitBucket = new TokenBucketRateLimiter(new TokenBucketRateLimiterOptions
        {
            ReplenishmentPeriod = TimeSpan.FromSeconds(11),
            TokensPerPeriod = 10,
            TokenLimit = 10
        });
        this.spaceTradersApi = spaceTradersApi;
    }

    private async Task AcquireRateLimitToken([CallerMemberName] string callerName = "")
    {
        var RateLimitAction = new Action<string>(caller =>
        {
            while (!this.burstLimitBucket.AttemptAcquire().IsAcquired)
            {
                Thread.Sleep(250);
            }
            while (!this.instantLimitBucket.AttemptAcquire().IsAcquired)
            {
                Thread.Sleep(250);
            }
        });
        await Task.Run(() => RateLimitAction(callerName));
    }

    public void ClearAccessToken()
    {
        this.spaceTradersApi.ClearAccessToken();
    }

    public void SetAccessTokenHeader(string token)
    {
        this.spaceTradersApi.SetAccessTokenHeader(token);
    }

    public async Task<Contract> GetContract(string contractId)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.GetContract(contractId);
    }

    public async Task<Contract[]> GetContracts()
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.GetContracts();
    }

    public async Task<Faction> GetFaction(string factionSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.GetFaction(factionSymbol);
    }

    public async Task<Faction[]> GetFactions(int pageNumber, int pageSize)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.GetFactions(pageNumber, pageSize);
    }

    public async Task<JumpGate> GetJumpGate(string waypointSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.GetJumpGate(waypointSymbol);
    }

    public async Task<Market> GetMarket(string waypointSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.GetMarket(waypointSymbol);
    }

    public async Task<Ship> GetShip(string shipSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.GetShip(shipSymbol);
    }

    public async Task<ShipCargo> GetShipCargo(string shipSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.GetShipCargo(shipSymbol);
    }

    public async Task<ShipCooldown> GetShipCooldown(string shipSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.GetShipCooldown(shipSymbol);
    }

    public async Task<ShipNavigationInformation> GetShipNavigationInformation(string shipSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.GetShipNavigationInformation(shipSymbol);
    }

    public async Task<Ship[]> GetShips(int pageNumber, int pageSize)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.GetShips(pageNumber, pageSize);
    }

    public async Task<Shipyard> GetShipyard(string waypointSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.GetShipyard(waypointSymbol);
    }

    public async Task<SolarSystem> GetSystem(string waypointSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.GetSystem(waypointSymbol);
    }

    public async Task<SolarSystem[]> GetSystems(int pageNumber, int pageSize)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.GetSystems(pageNumber, pageSize);
    }

    public async Task<Waypoint> GetWaypoint(string symbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.GetWaypoint(symbol);
    }

    public async Task<Waypoint[]> GetWaypoints(string waypointSymbol, int pageNumber, int pageSize)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.GetWaypoints(waypointSymbol, pageNumber, pageSize);
    }

    public async Task<ShipNavigationInformation> PatchShipNavigation(string shipSymbol, PatchShipNavigationRequestModel shipNavigationRequestModel)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PatchShipNavigation(shipSymbol, shipNavigationRequestModel);
    }

    public async Task<ChartResponse> PostShipCreateChart(string shipSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipCreateChart(shipSymbol);
    }

    public async Task<ShipNavigationInformation> PostShipDock(string shipSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipDock(shipSymbol);
    }

    public async Task<CargoJettisonResponse> PostShipJettisonCargo(string shipSymbol, PostShipJettisonCargoRequest shipJettisonCargoRequest)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipJettisonCargo(shipSymbol, shipJettisonCargoRequest);
    }

    public async Task<ShipNavigationInformation> PostShipOrbit(string shipSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipOrbit(shipSymbol);
    }

    public async Task<ShipCargo> PostShipTransferCargo(string shipSymbol, PostShipTransferCargoRequest shipTransferCargoRequest)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipTransferCargo(shipSymbol, shipTransferCargoRequest);
    }

    public async Task<AgentResponse> RegisterAgent(string symbol, string faction)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.RegisterAgent(symbol, faction);
    }

    public async Task<Agent> GetAgent()
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.GetAgent();
    }

    public async Task<ContractResponse> PostAcceptContract(string contractId)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostAcceptContract(contractId);
    }

    public async Task<DeliverContractResponse> PostDeliverContract(string contractId, ContractDeliverRequest contractDeliverRequest)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostDeliverContract(contractId, contractDeliverRequest);
    }

    public async Task<ContractResponse> PostFulfillContract(string contractId)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostFulfillContract(contractId);
    }

    public async Task<ShipPurchaseResponse> PostShipPurchase(ShipTypes shipTypes, string waypointSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipPurchase(shipTypes, waypointSymbol);
    }

    public async Task<ShipRefineResponse> PostShipRefine(string shipSymbol, string oreToRefine)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipRefine(shipSymbol, oreToRefine);
    }

    public async Task<SurveyResponse> PostShipCreateSurvey(string shipSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipCreateSurvey(shipSymbol);
    }

    public async Task<ExtractionResponse> PostShipExtractResources(string shipSymbol, string waypointSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipExtractResources(shipSymbol, waypointSymbol);
    }

    public async Task<JumpResponse> PostShipJump(string shipSymbol, string systemSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipJump(shipSymbol, systemSymbol);
    }

    public async Task<NavigationResponse> PostShipNavigate(string shipSymbol, string waypointSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipNavigate(shipSymbol, waypointSymbol);
    }

    public async Task<WarpResponse> PostShipWarp(string shipSymbol, string waypointSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipWarp(shipSymbol, waypointSymbol);
    }

    public async Task<CargoTransactionResponse> PostShipSellCargo(string shipSymbol, PostShipSellCargoRequest shipSellCargoRequest)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipSellCargo(shipSymbol, shipSellCargoRequest);
    }

    public async Task<ScanSystemsResponse> PostShipScanSystems(string shipSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipScanSystems(shipSymbol);
    }

    public async Task<ScanWaypointsResponse> PostShipScanWaypoints(string shipSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipScanWaypoints(shipSymbol);
    }

    public async Task<ScanShipsResponse> PostShipScanShips(string shipSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipScanShips(shipSymbol);
    }

    public async Task<RefuelResponse> PostShipRefuel(string shipSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipRefuel(shipSymbol);
    }

    public async Task<CargoTransactionResponse> PostShipPurchaseCargo(string shipSymbol, PostShipPurchaseCargoRequest shipPurchaseCargoRequest)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipPurchaseCargo(shipSymbol, shipPurchaseCargoRequest);
    }

    public async Task<Contract> PostShipNegotiateContract(string shipSymbol)
    {
        await this.AcquireRateLimitToken();
        return await this.spaceTradersApi.PostShipNegotiateContract(shipSymbol);
    }

    public void Dispose()
    {
        this.instantLimitBucket.Dispose();
        this.burstLimitBucket.Dispose();
    }
}
