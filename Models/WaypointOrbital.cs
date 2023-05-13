using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class WaypointOrbital
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}