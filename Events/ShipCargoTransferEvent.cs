using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Events;

using SpaceTradersWPF.Events.Models;

namespace SpaceTradersWPF.Events
{
    internal class ShipCargoTransferEvent : PubSubEvent<ShipCargoTransferEventArguments>
    {
    }
}
