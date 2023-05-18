using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class ScannedShipNavigationInformation
{
    [JsonProperty("systemSymbol")]
    public string SystemSymbol { get; set; }

    [JsonProperty("waypointSymbol")]
    public string WaypointSymbol { get; set; }

    [JsonProperty("route")]
    public Route Route { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("flightMode")]
    public string FlightMode { get; set; }
}