using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class SurveyDeposit
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}