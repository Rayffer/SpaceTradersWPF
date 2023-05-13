using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class Metadata
{
    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("page")]
    public int Page { get; set; }

    [JsonProperty("limit")]
    public int Limit { get; set; }
}