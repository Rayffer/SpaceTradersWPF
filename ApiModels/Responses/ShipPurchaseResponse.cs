using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Responses;

internal class ShipPurchaseResponse
{
    [JsonProperty("agent")]
    public Agent Agent { get; set; }

    [JsonProperty("ship")]
    public Ship Ship { get; set; }

    [JsonProperty("transaction")]
    public ShipyardTransaction Transaction { get; set; }
}
