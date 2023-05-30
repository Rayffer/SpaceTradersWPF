using SpaceTradersWPF.ApiModels;

namespace SpaceTradersWPF.Events.Models;

internal class WaypointInformationEventArguments
{
    public string WaypointSymbol { get; internal set; }
    public Waypoint Waypoint { get; internal set; }
}
