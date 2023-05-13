using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class ShipModule
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("capacity")]
    public int Capacity { get; set; }

    [JsonProperty("range")]
    public int Range { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("requirements")]
    public Requirements Requirements { get; set; }
}