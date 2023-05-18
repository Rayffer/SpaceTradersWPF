using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class ExtractionYield
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("units")]
    public int Units { get; set; }
}