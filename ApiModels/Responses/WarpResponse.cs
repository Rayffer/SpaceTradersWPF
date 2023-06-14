using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Responses;

internal class WarpResponse
{
    [JsonProperty("fuel")]
    public ShipFuel Fuel { get; set; }

    [JsonProperty("nav")]
    public ShipNavigationInformation NavigationInformation { get; set; }
}
