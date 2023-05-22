using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.Events;
using SpaceTradersWPF.Events.Models;
using SpaceTradersWPF.Services;
using SpaceTradersWPF.Types;
using SpaceTradersWPF.Views;

namespace SpaceTradersWPF.ViewModels;

internal class AgentFleetShipsOverviewViewModel : BindableBase
{
    private readonly ISpaceTradersApi spaceTradersApi;
    private readonly IEventAggregator eventAggregator;
    private readonly INotificationService notificationService;
    private readonly IRegionManager regionManager;
    private readonly IWaypointSurveyService waypointSurveyService;
    private IEnumerable<Ship> ships;
    private Ship selectedShip;
    private DelegateCommand loadShipsCommand;
    private DelegateCommand<Ship> performExtractionCommand;
    private DelegateCommand<Ship> performSurveyCommand;
    private DelegateCommand<Ship> performWarpCommand;
    private DelegateCommand<Ship> performOrbitCommand;
    private DelegateCommand<Ship> performDockCommand;
    private DelegateCommand<Ship> performNavigateCommand;
    private DelegateCommand<Ship> performRefuelCommand;
    private bool alreadyLoaded;

    public Ship SelectedShip
    {
        get => this.selectedShip;
        set => this.SetProperty(ref this.selectedShip, value);
    }

    public IEnumerable<Ship> Ships
    {
        get => this.ships;
        set => this.SetProperty(ref this.ships, value);
    }

    public ICommand LoadShipsCommand => this.loadShipsCommand ??= new DelegateCommand(async () => await this.LoadShips());
    public ICommand PerformExtractionCommand => this.performExtractionCommand ??= new DelegateCommand<Ship>(async ship => await this.PerformExtraction(ship), ship => ship != null);
    public ICommand PerformSurveyCommand => this.performSurveyCommand ??= new DelegateCommand<Ship>(async ship => await this.PerformSurvey(ship), ship => ship != null);
    public ICommand PerformWarpCommand => this.performWarpCommand ??= new DelegateCommand<Ship>(async ship => await this.PerformWarp(ship), ship => ship != null);
    public ICommand PerformOrbitCommand => this.performOrbitCommand ??= new DelegateCommand<Ship>(async ship => await this.PerformOrbit(ship), ship => ship != null);
    public ICommand PerformDockCommand => this.performDockCommand ??= new DelegateCommand<Ship>(async ship => await this.PerformDock(ship), ship => ship != null);
    public ICommand PerformNavigateCommand => this.performNavigateCommand ??= new DelegateCommand<Ship>(async ship => await this.PerformNavigate(ship), ship => ship != null);
    public ICommand PerformRefuelCommand => this.performRefuelCommand ??= new DelegateCommand<Ship>(async ship => await this.PerformRefuel(ship), ship => ship != null);

    public AgentFleetShipsOverviewViewModel(
        ISpaceTradersApi spaceTradersApi,
        IEventAggregator eventAggregator,
        INotificationService notificationService,
        IRegionManager regionManager,
        IWaypointSurveyService waypointSurveyService)
    {
        this.spaceTradersApi = spaceTradersApi;
        this.notificationService = notificationService;
        this.regionManager = regionManager;
        this.waypointSurveyService = waypointSurveyService;
        this.eventAggregator = eventAggregator;
        this.eventAggregator.GetEvent<ShipInformationEvent>().Subscribe(async (eventInformation) => await this.LoadSelectedShipInformation(eventInformation));
    }

    ~AgentFleetShipsOverviewViewModel()
    {
        this.eventAggregator.GetEvent<ShipInformationEvent>().Unsubscribe(async (eventInformation) => await this.LoadSelectedShipInformation(eventInformation));
    }

    private async Task LoadSelectedShipInformation(ShipInformationEventArguments arguments)
    {
        this.alreadyLoaded = true;
        await this.RefreshShips(arguments.Ship);
    }

    private async Task LoadShips()
    {
        if (this.alreadyLoaded)
        {
            this.alreadyLoaded = false;
            return;
        }
        this.Ships = await this.spaceTradersApi.GetShips(1, 20);
    }

    private async Task PerformExtraction(Ship ship)
    {
        var cooldown = await this.spaceTradersApi.GetShipCooldown(ship.Symbol);
        if (cooldown != null)
        {
            this.notificationService.ShowToastNotification(
                $"Ship {ship.Symbol} is cooling down",
                $"Remaining cooldown: {cooldown.RemainingSeconds} seconds",
                NotificationTypes.WarningFeedback,
                true);
            return;
        }
        var result = await this.spaceTradersApi.PostShipExtractResources(ship.Symbol, ship.NavigationInformation.WaypointSymbol);

        this.notificationService.ShowToastNotification(
            $"Ship extracted {result.Extraction.Yield.Units} unit{(result.Extraction.Yield.Units > 1 ? "s" : string.Empty)} of {result.Extraction.Yield.Symbol}",
            null,
            NotificationTypes.PositiveFeedback,
            true);
        await this.RefreshShips(ship);
    }

    private async Task PerformSurvey(Ship ship)
    {
        var cooldown = await this.spaceTradersApi.GetShipCooldown(ship.Symbol);
        if (cooldown != null)
        {
            this.notificationService.ShowToastNotification(
                $"Ship {ship.Symbol} is cooling down",
                $"Remaining cooldown: {cooldown.RemainingSeconds} seconds",
                NotificationTypes.WarningFeedback,
                true);
            return;
        }
        var surveyInformation = await this.spaceTradersApi.PostShipCreateSurvey(ship.Symbol);
        this.waypointSurveyService.SaveSurveyDetails(surveyInformation.Surveys);

        this.notificationService.ShowToastNotification(
            $"Ship {ship.Symbol} surveyed waypoint {ship.NavigationInformation.WaypointSymbol}",
            $"Found fields: {string.Join(", ", surveyInformation.Surveys.SelectMany(survey => survey.Deposits.Select(deposit => deposit.Symbol)))}",
            NotificationTypes.PositiveFeedback,
            true);
    }

    private async Task PerformWarp(Ship ship)
    {
        var cooldown = await this.spaceTradersApi.GetShipCooldown(ship.Symbol);
        if (cooldown != null)
        {
            this.notificationService.ShowToastNotification(
                $"Ship {ship.Symbol} is cooling down",
                $"Remaining cooldown: {cooldown.RemainingSeconds} seconds",
                NotificationTypes.WarningFeedback,
                true);
            return;
        }
    }

    private async Task PerformOrbit(Ship ship)
    {
        _ = await this.spaceTradersApi.PostShipOrbit(ship.Symbol);
        await this.RefreshShips(ship);
        this.notificationService.ShowToastNotification(
            $"Ship {ship.Symbol} entered orbit succesfully",
            null,
            NotificationTypes.PositiveFeedback,
            true);
    }

    private async Task PerformDock(Ship ship)
    {
        _ = await this.spaceTradersApi.PostShipDock(ship.Symbol);
        await this.RefreshShips(ship);
        this.notificationService.ShowToastNotification(
            $"Ship {ship.Symbol} docked succesfully",
            null,
            NotificationTypes.PositiveFeedback,
            true);
    }

    private async Task PerformNavigate(Ship ship)
    {
        this.regionManager.RegisterViewWithRegion(RegionNames.DialogAreaRegion, typeof(ShipNavigationView));
        this.eventAggregator
            .GetEvent<ShipNavigationRequestEvent>()
            .Publish(new ShipNavigationRequestEventArguments
            {
                Ship = ship
            });
    }

    private async Task PerformRefuel(Ship ship)
    {
        var refuelResponse = await this.spaceTradersApi.PostShipRefuel(ship.Symbol);
        await this.RefreshShips(ship);
        this.notificationService.ShowToastNotification(
            $"Ship {ship.Symbol} refueled succesfully",
            null,
            NotificationTypes.PositiveFeedback,
            true);
    }

    private async Task RefreshShips(Ship ship)
    {
        this.Ships = await this.spaceTradersApi.GetShips(1, 20);
        this.SelectedShip = this.Ships.First(x => x.Symbol == ship.Symbol);
    }
}
