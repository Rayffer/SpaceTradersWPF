using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Responses;

internal class ApiResponse<ResponseType>
{
    [JsonProperty("data")]
    public ResponseType Data { get; set; }

    [JsonProperty("error")]
    public Error Error { get; set; }

    [JsonProperty("meta")]
    public Metadata Metadata { get; set; }
}
