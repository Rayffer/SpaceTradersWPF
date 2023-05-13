using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class SystemWaypoint
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("x")]
    public int X { get; set; }

    [JsonProperty("y")]
    public int Y { get; set; }
}