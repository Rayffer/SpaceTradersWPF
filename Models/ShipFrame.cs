namespace SpaceTradersWPF.Models;

public class ShipFrame
{
    public string symbol { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int condition { get; set; }
    public int moduleSlots { get; set; }
    public int mountingPoints { get; set; }
    public int fuelCapacity { get; set; }
    public Requirements requirements { get; set; }
}
