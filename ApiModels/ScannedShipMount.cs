using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class ScannedShipMount
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}
