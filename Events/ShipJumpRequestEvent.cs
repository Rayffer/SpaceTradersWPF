using Prism.Events;

using SpaceTradersWPF.Events.Models;

namespace SpaceTradersWPF.Events;

internal class ShipJumpRequestEvent : PubSubEvent<ShipJumpRequestEventArguments>
{
}
