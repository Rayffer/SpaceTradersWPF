using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Responses;

internal class SurveyResponse
{
    [JsonProperty("cooldown")]
    public ShipCooldown Cooldown { get; set; }

    [JsonProperty("surveys")]
    public Survey[] Surveys { get; set; }
}
