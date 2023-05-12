using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;

using SpaceTradersWPF.Models;
using SpaceTradersWPF.Services;

namespace SpaceTradersWPF.ViewModels;

internal class DashboardViewModel : BindableBase
{
    private readonly ISpaceTradersApi spaceTradersApi;

    private DelegateCommand loadInformationCommand;
    private Agent agentInformation;
    private Waypoint headquarters;

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
        ISpaceTradersApi spaceTradersApi)
    {
        this.spaceTradersApi = spaceTradersApi;
    }

    private async void LoadInformation()
    {
        this.AgentInformation = await spaceTradersApi.GetAgent();
        this.Headquarters = await spaceTradersApi.GetWaypoint(this.AgentInformation.headquarters);
    }
}