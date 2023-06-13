using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Requests;

public class PostShipSellCargoRequest
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("units")]
    public int Units { get; set; }
}
