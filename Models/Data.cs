using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class Data
{
    [JsonProperty("page")]
    public string[] Page { get; set; }
}