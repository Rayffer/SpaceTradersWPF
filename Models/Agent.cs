using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

internal class Agent
{
    [JsonProperty("accountId")]
    public string AccountId { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("headquarters")]
    public string Headquarters { get; set; }

    [JsonProperty("credits")]
    public int Credits { get; set; }
}