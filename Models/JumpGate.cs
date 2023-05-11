namespace SpaceTradersWPF.Models;

public class JumpGate
{
    public int jumpRange { get; set; }
    public string factionSymbol { get; set; }
    public JumpGateConnectedsystem[] connectedSystems { get; set; }
}