using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class JumpGate
{
    [JsonProperty("jumpRange")]
    public int JumpRange { get; set; }

    [JsonProperty("factionSymbol")]
    public string FactionSymbol { get; set; }

    [JsonProperty("connectedSystems")]
    public JumpGateConnectedsystem[] ConnectedSystems { get; set; }
}
