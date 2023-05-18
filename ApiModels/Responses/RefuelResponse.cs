using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Responses;

internal class RefuelResponse
{
    [JsonProperty("agent")]
    public Agent Agent { get; set; }

    [JsonProperty("fuel")]
    public ShipFuel Fuel { get; set; }
}
