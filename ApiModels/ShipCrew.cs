namespace SpaceTradersWPF.ApiModels;

using Newtonsoft.Json;

public class ShipCrew
{
    [JsonProperty("current")]
    public int Current { get; set; }

    [JsonProperty("required")]
    public int Required { get; set; }

    [JsonProperty("capacity")]
    public int Capacity { get; set; }

    [JsonProperty("rotation")]
    public string Rotation { get; set; }

    [JsonProperty("morale")]
    public int Morale { get; set; }

    [JsonProperty("wages")]
    public int Wages { get; set; }
}