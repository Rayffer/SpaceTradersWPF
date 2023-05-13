using System.Linq;
using System.Windows.Input;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

using SpaceTradersWPF.Events;
using SpaceTradersWPF.Models;
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
    private Agent agentInformation;
    private Waypoint headquarters;

    public ICommand OpenSystemInformation => openSystemInformation ??= new DelegateCommand(PerformOpenSystemInformation);
    public ICommand OpenWaypointInformation => openWaypointInformation ??= new DelegateCommand(PerformOpenWaypointInformation);
    public ICommand LoadInformationCommand => loadInformationCommand ??= new DelegateCommand(LoadInformation);

    public Agent AgentInformation
    {
        get => agentInformation;
        set => SetProperty(ref agentInformation, value);
    }

    public Waypoint Headquarters
    {
        get => headquarters;
        set => SetProperty(ref headquarters, value);
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

    private async void LoadInformation()
    {
        this.AgentInformation = await spaceTradersApi.GetAgent();
        this.Headquarters = await spaceTradersApi.GetWaypoint(this.AgentInformation.Headquarters);
    }

    private void PerformOpenSystemInformation()
    {
        RemoveCurrentView();
        this.regionManager.RegisterViewWithRegion(RegionNames.MainAreaRegion, typeof(SystemInformationView));
        this.eventAggregator.GetEvent<SystemInformationEvent>().Publish(new SystemInformation
        {
            SystemSymbol = this.Headquarters.SystemSymbol
        });
    }

    private void PerformOpenWaypointInformation()
    {
        RemoveCurrentView();
        this.regionManager.RegisterViewWithRegion(RegionNames.MainAreaRegion, typeof(WaypointInformationView));
        this.eventAggregator.GetEvent<WaypointInformationEvent>().Publish(new WaypointInformation
        {
            WaypointSymbol = this.Headquarters.Symbol
        });
    }

    private void RemoveCurrentView()
    {
        var dashboardView = this.regionManager.Regions[RegionNames.MainAreaRegion].Views.OfType<DashboardView>().Single();
        this.regionManager.Regions[RegionNames.MainAreaRegion].Remove(dashboardView);
    }
}