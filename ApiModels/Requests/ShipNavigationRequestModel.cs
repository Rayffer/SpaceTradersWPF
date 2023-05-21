using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Requests;

internal class ShipNavigationRequestModel
{
    [JsonProperty("waypointSymbol")]
    public string WaypointSymbol { get; set; }
}
