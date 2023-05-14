using Newtonsoft.Json;

namespace SpaceTradersWPF.Models.Requests;

internal class ShipPurchaseRequestModel
{
    [JsonProperty("shipType")]
    public string ShipType { get; set; }

    [JsonProperty("waypointSymbol")]
    public string WaypointSymbol { get; set; }
}