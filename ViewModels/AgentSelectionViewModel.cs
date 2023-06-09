﻿using System.IO;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

using SpaceTradersWPF.ApiModels;
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

    public DelegateCommand<string> RegisterAgentCommand => this.registerAgentCommand ??= new DelegateCommand<string>(async (symbol) => await this.RegisterAgent(symbol));
    public DelegateCommand LoadInformationCommand => this.loadInformationCommand ??= new DelegateCommand(async () => await this.LoadInformation());

    public Faction SelectedFaction
    {
        get => this.selectedFaction;
        set => this.SetProperty(ref this.selectedFaction, value);
    }

    public Faction[] Factions
    {
        get => this.factions;
        set => this.SetProperty(ref this.factions, value);
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

    private async Task RegisterAgent(string symbol)
    {
        var agentInformation = symbol.Length switch
        {
            >= 3 and <= 14 => await this.spaceTradersApi.RegisterAgent(symbol, this.SelectedFaction.Symbol),
            _ => default
        };

        if (agentInformation == null)
        {
            // Launch dialog that data is not valid
            return;
        }

        Directory.CreateDirectory("Data");
        File.WriteAllText($"Data/AccessToken.Token", agentInformation.Token);
        File.WriteAllText($"Data/AgentInformation.json", JsonConvert.SerializeObject(agentInformation.Agent));

        this.spaceTradersApi.SetAccessTokenHeader(agentInformation.Token);

        this.regionManager.Regions[RegionNames.SplashScreenRegion].RemoveAll();
        this.regionManager.RegisterViewWithRegion(RegionNames.SplashScreenRegion, typeof(AgentGreetingView));
    }

    private async Task LoadInformation()
    {
        this.spaceTradersApi.ClearAccessToken();
        this.Factions = await this.spaceTradersApi.GetFactions(1, 20);
    }
}
