namespace SpaceTradersWPF.Models;

public class ShipEngine
{
    public string symbol { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int condition { get; set; }
    public int speed { get; set; }
    public Requirements requirements { get; set; }
}
