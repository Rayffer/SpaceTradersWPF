﻿using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class FactionTrait
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }
}
