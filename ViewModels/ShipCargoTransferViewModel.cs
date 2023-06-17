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

internal class ShipCargoTransferViewModel : BindableBase
{
    private int cargoAmountToTransfer;
    private Ship ship;
    private Ship selectedShipToTransferTo;
    private Inventory selectedCargo;
    private IEnumerable<Ship> waypointShips;
    private readonly IRegionManager regionManager;
    private readonly ISpaceTradersApi spaceTradersApi;
    private readonly IEventAggregator eventAggregator;
    private readonly INotificationService notificationService;
    private DelegateCommand cancelNavigationCommand;
    private DelegateCommand<object[]> performCargoTransferCommand;
    private readonly SubscriptionToken subscriptionToken;

    public ICommand CancelNavigationCommand => this.cancelNavigationCommand ??= new DelegateCommand(this.CancelNavigation);
    public ICommand PerformCargoTransferCommand => this.performCargoTransferCommand ??= new DelegateCommand<object[]>(async arguments => await this.PerformCargoTransfer(arguments));

    public IEnumerable<Ship> WaypointShips
    {
        get => this.waypointShips;
        set => this.SetProperty(ref this.waypointShips, value);
    }

    public Ship Ship
    {
        get => this.ship;
        set => this.SetProperty(ref this.ship, value);
    }

    public int CargoAmountToTransfer
    {
        get => this.cargoAmountToTransfer;
        set => this.SetProperty(ref this.cargoAmountToTransfer, value);
    }

    public Inventory SelectedCargo
    {
        get => this.selectedCargo;
        set => this.SetProperty(ref this.selectedCargo, value);
    }

    public Ship SelectedShipToTransferTo
    {
        get => this.selectedShipToTransferTo;
        set => this.SetProperty(ref this.selectedShipToTransferTo, value);
    }

    public ShipCargoTransferViewModel(
        IRegionManager regionManager,
        ISpaceTradersApi spaceTradersApi,
        IEventAggregator eventAggregator,
        INotificationService notificationService)
    {
        this.regionManager = regionManager;
        this.spaceTradersApi = spaceTradersApi;
        this.eventAggregator = eventAggregator;
        this.notificationService = notificationService;
        this.subscriptionToken = this.eventAggregator
            .GetEvent<ShipCargoTransferEvent>()
            .Subscribe(async Ship => await this.LoadInformation(Ship));
    }

    private async Task LoadInformation(ShipCargoTransferEventArguments ship)
    {
        this.eventAggregator
            .GetEvent<ShipCargoTransferEvent>()
            .Unsubscribe(this.subscriptionToken);

        this.Ship = ship.ShipToTransferFrom;
        var ships = await this.spaceTradersApi.GetShips(1, 20);
        this.WaypointShips = ships.Where(ship => ship.NavigationInformation.WaypointSymbol == this.Ship.NavigationInformation.WaypointSymbol).ToList();
    }

    private void CancelNavigation()
    {
        this.RemoveView();
    }

    private async Task PerformCargoTransfer(object[] arguments)
    {
        var cargoTransferResult = await this.spaceTradersApi.PostShipTransferCargo(this.Ship.Symbol, new ApiModels.Requests.PostShipTransferCargoRequest
        {
            ShipSymbol = this.SelectedShipToTransferTo.Symbol,
            Symbol = this.SelectedCargo.Symbol,
            Units = this.CargoAmountToTransfer
        });

        this.notificationService
            .ShowToastNotification(
                $"Transfered cargo from {this.Ship.Symbol} to {this.SelectedShipToTransferTo.Symbol}",
                $"Transfered {this.CargoAmountToTransfer} units of {this.SelectedCargo.Symbol}",
                NotificationTypes.PositiveFeedback);

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
