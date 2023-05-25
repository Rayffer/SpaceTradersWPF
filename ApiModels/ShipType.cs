using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class ShipType
{
    [JsonProperty("type")]
    public string Type { get; set; }
}
