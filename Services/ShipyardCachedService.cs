using System.Linq;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.Repositories;

namespace SpaceTradersWPF.Services;

internal class ShipyardCachedService : IShipyardService
{
    private readonly IInformationRepository<Shipyard> informationRepository;
    private readonly object lockObject = new();

    public ShipyardCachedService(
        IInformationRepository<Shipyard> informationRepository)
    {
        this.informationRepository = informationRepository;
    }

    public Shipyard GetShipyard(string waypointSymbol)
    {
        lock (this.lockObject)
        {
            return this.informationRepository.Store.Where(x => x.Symbol == waypointSymbol).LastOrDefault();
        }
    }

    public void RemoveShipyard(string waypointSymbol)
    {
        lock (this.lockObject)
        {
            var shipyardToRemove = this.informationRepository.Store.FirstOrDefault(x => x.Symbol == waypointSymbol);
            this.informationRepository.Store.Remove(shipyardToRemove);
            this.informationRepository.SaveInformation();
        }
    }

    public void SaveShipyard(params Shipyard[] shipyardInformation)
    {
        lock (this.lockObject)
        {
            this.informationRepository.SaveInformation(shipyardInformation);
        }
    }
}
