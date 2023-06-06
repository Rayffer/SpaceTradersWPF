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
    private bool closeRequested;
    private double estimatedFuelCost;
    private readonly ISpaceTradersApi spaceTradersApi;
    private readonly IRegionManager regionManager;
    private readonly IEventAggregator eventAggregator;
    private readonly INotificationService notificationService;
    private readonly SubscriptionToken subscriptionToken;
    private Waypoint selectedWaypoint;
    private Ship ship;
    private Waypoint[] waypoints;
    private DelegateCommand cancelNavigationCommand;
    private DelegateCommand performNavigationCommand;
    private DelegateCommand<Waypoint> updateFuelCommand;

    public ICommand CancelNavigationCommand => this.cancelNavigationCommand ??= new DelegateCommand(this.CancelNavigation);
    public ICommand PerformNavigationCommand => this.performNavigationCommand ??= new DelegateCommand(async () => await this.PerformNavigation());
    public ICommand UpdateFuelCommand => this.updateFuelCommand ??= new DelegateCommand<Waypoint>(this.UpdateFuel);

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

    private Waypoint currentShipWaypoint;

    public Waypoint[] Waypoints
    {
        get => this.waypoints?.Where(waypoint => waypoint.Symbol != this.Ship?.NavigationInformation.WaypointSymbol).ToArray();
        set => this.SetProperty(ref this.waypoints, value);
    }

    public bool CloseRequested
    {
        get => this.closeRequested;
        set => this.SetProperty(ref this.closeRequested, value);
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

    internal void RemoveElementAnimationCompleted(FlyoutNotificationView view)
    {
        if (!this.regionManager.Regions[RegionNames.DialogAreaRegion].Views.Contains(view))
        {
            return;
        }

        this.regionManager.Regions[RegionNames.DialogAreaRegion].Remove(view);
    }

    private void UpdateFuel(Waypoint destinationWaypoint)
    {
        var xDistance = destinationWaypoint.X - this.currentShipWaypoint.X;
        var yDistance = destinationWaypoint.Y - this.currentShipWaypoint.Y;

        this.EstimatedFuelCost = Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));
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
