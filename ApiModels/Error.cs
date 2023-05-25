using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class Error
{
    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("data")]
    public Data Data { get; set; }
}
