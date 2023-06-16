using System.Threading.Tasks;

using SpaceTradersWPF.ApiModels;

namespace SpaceTradersWPF.Services;

internal partial interface ISpaceTradersApi
{
    Task<Faction> GetFaction(string factionSymbol);

    Task<Faction[]> GetFactions(int pageNumber, int pageSize);
}
