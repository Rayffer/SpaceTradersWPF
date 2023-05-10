using System;

namespace SpaceTradersWPF.Models;

public class ShipyardTransaction
{
    public string waypointSymbol { get; set; }
    public string shipSymbol { get; set; }
    public int price { get; set; }
    public string agentSymbol { get; set; }
    public DateTime timestamp { get; set; }
}
