using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Responses;

internal class ScanShipsResponse
{
    [JsonProperty("cooldown")]
    private ShipCooldown Cooldown { get; set; }

    [JsonProperty("ships")]
    private Ship[] Ships { get; set; }
}
