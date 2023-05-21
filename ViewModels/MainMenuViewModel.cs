using System.Linq;
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

    public ICommand OpenShipInformationCommand => this.openShipInformationCommand ??= new DelegateCommand(this.OpenShipInformation);
    public ICommand ExitApplicationCommand => this.exitApplicationCommand ??= new DelegateCommand(this.ExitApplication);
    public ICommand LogoutAgentCommand => this.logoutAgentCommand ??= new DelegateCommand(this.LogoutAgent);
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
