using System;

using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class Chart
{
    [JsonProperty("waypointSymbol")]
    public string WaypointSymbol { get; set; }

    [JsonProperty("submittedBy")]
    public string SubmittedBy { get; set; }

    [JsonProperty("submittedOn")]
    public DateTime SubmittedOn { get; set; }
}