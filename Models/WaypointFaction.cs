using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class WaypointFaction
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}