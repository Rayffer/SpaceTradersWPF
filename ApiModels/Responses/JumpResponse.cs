using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Responses;

internal class JumpResponse
{
    [JsonProperty("cooldown")]
    public ShipCooldown Cooldown { get; set; }

    [JsonProperty("nav")]
    public ShipNavigationInformation NavigationInformation { get; set; }
}
