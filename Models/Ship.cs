namespace SpaceTradersWPF.Models;

public class Ship
{
    public string symbol { get; set; }
    public ShipRegistration registration { get; set; }
    public ScannedShipNavigationInformation nav { get; set; }
    public ShipCrew crew { get; set; }
    public ScannedShipFrame frame { get; set; }
    public ScannedShipReactor reactor { get; set; }
    public ScannedShipEngine engine { get; set; }
    public ShipModule[] modules { get; set; }
    public ScannedShipMount[] mounts { get; set; }
    public ShipCargo cargo { get; set; }
    public ShipFuel fuel { get; set; }
}
