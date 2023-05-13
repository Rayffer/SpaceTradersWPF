﻿using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class ShipFuel
{
    [JsonProperty("current")]
    public int Current { get; set; }

    [JsonProperty("capacity")]
    public int Capacity { get; set; }

    [JsonProperty("consumed")]
    public FuelConsumed Consumed { get; set; }
}