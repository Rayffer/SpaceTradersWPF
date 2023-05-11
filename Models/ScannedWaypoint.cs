namespace SpaceTradersWPF.Models;

public class ScannedWaypoint
{
    public string symbol { get; set; }
    public string type { get; set; }
    public string systemSymbol { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public ScannedWaypointOrbital[] orbitals { get; set; }
    public ScannedWaypointFaction faction { get; set; }
    public ScannedWaypointTrait[] traits { get; set; }
    public ScannedWaypointChart chart { get; set; }
}