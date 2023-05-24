using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Responses;

internal class ChartResponse
{
    [JsonProperty("chart")]
    public Chart Chart { get; set; }

    [JsonProperty("waypoint")]
    public Chart Waypoint { get; set; }
}
