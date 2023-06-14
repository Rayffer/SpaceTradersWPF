using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Requests;

public class PostShipTransferCargoRequest
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("units")]
    public int Units { get; set; }

    [JsonProperty("shipSymbol")]
    public string ShipSymbol { get; set; }
}
