using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class ScannedShipReactor
{
    [JsonProperty("symbol")]
    public string symbol { get; set; }
}