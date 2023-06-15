using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Responses;

internal class ScanSystemsResponse
{
    [JsonProperty("cooldown")]
    public ShipCooldown cooldown { get; set; }

    [JsonProperty("systems")]
    public SolarSystem[] systems { get; set; }
}
