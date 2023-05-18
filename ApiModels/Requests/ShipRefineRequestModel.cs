using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Requests;

internal class ShipRefineRequestModel
{
    [JsonProperty("produce")]
    public string Produce { get; set; }
}