namespace SpaceTradersWPF.Models;

public class ShipReactor
{
    public string symbol { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int condition { get; set; }
    public int powerOutput { get; set; }
    public Requirements requirements { get; set; }
}