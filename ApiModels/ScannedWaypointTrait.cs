namespace SpaceTradersWPF.ApiModels;

using Newtonsoft.Json;

public class ScannedWaypointTrait
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }
}