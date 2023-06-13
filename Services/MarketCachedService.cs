using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.Repositories;

namespace SpaceTradersWPF.Services;

internal class MarketCachedService : IMarketService
{
    private readonly IInformationRepository<Market> informationRepository;
    private readonly object lockObject = new();

    public MarketCachedService(
        IInformationRepository<Market> informationRepository)
    {
        this.informationRepository = informationRepository;
    }

    public Market GetMarket(string waypointSymbol)
    {
        lock (this.lockObject)
        {
            return this.informationRepository.Store.Where(x => x.Symbol == waypointSymbol).LastOrDefault();
        }
    }

    public void RemoveMarket(string waypointSymbol)
    {
        lock (this.lockObject)
        {
            var marketToRemove = this.informationRepository.Store.FirstOrDefault(x => x.Symbol == waypointSymbol);
            this.informationRepository.Store.Remove(marketToRemove);
            this.informationRepository.SaveInformation();
        }
    }

    public void SaveMarket(params Market[] marketInformations)
    {
        lock (this.lockObject)
        {
            this.informationRepository.SaveInformation(marketInformations);
        }
    }
}
