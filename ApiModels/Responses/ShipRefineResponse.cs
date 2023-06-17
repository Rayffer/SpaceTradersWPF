using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels.Responses
{
    internal class ShipRefineResponse
    {
        [JsonProperty("cargo")]
        public ShipCargo Cargo { get; set; }

        [JsonProperty("cooldown")]
        public ShipCooldown Cooldown { get; set; }

        [JsonProperty("produced")]
        public RefinedCargo[] Produced { get; set; }

        [JsonProperty("consumed")]
        public RefinedCargo[] Consumed { get; set; }
    }
}
