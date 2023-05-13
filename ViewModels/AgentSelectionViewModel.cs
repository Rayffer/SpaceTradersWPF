using System.IO;

using Newtonsoft.Json;

using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

using SpaceTradersWPF.Services;
using SpaceTradersWPF.Views;

namespace SpaceTradersWPF.ViewModels;

internal class AgentSelectionViewModel : BindableBase
{
    private readonly IContainerExtension containerExtension;
    private readonly IRegionManager regionManager;
    private readonly ISpaceTradersApi spaceTradersApi;
    private DelegateCommand<string> registerAgentCommand;

    public AgentSelectionViewModel(
        IContainerExtension containerExtension,
        IRegionManager regionManager,
        ISpaceTradersApi spaceTradersApi)
    {
        this.containerExtension = containerExtension;
        this.regionManager = regionManager;
        this.spaceTradersApi = spaceTradersApi;
    }

    public DelegateCommand<string> RegisterAgentCommand => registerAgentCommand ??= new DelegateCommand<string>(RegisterAgent);

    private void RegisterAgent(string symbol)
    {
        var agentInformation = symbol.Length switch
        {
            >= 3 and <= 14 => this.spaceTradersApi.RegisterAgent(symbol, "COSMIC"),
            _ => default
        };

        if (agentInformation == null)
        {
            // Launch dialog that data is not valid
            return;
        }

        Directory.CreateDirectory("Data");
        File.WriteAllText($"Data/AccessToken.Token", agentInformation.Token);

        this.spaceTradersApi.SetAccessTokenHeader(agentInformation.Token);
        this.regionManager.RegisterViewWithRegion(RegionNames.MainMenuRegion, typeof(MainMenuView));
        this.regionManager.RegisterViewWithRegion(RegionNames.MainAreaRegion, typeof(DashboardView));
    }
}