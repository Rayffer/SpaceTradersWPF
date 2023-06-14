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

internal class ShipJumpViewModel : BindableBase
{
    private readonly ISpaceTradersApi spaceTradersApi;
    private readonly IRegionManager regionManager;
    private readonly IEventAggregator eventAggregator;
    private readonly INotificationService notificationService;
    private readonly SubscriptionToken subscriptionToken;
    private JumpGateConnectedSystem[] jumpGateSystems;
    private JumpGateConnectedSystem selectedSystemToJump;
    private Ship ship;
    private DelegateCommand cancelJumpCommand;
    private DelegateCommand performJumpCommand;

    public ICommand CancelJumpCommand => this.cancelJumpCommand ??= new DelegateCommand(this.CancelJump);
    public ICommand PerformJumpCommand => this.performJumpCommand ??= new DelegateCommand(async () => await this.PerformJump());

    public JumpGateConnectedSystem[] JumpGateSystems
    {
        get => this.jumpGateSystems?.Where(system => system.Symbol != this.Ship?.NavigationInformation.SystemSymbol).ToArray();
        set => this.SetProperty(ref this.jumpGateSystems, value);
    }

    public JumpGateConnectedSystem SelectedSystemToJump
    {
        get => this.selectedSystemToJump;
        set => this.SetProperty(ref this.selectedSystemToJump, value);
    }

    public Ship Ship
    {
        get => this.ship;
        set => this.SetProperty(ref this.ship, value);
    }

    public ShipJumpViewModel(
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
            .GetEvent<ShipJumpRequestEvent>()
            .Subscribe(async eventInformation => await this.LoadInformation(eventInformation));
    }

    private async Task LoadInformation(ShipJumpRequestEventArguments eventInformation)
    {
        this.eventAggregator
            .GetEvent<ShipNavigationRequestEvent>()
            .Unsubscribe(this.subscriptionToken);

        this.Ship = eventInformation.Ship;
        this.JumpGateSystems = (await this.spaceTradersApi.GetJumpGate(this.Ship.NavigationInformation.WaypointSymbol)).ConnectedSystems;
    }

    private void CancelJump()
    {
        this.RemoveView();
    }

    private async Task PerformJump()
    {
        var navigationResponse = await this.spaceTradersApi.PostShipJump(this.Ship.Symbol, this.SelectedSystemToJump.Symbol);
        if (navigationResponse == null)
        {
            this.RemoveView();
            return;
        }
        this.eventAggregator.GetEvent<ShipInformationEvent>().Publish(new ShipInformationEventArguments
        {
            Ship = this.Ship
        });

        this.notificationService
            .ShowFlyoutNotification(
                $"Ship {this.Ship.Symbol} jumped to {this.SelectedSystemToJump.Symbol}",
                string.Empty,
                Types.NotificationTypes.PositiveFeedback);

        this.RemoveView();
    }

    private void RemoveView()
    {
        if (!this.regionManager.Regions[RegionNames.DialogAreaRegion].Views.OfType<ShipJumpView>().Any())
        {
            return;
        }

        var viewToRemove = this.regionManager.Regions[RegionNames.DialogAreaRegion].Views.OfType<ShipJumpView>().Single();
        this.regionManager.Regions[RegionNames.DialogAreaRegion].Remove(viewToRemove);
    }
}
