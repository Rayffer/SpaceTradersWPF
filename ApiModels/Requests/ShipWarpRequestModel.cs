using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Requests;

internal class ShipWarpRequestModel
{
    [JsonProperty("waypointSymbol")]
    public string WaypointSymbol { get; set; }
}
