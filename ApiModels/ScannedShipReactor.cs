using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class ScannedShipReactor
{
    [JsonProperty("symbol")]
    public string symbol { get; set; }
}