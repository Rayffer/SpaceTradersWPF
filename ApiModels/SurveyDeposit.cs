using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class SurveyDeposit
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}
