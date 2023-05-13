﻿using System;

using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class Contract
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("factionSymbol")]
    public string FactionSymbol { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("terms")]
    public ContractTerms Terms { get; set; }

    [JsonProperty("accepted")]
    public bool Accepted { get; set; }

    [JsonProperty("fulfilled")]
    public bool Fulfilled { get; set; }

    [JsonProperty("expiration")]
    public DateTime Expiration { get; set; }
}