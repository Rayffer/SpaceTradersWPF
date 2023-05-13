using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class Extraction
{
    [JsonProperty("shipSymbol")]
    public string ShipSymbol { get; set; }

    [JsonProperty("yield")]
    public ExtractionYield Yield { get; set; }
}