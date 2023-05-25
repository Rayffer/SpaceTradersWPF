using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class ScannedShip
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("registration")]
    public ShipRegistration Registration { get; set; }

    [JsonProperty("nav")]
    public ScannedShipNavigationInformation Nav { get; set; }

    [JsonProperty("frame")]
    public ScannedShipFrame Frame { get; set; }

    [JsonProperty("reactor")]
    public ScannedShipReactor Reactor { get; set; }

    [JsonProperty("engine")]
    public ScannedShipEngine Engine { get; set; }

    [JsonProperty("mounts")]
    public ScannedShipMount[] Mounts { get; set; }
}
