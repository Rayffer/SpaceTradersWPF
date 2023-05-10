using System.IO;
using System.Windows;

using Newtonsoft.Json;

using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;

using SpaceTradersWPF.Services;

namespace SpaceTradersWPF.ViewModels;

internal class AgentSelectionViewModel
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

        if (Directory.Exists("Data") &&
            File.Exists("Data/AccessToken.Token") &&
            MessageBox.Show("Do you want to proceed with the current agent?", "Continue previous session", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            this.spaceTradersApi.SetAccessTokenHeader(File.ReadAllText("Data/AccessToken.Token"));
        }
    }

    public DelegateCommand<string> RegisterAgentCommand => registerAgentCommand ??= new DelegateCommand<string>(RegisterAgent);

    private void RegisterAgent(string symbol)
    {
        var agentInformation = symbol.Length switch
        {
            >= 3 and <= 14 => this.spaceTradersApi.RegisterAgent(symbol, "COSMIC"),
            _ => default
        };

        Directory.CreateDirectory("Data");
        Directory.CreateDirectory("Data/Factions");
        Directory.CreateDirectory("Data/Contracts");
        Directory.CreateDirectory("Data/Ships");
        File.WriteAllText($"Data/Agent.json", JsonConvert.SerializeObject(agentInformation.data.agent, Formatting.Indented));
        File.WriteAllText($"Data/AccessToken.Token", agentInformation.data.token);
        File.WriteAllText($"Data/Factions/Faction_{agentInformation.data.faction.symbol}.json", JsonConvert.SerializeObject(agentInformation.data.faction, Formatting.Indented));
        File.WriteAllText($"Data/Contracts/Contract_{agentInformation.data.contract.id}.json", JsonConvert.SerializeObject(agentInformation.data.contract, Formatting.Indented));
        File.WriteAllText($"Data/Ships/Ship_{agentInformation.data.ship.symbol}.json", JsonConvert.SerializeObject(agentInformation.data.ship, Formatting.Indented));
    }
}