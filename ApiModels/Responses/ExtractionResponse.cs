using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Responses;

internal class ExtractionResponse
{
    [JsonProperty("cooldown")]
    public ShipCooldown Cooldown { get; set; }

    [JsonProperty("extraction")]
    public Extraction Extraction { get; set; }

    [JsonProperty("cargo")]
    public ShipCargo Cargo { get; set; }
}
