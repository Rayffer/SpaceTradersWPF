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
    private Ship selectedShip;
    private DelegateCommand loadShipsCommand;

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

    public AgentFleetShipsOverviewViewModel(ISpaceTradersApi spaceTradersApi)
    {
        this.spaceTradersApi = spaceTradersApi;
    }

    private async Task LoadShips()
    {
        this.Ships = await this.spaceTradersApi.GetShips(1, 20);
    }

    private DelegateCommand performExtractionCommand;
    public ICommand PerformExtractionCommand => performExtractionCommand ??= new DelegateCommand(PerformExtraction);

    private void PerformExtraction()
    {
    }

    private DelegateCommand performSurveyCommand;
    public ICommand PerformSurveyCommand => performSurveyCommand ??= new DelegateCommand(PerformSurvey);

    private void PerformSurvey()
    {
    }

    private DelegateCommand performWarpCommand;
    public ICommand PerformWarpCommand => performWarpCommand ??= new DelegateCommand(PerformWarp);

    private void PerformWarp()
    {
    }

    private DelegateCommand performOrbitCommand;
    public ICommand PerformOrbitCommand => performOrbitCommand ??= new DelegateCommand(PerformOrbit);

    private void PerformOrbit()
    {
    }

    private DelegateCommand performDockCommand;
    public ICommand PerformDockCommand => performDockCommand ??= new DelegateCommand(PerformDock);

    private void PerformDock()
    {
    }

    private DelegateCommand performNavigateCommand;
    public ICommand PerformNavigateCommand => performNavigateCommand ??= new DelegateCommand(PerformNavigate);

    private void PerformNavigate()
    {
    }

    private DelegateCommand performRefuelCommand;
    public ICommand PerformRefuelCommand => performRefuelCommand ??= new DelegateCommand(PerformRefuel);

    private void PerformRefuel()
    {
    }
}
