using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Responses;

public class CargoJettisonResponse
{
    [JsonProperty("cargo")]
    public ShipCargo Cargo { get; set; }
}
