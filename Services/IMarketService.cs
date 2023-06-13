using SpaceTradersWPF.ApiModels;

namespace SpaceTradersWPF.Services;

internal interface IMarketService
{
    Market GetMarket(string waypointSymbol);

    void SaveMarket(params Market[] marketInformations);

    void RemoveMarket(string waypointSymbol);
}
