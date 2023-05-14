using Newtonsoft.Json;

namespace SpaceTradersWPF.Models.Requests;

internal class ShipRefineRequestModel
{
    [JsonProperty("produce")]
    public string Produce { get; set; }
}