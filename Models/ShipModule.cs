namespace SpaceTradersWPF.Models;

public class ShipModule
{
    public string symbol { get; set; }
    public int capacity { get; set; }
    public int range { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public Requirements requirements { get; set; }
}