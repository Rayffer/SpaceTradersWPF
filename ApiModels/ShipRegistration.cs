using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class ShipRegistration
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("factionSymbol")]
    public string FactionSymbol { get; set; }

    [JsonProperty("role")]
    public string Role { get; set; }
}