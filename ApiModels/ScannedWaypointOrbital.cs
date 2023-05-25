using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class ScannedWaypointOrbital
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}
