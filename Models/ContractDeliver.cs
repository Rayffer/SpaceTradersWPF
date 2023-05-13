using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class ContractDeliver
{
    [JsonProperty("tradeSymbol")]
    public string TradeSymbol { get; set; }

    [JsonProperty("destinationSymbol")]
    public string DestinationSymbol { get; set; }

    [JsonProperty("unitsRequired")]
    public int UnitsRequired { get; set; }

    [JsonProperty("unitsFulfilled")]
    public int UnitsFulfilled { get; set; }
}