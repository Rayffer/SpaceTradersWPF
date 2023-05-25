using System;

using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class ScannedWaypointChart
{
    [JsonProperty("waypointSymbol")]
    public string WaypointSymbol { get; set; }

    [JsonProperty("submittedBy")]
    public string SubmittedBy { get; set; }

    [JsonProperty("submittedOn")]
    public DateTime SubmittedOn { get; set; }
}
