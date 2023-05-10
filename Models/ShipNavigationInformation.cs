namespace SpaceTradersWPF.Models;

public class ShipNavigationInformation
{
    public string systemSymbol { get; set; }
    public string waypointSymbol { get; set; }
    public Route route { get; set; }
    public string status { get; set; }
    public string flightMode { get; set; }
}
