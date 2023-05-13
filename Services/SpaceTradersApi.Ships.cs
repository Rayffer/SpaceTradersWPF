using System;
using System.Threading.Tasks;

using SpaceTradersWPF.Models;

namespace SpaceTradersWPF.Services;

internal partial class SpaceTradersApi
{
    private readonly string GetShipsResource = "/my/ships";
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
    private readonly string PatchShipOrbitResource = "/my/ships/{0}/nav";

    public Task<Ship> GetShip(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<(Agent agent, Ship ship, ShipyardTransaction transaction)> PostShipPurchase(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<ShipNavigationInformation> PostShipOrbit(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<ShipNavigationInformation> PostShipRefine(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<ShipNavigationInformation> PostShipCreateChart(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<ShipNavigationInformation> PostShipDock(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<(ShipCooldown cooldown, Survey survey)> PostShipCreateSurvey(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<(ShipCooldown cooldown, Survey survey)> PostShipExtractResources(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<ShipNavigationInformation> PostShipJettisonCargo(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<(ShipCooldown cooldown, ShipNavigationInformation navigationInformation)> PostShipJump(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<(ShipFuel fuel, ShipNavigationInformation navigationInformation)> PostShipNavigate(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<ShipNavigationInformation> PatchShipNavigation(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<(ShipFuel fuel, ShipNavigationInformation navigationInformation)> PostShipWarp(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<(Agent agent, ShipCargo cargo, MarketTransaction transaction)> PostShipSellCargo(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<(ShipCooldown cooldown, Models.System[] systems)> PostShipScanSystems(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<(ShipCooldown cooldown, Waypoint[] waypoints)> PostShipScanWaypoints(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<(ShipCooldown cooldown, Ship[] ships)> PostShipScanShips(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<(Agent agent, ShipFuel fuel)> PostShipRefuel(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<(Agent agent, ShipCargo cargo, MarketTransaction transaction)> PostShipPurchaseCargo(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<ShipCargo> PostShipTransferCargo(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<ShipCargo> GetShipCargo(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<ShipCooldown> GetShipCooldown(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<ShipNavigationInformation> GetShipNavigationInformation(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<Ship[]> GetShips(int pageNumber, int PageSize)
    {
        throw new NotImplementedException();
    }
}