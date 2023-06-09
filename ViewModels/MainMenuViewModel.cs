﻿using System.Linq;
using System.Windows;
using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using SpaceTradersWPF.Views;

namespace SpaceTradersWPF.ViewModels;

internal class MainMenuViewModel : BindableBase
{
    private DelegateCommand openDashboardCommand;
    private DelegateCommand openAgentInformationCommand;
    private DelegateCommand openContractsInformationCommand;
    private DelegateCommand openFleetInformationCommand;
    private DelegateCommand openSystemsInformationCommand;
    private DelegateCommand openMapCommand;
    private DelegateCommand openSystemMapCommand;
    private DelegateCommand exitApplicationCommand;
    private DelegateCommand openShipInformationCommand;
    private readonly IRegionManager regionManager;

    public ICommand OpenShipInformationCommand => this.openShipInformationCommand ??= new DelegateCommand(this.OpenShipInformation);
    public ICommand ExitApplicationCommand => this.exitApplicationCommand ??= new DelegateCommand(this.ExitApplication);
    public ICommand OpenSystemMapCommand => this.openSystemMapCommand ??= new DelegateCommand(this.OpenSystemMap);
    public ICommand OpenMapCommand => this.openMapCommand ??= new DelegateCommand(this.OpenMap);
    public ICommand OpenSystemsInformationCommand => this.openSystemsInformationCommand ??= new DelegateCommand(this.OpenSystemsInformation);
    public ICommand OpenFleetInformationCommand => this.openFleetInformationCommand ??= new DelegateCommand(this.OpenFleetInformation);
    public ICommand OpenContractsInformationCommand => this.openContractsInformationCommand ??= new DelegateCommand(this.OpenContractsInformation);
    public ICommand OpenAgentInformationCommand => this.openAgentInformationCommand ??= new DelegateCommand(this.OpenAgentInformation);

    public ICommand OpenDashboardCommand => this.openDashboardCommand ??= new DelegateCommand(this.OpenDashboard);

    public MainMenuViewModel(IRegionManager regionManager)
    {
        this.regionManager = regionManager;
    }

    private void OpenDashboard()
    {
        if (this.regionManager.Regions[RegionNames.MainAreaRegion].Views.OfType<DashboardView>().Any())
        {
            return;
        }

        this.regionManager.Regions[RegionNames.MainAreaRegion].RemoveAll();
        this.regionManager.RegisterViewWithRegion(RegionNames.MainAreaRegion, typeof(DashboardView));
    }

    private void OpenAgentInformation()
    {
    }

    private void OpenContractsInformation()
    {
        if (this.regionManager.Regions[RegionNames.MainAreaRegion].Views.OfType<AgentContractsView>().Any())
        {
            return;
        }

        this.regionManager.Regions[RegionNames.MainAreaRegion].RemoveAll();
        this.regionManager.RegisterViewWithRegion(RegionNames.MainAreaRegion, typeof(AgentContractsView));
    }

    private void OpenFleetInformation()
    {
    }

    private void OpenSystemsInformation()
    {
        if (this.regionManager.Regions[RegionNames.MainAreaRegion].Views.OfType<SystemInformationView>().Any())
        {
            return;
        }

        this.regionManager.Regions[RegionNames.MainAreaRegion].RemoveAll();
        this.regionManager.RegisterViewWithRegion(RegionNames.MainAreaRegion, typeof(SystemInformationView));
    }

    private void OpenMap()
    {
    }

    private void OpenSystemMap()
    {
    }

    private void ExitApplication()
    {
        Application.Current.Shutdown();
    }

    private void OpenShipInformation()
    {
        if (this.regionManager.Regions[RegionNames.MainAreaRegion].Views.OfType<AgentShipsView>().Any())
        {
            return;
        }

        this.regionManager.Regions[RegionNames.MainAreaRegion].RemoveAll();
        this.regionManager.RegisterViewWithRegion(RegionNames.MainAreaRegion, typeof(AgentShipsView));
    }
}
