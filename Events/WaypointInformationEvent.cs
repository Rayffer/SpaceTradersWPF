using Prism.Events;

namespace SpaceTradersWPF.Events;

internal class WaypointInformation
{
    public string WaypointSymbol { get; internal set; }
}

internal class WaypointInformationEvent : PubSubEvent<WaypointInformation>
{
}