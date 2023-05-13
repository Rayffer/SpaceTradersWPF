using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class ScannedWaypointOrbital
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}