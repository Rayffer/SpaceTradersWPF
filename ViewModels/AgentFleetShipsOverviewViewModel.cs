using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;

using SpaceTradersWPF.Models;
using SpaceTradersWPF.Services;

namespace SpaceTradersWPF.ViewModels;

internal class AgentFleetShipsOverviewViewModel : BindableBase
{
    private readonly ISpaceTradersApi spaceTradersApi;
    private IEnumerable<Ship> ships;
    private DelegateCommand loadShipsCommand;

    public IEnumerable<Ship> Ships
    {
        get => ships;
        set => SetProperty(ref ships, value);
    }

    public ICommand LoadShipsCommand => loadShipsCommand ??= new DelegateCommand(async () => await LoadShips());

    public AgentFleetShipsOverviewViewModel(ISpaceTradersApi spaceTradersApi)
    {
        this.spaceTradersApi = spaceTradersApi;
    }

    private async Task LoadShips()
    {
        this.Ships = await this.spaceTradersApi.GetShips(1, 20);
    }
}