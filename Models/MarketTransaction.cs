using System;

namespace SpaceTradersWPF.Models;

public class MarketTransaction
{
    public string waypointSymbol { get; set; }
    public string shipSymbol { get; set; }
    public string tradeSymbol { get; set; }
    public string type { get; set; }
    public int units { get; set; }
    public int pricePerUnit { get; set; }
    public int totalPrice { get; set; }
    public DateTime timestamp { get; set; }
}
