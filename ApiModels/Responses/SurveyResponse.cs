using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Responses;

internal class SurveyResponse
{
    [JsonProperty("cooldown")]
    public ShipCooldown Cooldown { get; set; }

    [JsonProperty("survey")]
    public Survey Survey { get; set; }
}
