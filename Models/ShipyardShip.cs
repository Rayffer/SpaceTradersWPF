namespace SpaceTradersWPF.Models;

public class ShipyardShip
{
    public string type { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int purchasePrice { get; set; }
    public ShipFrame frame { get; set; }
    public ShipReactor reactor { get; set; }
    public ShipEngine engine { get; set; }
    public ShipModule[] modules { get; set; }
    public ShipMount[] mounts { get; set; }
}