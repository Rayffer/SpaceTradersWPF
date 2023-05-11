namespace SpaceTradersWPF.Models;

public class Waypoint
{
    public string symbol { get; set; }
    public string type { get; set; }
    public string systemSymbol { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public WaypointOrbital[] orbitals { get; set; }
    public WaypointFaction faction { get; set; }
    public WaypointTrait[] traits { get; set; }
    public Chart chart { get; set; }
}