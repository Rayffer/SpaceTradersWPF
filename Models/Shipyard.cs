namespace SpaceTradersWPF.Models;

public class Shipyard
{
    public string symbol { get; set; }
    public Shiptype[] shipTypes { get; set; }
    public ShipyardTransaction[] transactions { get; set; }
    public ShipyardShip[] ships { get; set; }
}
