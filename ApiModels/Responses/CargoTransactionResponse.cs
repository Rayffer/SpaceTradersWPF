using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Responses;

internal class CargoTransactionResponse
{
    [JsonProperty("agent")]
    public Agent Agent { get; set; }

    [JsonProperty("cargo")]
    public ShipCargo Cargo { get; set; }

    [JsonProperty("transaction")]
    public MarketTransaction Transaction { get; set; }
}
