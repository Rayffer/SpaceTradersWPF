using System.IO;
using System.Threading.Tasks;

using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

using SpaceTradersWPF.Models;
using SpaceTradersWPF.Services;
using SpaceTradersWPF.Views;

namespace SpaceTradersWPF.ViewModels;

internal class AgentSelectionViewModel : BindableBase
{
    private readonly IContainerExtension containerExtension;
    private readonly IRegionManager regionManager;
    private readonly ISpaceTradersApi spaceTradersApi;
    private DelegateCommand<string> registerAgentCommand;
    private DelegateCommand loadInformationCommand;
    private Faction[] factions;
    private Faction selectedFaction;

    public DelegateCommand<string> RegisterAgentCommand => registerAgentCommand ??= new DelegateCommand<string>(RegisterAgent);
    public DelegateCommand LoadInformationCommand => loadInformationCommand ??= new DelegateCommand(async () => await LoadInformation());

    public Faction SelectedFaction
    {
        get => selectedFaction;
        set => SetProperty(ref selectedFaction, value);
    }

    public Faction[] Factions
    {
        get => factions;
        set => SetProperty(ref factions, value);
    }

    public AgentSelectionViewModel(
        IContainerExtension containerExtension,
        IRegionManager regionManager,
        ISpaceTradersApi spaceTradersApi)
    {
        this.containerExtension = containerExtension;
        this.regionManager = regionManager;
        this.spaceTradersApi = spaceTradersApi;
    }

    private void RegisterAgent(string symbol)
    {
        var agentInformation = symbol.Length switch
        {
            >= 3 and <= 14 => this.spaceTradersApi.RegisterAgent(symbol, SelectedFaction.Symbol),
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

        this.regionManager.Regions[RegionNames.SplashScreenRegion].RemoveAll();
        this.regionManager.RegisterViewWithRegion(RegionNames.SplashScreenRegion, typeof(AgentGreetingView));
    }

    private async Task LoadInformation()
    {
        this.Factions = await this.spaceTradersApi.GetFactions(1, 20);
    }
}