using System;

using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class Survey
{
    [JsonProperty("signature")]
    public string Signature { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("deposits")]
    public SurveyDeposit[] Deposits { get; set; }

    [JsonProperty("expiration")]
    public DateTime Expiration { get; set; }

    [JsonProperty("size")]
    public string Size { get; set; }
}