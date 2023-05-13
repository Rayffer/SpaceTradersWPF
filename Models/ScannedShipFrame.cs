using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class ScannedShipFrame
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}