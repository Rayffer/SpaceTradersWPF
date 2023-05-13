using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class ScannedShipMount
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}