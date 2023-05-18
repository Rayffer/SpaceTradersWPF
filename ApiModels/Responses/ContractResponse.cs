using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Responses;

internal class ContractResponse
{
    [JsonProperty("contract")]
    public Contract Contract { get; set; }

    [JsonProperty("agent")]
    public Agent Agent { get; set; }
}
