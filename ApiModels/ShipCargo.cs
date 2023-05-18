namespace SpaceTradersWPF.ApiModels;

using Newtonsoft.Json;

public class ShipCargo
{
    [JsonProperty("capacity")]
    public int Capacity { get; set; }

    [JsonProperty("units")]
    public int Units { get; set; }

    [JsonProperty("inventory")]
    public Inventory[] Inventory { get; set; }
}