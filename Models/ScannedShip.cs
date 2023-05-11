namespace SpaceTradersWPF.Models;

public class ScannedShip
{
    public string symbol { get; set; }
    public ShipRegistration registration { get; set; }
    public ScannedShipNavigationInformation nav { get; set; }
    public ScannedShipFrame frame { get; set; }
    public ScannedShipReactor reactor { get; set; }
    public ScannedShipEngine engine { get; set; }
    public ScannedShipMount[] mounts { get; set; }
}