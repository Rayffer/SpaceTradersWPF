using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Responses;

internal class AgentResponse
{
    [JsonProperty("agent")]
    public Agent Agent { get; set; }

    [JsonProperty("contract")]
    public Contract Contract { get; set; }

    [JsonProperty("faction")]
    public Faction Faction { get; set; }

    [JsonProperty("ship")]
    public Ship Ship { get; set; }

    [JsonProperty("token")]
    public string Token { get; set; }
}
