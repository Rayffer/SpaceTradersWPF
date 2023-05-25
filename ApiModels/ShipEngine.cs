namespace SpaceTradersWPF.ApiModels;

using Newtonsoft.Json;

public class ShipEngine
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("condition")]
    public int Condition { get; set; }

    [JsonProperty("speed")]
    public int Speed { get; set; }

    [JsonProperty("requirements")]
    public Requirements Requirements { get; set; }
}
