using System;

using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class MarketTransaction
{
    [JsonProperty("waypointSymbol")]
    public string WaypointSymbol { get; set; }

    [JsonProperty("shipSymbol")]
    public string ShipSymbol { get; set; }

    [JsonProperty("tradeSymbol")]
    public string TradeSymbol { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("units")]
    public int Units { get; set; }

    [JsonProperty("pricePerUnit")]
    public int PricePerUnit { get; set; }

    [JsonProperty("totalPrice")]
    public int TotalPrice { get; set; }

    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }
}
