namespace SpaceTradersWPF.Models;

public class System
{
    public string symbol { get; set; }
    public string sectorSymbol { get; set; }
    public string type { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public SystemWaypoint[] waypoints { get; set; }
    public SystemFaction[] factions { get; set; }
}
