using System;

using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class FuelConsumed
{
    [JsonProperty("amount")]
    public int Amount { get; set; }

    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }
}