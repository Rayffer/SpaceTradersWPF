using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

internal class ApiResponse<ResponseType>
{
    [JsonProperty("data")]
    public ResponseType Data { get; set; }
}