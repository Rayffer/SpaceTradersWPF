using System.Threading.Tasks;
using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using SpaceTradersWPF.Models;
using SpaceTradersWPF.Services;
using SpaceTradersWPF.Views;

namespace SpaceTradersWPF.ViewModels;

internal class AgentGreetingViewModel : BindableBase
{
    private readonly ISpaceTradersApi spaceTradersApi;
    private readonly IRegionManager regionManager;
    private Agent currentAgent;
    private DelegateCommand loadAgentInformationCommand;

    public Agent CurrentAgent
    {
        get => currentAgent;
        set => SetProperty(ref currentAgent, value);
    }

    public AgentGreetingViewModel(
        ISpaceTradersApi spaceTradersApi,
        IRegionManager regionManager)
    {
        this.spaceTradersApi = spaceTradersApi;
        this.regionManager = regionManager;
    }

    public ICommand LoadAgentInformationCommand => loadAgentInformationCommand ??= new DelegateCommand(async () => await this.LoadAgentInformation());

    private async Task LoadAgentInformation()
    {
        await Task.Delay(500);
        this.CurrentAgent = await this.spaceTradersApi.GetAgent();
        await Task.Delay(3000);
        this.regionManager.Regions[RegionNames.SplashScreenRegion].RemoveAll();
        this.regionManager.RegisterViewWithRegion(RegionNames.MainMenuRegion, typeof(MainMenuView));
        this.regionManager.RegisterViewWithRegion(RegionNames.MainAreaRegion, typeof(DashboardView));
    }
}