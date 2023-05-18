using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Responses;

internal class ScanWaypointsResponse
{
    [JsonProperty("cooldown")]
    public ShipCooldown Cooldown { get; set; }

    [JsonProperty("waypoints")]
    public Waypoint[] Waypoints { get; set; }
}
