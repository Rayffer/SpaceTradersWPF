using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class SystemFaction
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}