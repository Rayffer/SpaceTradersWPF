using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class ShipReactor
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("condition")]
    public int Condition { get; set; }

    [JsonProperty("powerOutput")]
    public int PowerOutput { get; set; }

    [JsonProperty("requirements")]
    public Requirements Requirements { get; set; }
}