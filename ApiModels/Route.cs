using System;

using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class Route
{
    [JsonProperty("destination")]
    public Destination Destination { get; set; }

    [JsonProperty("departure")]
    public Departure Departure { get; set; }

    [JsonProperty("departureTime")]
    public DateTime DepartureTime { get; set; }

    [JsonProperty("arrival")]
    public DateTime Arrival { get; set; }
}
