using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class ScannedShipFrame
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}
