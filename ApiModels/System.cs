using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class System
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("sectorSymbol")]
    public string SectorSymbol { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("x")]
    public int X { get; set; }

    [JsonProperty("y")]
    public int Y { get; set; }

    [JsonProperty("waypoints")]
    public SystemWaypoint[] Waypoints { get; set; }

    [JsonProperty("factions")]
    public SystemFaction[] Factions { get; set; }
}