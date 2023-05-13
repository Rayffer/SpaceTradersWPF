using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

internal class AgentResponse
{
    [JsonProperty("data")]
    public Data Data { get; set; }
}