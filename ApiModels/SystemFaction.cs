using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class SystemFaction
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}
