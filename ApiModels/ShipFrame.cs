namespace SpaceTradersWPF.ApiModels;

using Newtonsoft.Json;

public class ShipFrame
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("condition")]
    public int Condition { get; set; }

    [JsonProperty("moduleSlots")]
    public int ModuleSlots { get; set; }

    [JsonProperty("mountingPoints")]
    public int MountingPoints { get; set; }

    [JsonProperty("fuelCapacity")]
    public int FuelCapacity { get; set; }

    [JsonProperty("requirements")]
    public Requirements Requirements { get; set; }
}
