using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class ScannedWaypointFaction
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}