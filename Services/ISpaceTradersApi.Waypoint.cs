using System.Threading.Tasks;

using SpaceTradersWPF.ApiModels;

namespace SpaceTradersWPF.Services;

internal partial interface ISpaceTradersApi
{
    Task<Waypoint> GetWaypoint(string symbol);

    Task<Waypoint[]> GetWaypoints(string waypointSymbol, int pageNumber, int pageSize);
}
