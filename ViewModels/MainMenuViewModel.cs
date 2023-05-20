﻿using System.Linq;
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
    private DelegateCommand logoutAgentCommand;
    private DelegateCommand exitApplicationCommand;
    private DelegateCommand openShipInformationCommand;
    private readonly IRegionManager regionManager;

    public ICommand OpenShipInformationCommand => openShipInformationCommand ??= new DelegateCommand(OpenShipInformation);
    public ICommand ExitApplicationCommand => exitApplicationCommand ??= new DelegateCommand(ExitApplication);
    public ICommand LogoutAgentCommand => logoutAgentCommand ??= new DelegateCommand(LogoutAgent);
    public ICommand OpenSystemMapCommand => openSystemMapCommand ??= new DelegateCommand(OpenSystemMap);
    public ICommand OpenMapCommand => openMapCommand ??= new DelegateCommand(OpenMap);
    public ICommand OpenSystemsInformationCommand => openSystemsInformationCommand ??= new DelegateCommand(OpenSystemsInformation);
    public ICommand OpenFleetInformationCommand => openFleetInformationCommand ??= new DelegateCommand(OpenFleetInformation);
    public ICommand OpenContractsInformationCommand => openContractsInformationCommand ??= new DelegateCommand(OpenContractsInformation);
    public ICommand OpenAgentInformationCommand => openAgentInformationCommand ??= new DelegateCommand(OpenAgentInformation);

    public ICommand OpenDashboardCommand => openDashboardCommand ??= new DelegateCommand(OpenDashboard);

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
    }

    private void OpenFleetInformation()
    {
    }

    private void OpenSystemsInformation()
    {
    }

    private void OpenMap()
    {
    }

    private void OpenSystemMap()
    {
    }

    private void LogoutAgent()
    {
    }

    private void ExitApplication()
    {
    }

    private void OpenShipInformation()
    {
        if (this.regionManager.Regions[RegionNames.MainAreaRegion].Views.OfType<AgentFleetShipsOverviewView>().Any())
        {
            return;
        }

        this.regionManager.Regions[RegionNames.MainAreaRegion].RemoveAll();
        this.regionManager.RegisterViewWithRegion(RegionNames.MainAreaRegion, typeof(AgentFleetShipsOverviewView));
    }
}
