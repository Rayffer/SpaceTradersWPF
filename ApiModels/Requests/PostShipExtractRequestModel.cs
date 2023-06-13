using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Requests;

internal class PostShipExtractRequestModel
{
    [JsonProperty("survey")]
    public Survey Survey { get; set; }
}
