using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class ShipyardShip
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("purchasePrice")]
    public int PurchasePrice { get; set; }

    [JsonProperty("frame")]
    public ShipFrame Frame { get; set; }

    [JsonProperty("reactor")]
    public ShipReactor Reactor { get; set; }

    [JsonProperty("engine")]
    public ShipEngine Engine { get; set; }

    [JsonProperty("modules")]
    public ShipModule[] Modules { get; set; }

    [JsonProperty("mounts")]
    public ShipMount[] Mounts { get; set; }
}