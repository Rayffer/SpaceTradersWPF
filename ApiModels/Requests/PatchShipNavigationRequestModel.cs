using Newtonsoft.Json;

namespace SpaceTradersWPF.Services
{
    internal class PatchShipNavigationRequestModel
    {
        [JsonProperty("flightMode")]
        public string FlightMode { get; set; }
    }
}