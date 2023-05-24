using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Requests;

public class ContractDeliverRequest
{
    [JsonProperty("shipSymbol")]
    public string ShipSymbol { get; set; }

    [JsonProperty("tradeSymbol")]
    public string TradeSymbol { get; set; }

    [JsonProperty("units")]
    public int Units { get; set; }
}
