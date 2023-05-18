using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class WaypointFaction
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}