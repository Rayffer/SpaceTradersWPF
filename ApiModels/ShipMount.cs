using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class ShipMount
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("strength")]
    public int Strength { get; set; }

    [JsonProperty("deposits")]
    public string[] Deposits { get; set; }

    [JsonProperty("requirements")]
    public Requirements Requirements { get; set; }
}
