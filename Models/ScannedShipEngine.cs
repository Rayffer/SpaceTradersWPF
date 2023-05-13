using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class ScannedShipEngine
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}