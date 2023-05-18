using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class ScannedWaypointFaction
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}