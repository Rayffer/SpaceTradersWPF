using Prism.Mvvm;

using SpaceTradersWPF.Services;

namespace SpaceTradersWPF.ViewModels;

internal class ShipAutomationViewModel : BindableBase
{
    private readonly IShipAutomationService shipAutomationService;
    private readonly ISpaceTradersApi spaceTradersApi;

    public ShipAutomationViewModel(
        IShipAutomationService shipAutomationService,
        ISpaceTradersApi spaceTradersApi)
    {
        this.shipAutomationService = shipAutomationService;
        this.spaceTradersApi = spaceTradersApi;
    }
}
