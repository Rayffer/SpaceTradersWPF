using System;

using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class ShipyardTransaction
{
    [JsonProperty("waypointSymbol")]
    public string WaypointSymbol { get; set; }

    [JsonProperty("shipSymbol")]
    public string ShipSymbol { get; set; }

    [JsonProperty("price")]
    public int Price { get; set; }

    [JsonProperty("agentSymbol")]
    public string AgentSymbol { get; set; }

    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }
}
