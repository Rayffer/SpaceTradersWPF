using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Requests;

internal class ShipJumpRequestModel
{
    [JsonProperty("systemSymbol")]
    public string SystemSymbol { get; set; }
}
