using Prism.Events;

using SpaceTradersWPF.Events.Models;

namespace SpaceTradersWPF.Events;

internal class ShipNavigationRequestEvent : PubSubEvent<ShipNavigationRequestEventArguments>
{
}
