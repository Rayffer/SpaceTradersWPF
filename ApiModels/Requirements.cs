using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class Requirements
{
    [JsonProperty("power")]
    public int Power { get; set; }

    [JsonProperty("crew")]
    public int Crew { get; set; }

    [JsonProperty("slots")]
    public int Slots { get; set; }
}
