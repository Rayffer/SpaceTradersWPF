using System.Collections.Generic;
using System.Linq;
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
    private Ship selectedShip;
    private DelegateCommand loadShipsCommand;
    private DelegateCommand<Ship> performExtractionCommand;
    private DelegateCommand<Ship> performSurveyCommand;
    private DelegateCommand<Ship> performWarpCommand;
    private DelegateCommand<Ship> performOrbitCommand;
    private DelegateCommand<Ship> performDockCommand;
    private DelegateCommand<Ship> performNavigateCommand;
    private DelegateCommand<Ship> performRefuelCommand;

    public Ship SelectedShip
    {
        get => selectedShip;
        set => SetProperty(ref selectedShip, value);
    }

    public IEnumerable<Ship> Ships
    {
        get => ships;
        set => SetProperty(ref ships, value);
    }

    public ICommand LoadShipsCommand => loadShipsCommand ??= new DelegateCommand(async () => await LoadShips());
    public ICommand PerformExtractionCommand => performExtractionCommand ??= new DelegateCommand<Ship>(async ship => await PerformExtraction(ship), ship => ship != null);
    public ICommand PerformSurveyCommand => performSurveyCommand ??= new DelegateCommand<Ship>(async ship => await PerformSurvey(ship), ship => ship != null);
    public ICommand PerformWarpCommand => performWarpCommand ??= new DelegateCommand<Ship>(async ship => await PerformWarp(ship), ship => ship != null);
    public ICommand PerformOrbitCommand => performOrbitCommand ??= new DelegateCommand<Ship>(async ship => await PerformOrbit(ship), ship => ship != null);
    public ICommand PerformDockCommand => performDockCommand ??= new DelegateCommand<Ship>(async ship => await PerformDock(ship), ship => ship != null);
    public ICommand PerformNavigateCommand => performNavigateCommand ??= new DelegateCommand<Ship>(async ship => await PerformNavigate(ship), ship => ship != null);
    public ICommand PerformRefuelCommand => performRefuelCommand ??= new DelegateCommand<Ship>(async ship => await PerformRefuel(ship), ship => ship != null);

    public AgentFleetShipsOverviewViewModel(ISpaceTradersApi spaceTradersApi)
    {
        this.spaceTradersApi = spaceTradersApi;
    }

    private async Task LoadShips()
    {
        this.Ships = await this.spaceTradersApi.GetShips(1, 20);
    }

    private async Task PerformExtraction(Ship ship)
    {
        _ = await this.spaceTradersApi.PostShipExtractResources(ship.Symbol);
        await RefreshShips(ship);
    }

    private async Task PerformSurvey(Ship ship)
    {
    }

    private async Task PerformWarp(Ship ship)
    {
    }

    private async Task PerformOrbit(Ship ship)
    {
        _ = await this.spaceTradersApi.PostShipOrbit(ship.Symbol);
        await RefreshShips(ship);
    }

    private async Task PerformDock(Ship ship)
    {
        _ = await this.spaceTradersApi.PostShipDock(ship.Symbol);
        await RefreshShips(ship);
    }

    private async Task PerformNavigate(Ship ship)
    {
    }

    private async Task PerformRefuel(Ship ship)
    {
        _ = await this.spaceTradersApi.PostShipRefuel(ship.Symbol);
        await RefreshShips(ship);
    }

    private async Task RefreshShips(Ship ship)
    {
        this.Ships = await this.spaceTradersApi.GetShips(1, 20);
        this.SelectedShip = this.Ships.First(x => x.Symbol == ship.Symbol);
    }
}