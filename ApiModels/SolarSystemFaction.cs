using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class SolarSystemFaction
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}
