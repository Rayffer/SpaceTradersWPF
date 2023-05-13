using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class Shiptype
{
    [JsonProperty("type")]
    public string Type { get; set; }
}