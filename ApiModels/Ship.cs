namespace SpaceTradersWPF.ApiModels;

using Newtonsoft.Json;

public class Ship
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("registration")]
    public ShipRegistration Registration { get; set; }

    [JsonProperty("nav")]
    public ShipNavigationInformation NavigationInformation { get; set; }

    [JsonProperty("crew")]
    public ShipCrew Crew { get; set; }

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

    [JsonProperty("cargo")]
    public ShipCargo Cargo { get; set; }

    [JsonProperty("fuel")]
    public ShipFuel Fuel { get; set; }
}