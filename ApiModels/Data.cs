using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class Data
{
    [JsonProperty("page")]
    public string[] Page { get; set; }
}
