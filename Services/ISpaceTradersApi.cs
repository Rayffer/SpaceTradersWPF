using System.Threading.Tasks;

using SpaceTradersWPF.Models;

namespace SpaceTradersWPF.Services;

internal interface ISpaceTradersApi
{
    AgentResponse RegisterAgent(string symbol, string faction);

    void SetAccessTokenHeader(string token);

    Task<Agent> GetAgent();

    Task<Waypoint> GetWaypoint(string symbol);
}