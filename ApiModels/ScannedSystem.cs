using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class ScannedSystem
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("sectorSymbol")]
    public string SectorSymbol { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("x")]
    public int X { get; set; }

    [JsonProperty("y")]
    public int Y { get; set; }

    [JsonProperty("distance")]
    public int Distance { get; set; }
}