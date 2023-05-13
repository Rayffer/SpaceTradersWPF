using System;

using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class ShipCooldown
{
    [JsonProperty("shipSymbol")]
    public string ShipSymbol { get; set; }

    [JsonProperty("totalSeconds")]
    public int TotalSeconds { get; set; }

    [JsonProperty("remainingSeconds")]
    public int RemainingSeconds { get; set; }

    [JsonProperty("expiration")]
    public DateTime Expiration { get; set; }
}