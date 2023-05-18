using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class ScannedShipEngine
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}