using System;
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
using SpaceTradersWPF.Views;

namespace SpaceTradersWPF.ViewModels;

internal class ShipNavigationViewModel : BindableBase
{
    private double estimatedFuelCost;
    private readonly ISpaceTradersApi spaceTradersApi;
    private readonly IRegionManager regionManager;
    private readonly IEventAggregator eventAggregator;
    private readonly INotificationService notificationService;
    private readonly SubscriptionToken subscriptionToken;
    private double estimatedFlightTime;
    private Waypoint selectedWaypoint;
    private Waypoint currentShipWaypoint;
    private Ship ship;
    private Waypoint[] waypoints;
    private DelegateCommand cancelNavigationCommand;
    private DelegateCommand performNavigationCommand;
    private DelegateCommand<Waypoint> updateFuelCommand;
    private DelegateCommand<Waypoint> updateFlightTimeCommand;

    public ICommand CancelNavigationCommand => this.cancelNavigationCommand ??= new DelegateCommand(this.CancelNavigation);
    public ICommand PerformNavigationCommand => this.performNavigationCommand ??= new DelegateCommand(async () => await this.PerformNavigation());
    public ICommand UpdateFuelCommand => this.updateFuelCommand ??= new DelegateCommand<Waypoint>(this.UpdateFuel);
    public ICommand UpdateFlightTimeCommand => this.updateFlightTimeCommand ??= new DelegateCommand<Waypoint>(this.UpdateFlightTime);

    public double EstimatedFuelCost
    {
        get => this.estimatedFuelCost;
        set => this.SetProperty(ref this.estimatedFuelCost, value);
    }

    public Waypoint SelectedWaypoint
    {
        get => this.selectedWaypoint;
        set => this.SetProperty(ref this.selectedWaypoint, value);
    }

    public Ship Ship
    {
        get => this.ship;
        set => this.SetProperty(ref this.ship, value);
    }

    public Waypoint[] Waypoints
    {
        get => this.waypoints?.Where(waypoint => waypoint.Symbol != this.Ship?.NavigationInformation.WaypointSymbol).ToArray();
        set => this.SetProperty(ref this.waypoints, value);
    }

    public double EstimatedFlightTime
    {
        get => this.estimatedFlightTime;
        set => this.SetProperty(ref this.estimatedFlightTime, value);
    }

    public ShipNavigationViewModel(
        ISpaceTradersApi spaceTradersApi,
        IRegionManager regionManager,
        IEventAggregator eventAggregator,
        INotificationService notificationService)
    {
        this.spaceTradersApi = spaceTradersApi;
        this.regionManager = regionManager;
        this.eventAggregator = eventAggregator;
        this.notificationService = notificationService;
        this.subscriptionToken = this.eventAggregator
            .GetEvent<ShipNavigationRequestEvent>()
            .Subscribe(async eventInformation => await this.LoadInformation(eventInformation));
    }

    private async Task LoadInformation(ShipNavigationRequestEventArguments eventInformation)
    {
        this.eventAggregator
            .GetEvent<ShipNavigationRequestEvent>()
            .Unsubscribe(this.subscriptionToken);

        this.Ship = eventInformation.Ship;
        this.currentShipWaypoint = await this.spaceTradersApi.GetWaypoint(this.Ship.NavigationInformation.WaypointSymbol);
        this.Waypoints = await this.spaceTradersApi.GetWaypoints(this.Ship.NavigationInformation.SystemSymbol, 1, 20);
    }

    private void UpdateFuel(Waypoint destinationWaypoint)
    {
        var xDistance = destinationWaypoint.X - this.currentShipWaypoint.X;
        var yDistance = destinationWaypoint.Y - this.currentShipWaypoint.Y;

        if (this.Ship.Frame.Symbol == "FRAME_PROBE")
        {
            this.EstimatedFuelCost = 0;
            return;
        }

        var distance = Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));
        this.EstimatedFuelCost = this.ship.NavigationInformation.FlightMode switch
        {
            "DRIFT" => 1,
            "CRUISE" => distance,
            "STEALTH" => distance,
            "BURN" => distance * 2,
            _ => 0
        };
    }

    private void UpdateFlightTime(Waypoint destinationWaypoint)
    {
        var xDistance = destinationWaypoint.X - this.currentShipWaypoint.X;
        var yDistance = destinationWaypoint.Y - this.currentShipWaypoint.Y;

        var distance = Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));
        this.EstimatedFlightTime = this.ship.NavigationInformation.FlightMode switch
        {
            "DRIFT" => 15 + 100 * distance / this.Ship.Engine.Speed,
            "CRUISE" => 15 + 10 * distance / this.Ship.Engine.Speed,
            "STEALTH" => 15 + 20 * distance / this.Ship.Engine.Speed,
            "BURN" => 15 + 5 * distance / this.Ship.Engine.Speed,
            _ => 0
        };
    }

    private void CancelNavigation()
    {
        this.RemoveView();
    }

    private async Task PerformNavigation()
    {
        var navigationResponse = await this.spaceTradersApi.PostShipNavigate(this.Ship.Symbol, this.SelectedWaypoint.Symbol);
        this.eventAggregator.GetEvent<ShipInformationEvent>().Publish(new ShipInformationEventArguments
        {
            Ship = this.Ship
        });

        this.notificationService
            .ShowFlyoutNotification(
                $"Ship enroute to {this.SelectedWaypoint.Symbol}",
                $"Estimated arrival at: {navigationResponse.NavigationInformation.Route.Arrival}",
                Types.NotificationTypes.PositiveFeedback);

        this.RemoveView();
    }

    private void RemoveView()
    {
        if (!this.regionManager.Regions[RegionNames.DialogAreaRegion].Views.OfType<ShipNavigationView>().Any())
        {
            return;
        }

        var viewToRemove = this.regionManager.Regions[RegionNames.DialogAreaRegion].Views.OfType<ShipNavigationView>().Single();
        this.regionManager.Regions[RegionNames.DialogAreaRegion].Remove(viewToRemove);
    }
}
