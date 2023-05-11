namespace SpaceTradersWPF.Models;

public class ShipCargo
{
    public int capacity { get; set; }
    public int units { get; set; }
    public Inventory[] inventory { get; set; }
}