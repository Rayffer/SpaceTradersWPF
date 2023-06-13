using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Requests;

internal class PatchShipNavigationRequestModel
{
    [JsonProperty("flightMode")]
    public string FlightMode { get; set; }
}
