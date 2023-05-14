using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class ShipType
{
    [JsonProperty("type")]
    public string Type { get; set; }
}