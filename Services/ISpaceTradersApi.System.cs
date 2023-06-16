using System.Threading.Tasks;

using SpaceTradersWPF.ApiModels;

namespace SpaceTradersWPF.Services;

internal partial interface ISpaceTradersApi
{
    Task<SolarSystem> GetSystem(string waypointSymbol);

    Task<SolarSystem[]> GetSystems(int pageNumber, int pageSize);
}
