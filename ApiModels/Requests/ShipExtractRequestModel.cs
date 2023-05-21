using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Requests;

internal class ShipExtractRequestModel
{
    [JsonProperty("survey")]
    public Survey Survey { get; set; }
}
