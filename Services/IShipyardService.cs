using SpaceTradersWPF.ApiModels;

namespace SpaceTradersWPF.Services;

internal interface IShipyardService
{
    Shipyard GetShipyard(string waypointSymbol);

    void SaveShipyard(params Shipyard[] shipyardInformation);

    void RemoveShipyard(string waypointSymbol);
}
