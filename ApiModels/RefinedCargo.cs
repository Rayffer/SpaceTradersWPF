using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class RefinedCargo
{
    [JsonProperty("tradeSymbol")]
    public string TradeSymbol { get; set; }

    [JsonProperty("units")]
    public int Units { get; set; }
}