namespace SpaceTradersWPF.ApiModels;

using Newtonsoft.Json;

public class ShipNavigationInformation
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