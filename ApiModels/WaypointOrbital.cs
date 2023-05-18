using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class WaypointOrbital
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}