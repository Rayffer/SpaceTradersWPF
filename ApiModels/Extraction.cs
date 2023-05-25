using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class Extraction
{
    [JsonProperty("shipSymbol")]
    public string ShipSymbol { get; set; }

    [JsonProperty("yield")]
    public ExtractionYield Yield { get; set; }
}
