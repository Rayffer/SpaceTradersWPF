using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class Shipyard
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("shipTypes")]
    public ShipType[] ShipTypes { get; set; }

    [JsonProperty("transactions")]
    public ShipyardTransaction[] Transactions { get; set; }

    [JsonProperty("ships")]
    public ShipyardShip[] Ships { get; set; }
}