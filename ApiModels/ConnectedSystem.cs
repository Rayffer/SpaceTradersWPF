using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

internal class ConnectedSystem
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("sectorSymbol")]
    public string SectorSymbol { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("factionSymbol")]
    public string FactionSymbol { get; set; }

    [JsonProperty("x")]
    public int X { get; set; }

    [JsonProperty("y")]
    public int Y { get; set; }

    [JsonProperty("distance")]
    public int Distance { get; set; }
}
