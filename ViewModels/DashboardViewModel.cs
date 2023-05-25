using System;
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
using SpaceTradersWPF.Views;

namespace SpaceTradersWPF.ViewModels;

internal class DashboardViewModel : BindableBase
{
    private readonly ISpaceTradersApi spaceTradersApi;
    private readonly IRegionManager regionManager;
    private readonly IEventAggregator eventAggregator;
    private DelegateCommand loadInformationCommand;
    private DelegateCommand openSystemInformation;
    private DelegateCommand openWaypointInformation;
    private DelegateCommand<Ship> openShipInformation;
    private Agent agentInformation;
    private Waypoint headquarters;
    private IEnumerable<Ship> ships;

    public ICommand OpenSystemInformation => this.openSystemInformation ??= new DelegateCommand(this.PerformOpenSystemInformation);
    public ICommand OpenWaypointInformation => this.openWaypointInformation ??= new DelegateCommand(this.PerformOpenWaypointInformation);
    public ICommand OpenShipInformation => this.openShipInformation ??= new DelegateCommand<Ship>(this.PerformOpenShipInformation);
    public ICommand LoadInformationCommand => this.loadInformationCommand ??= new DelegateCommand(async () => await this.LoadInformation());

    public Agent AgentInformation
    {
        get => this.agentInformation;
        set => this.SetProperty(ref this.agentInformation, value);
    }

    public Waypoint Headquarters
    {
        get => this.headquarters;
        set => this.SetProperty(ref this.headquarters, value);
    }

    public IEnumerable<Ship> Ships
    {
        get => this.ships;
        set => this.SetProperty(ref this.ships, value);
    }

    public DashboardViewModel(
        ISpaceTradersApi spaceTradersApi,
        IRegionManager regionManager,
        IEventAggregator eventAggregator)
    {
        this.spaceTradersApi = spaceTradersApi;
        this.regionManager = regionManager;
        this.eventAggregator = eventAggregator;
    }

    private async Task LoadInformation()
    {
        this.AgentInformation = await this.spaceTradersApi.GetAgent();
        this.Headquarters = await this.spaceTradersApi.GetWaypoint(this.AgentInformation.Headquarters);
        this.Ships = await this.spaceTradersApi.GetShips(1, 20);
    }

    private void PerformOpenSystemInformation()
    {
        this.RemoveCurrentView();
        this.regionManager.RegisterViewWithRegion(RegionNames.MainAreaRegion, typeof(SystemInformationView));
        this.eventAggregator.GetEvent<SystemInformationEvent>().Publish(new SystemInformationEventArguments
        {
            SystemSymbol = this.Headquarters.SystemSymbol
        });
    }

    private void PerformOpenWaypointInformation()
    {
        this.RemoveCurrentView();
        this.regionManager.RegisterViewWithRegion(RegionNames.MainAreaRegion, typeof(WaypointInformationView));
        this.eventAggregator.GetEvent<WaypointInformationEvent>().Publish(new WaypointInformationEventArguments
        {
            WaypointSymbol = this.Headquarters.Symbol
        });
    }

    private void PerformOpenShipInformation(Ship ship)
    {
        this.RemoveCurrentView();
        this.regionManager.RegisterViewWithRegion(RegionNames.MainAreaRegion, typeof(AgentShipsView));
        this.eventAggregator.GetEvent<ShipInformationEvent>().Publish(new ShipInformationEventArguments
        {
            Ship = ship
        });
    }

    private void RemoveCurrentView()
    {
        var dashboardView = this.regionManager.Regions[RegionNames.MainAreaRegion].Views.OfType<DashboardView>().Single();
        this.regionManager.Regions[RegionNames.MainAreaRegion].Remove(dashboardView);
    }
}
