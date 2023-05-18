using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class ScannedWaypoint
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("systemSymbol")]
    public string SystemSymbol { get; set; }

    [JsonProperty("x")]
    public int X { get; set; }

    [JsonProperty("y")]
    public int Y { get; set; }

    [JsonProperty("orbitals")]
    public ScannedWaypointOrbital[] Orbitals { get; set; }

    [JsonProperty("faction")]
    public ScannedWaypointFaction Faction { get; set; }

    [JsonProperty("traits")]
    public ScannedWaypointTrait[] Traits { get; set; }

    [JsonProperty("chart")]
    public ScannedWaypointChart Chart { get; set; }
}