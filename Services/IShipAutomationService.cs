using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.Models;

namespace SpaceTradersWPF.Services;

internal interface IShipAutomationService
{
    Survey GetShipAutomation(string shipSymbol);

    void SaveShipAutomation(params ShipAutomation[] shipAutomations);

    void RemoveShipAutomation(ShipAutomation shipAutomation);
}
