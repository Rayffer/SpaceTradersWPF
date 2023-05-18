using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Responses;

internal class DeliverContractResponse
{
    [JsonProperty("contract")]
    public Contract Contract { get; set; }

    [JsonProperty("cargo")]
    public ShipCargo Cargo { get; set; }
}
