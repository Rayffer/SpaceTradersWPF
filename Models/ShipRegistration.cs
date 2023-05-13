using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class ShipRegistration
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("factionSymbol")]
    public string FactionSymbol { get; set; }

    [JsonProperty("role")]
    public string Role { get; set; }
}