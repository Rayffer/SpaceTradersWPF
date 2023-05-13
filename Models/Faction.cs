using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class Faction
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("headquarters")]
    public string Headquarters { get; set; }

    [JsonProperty("traits")]
    public FactionTrait[] Traits { get; set; }
}