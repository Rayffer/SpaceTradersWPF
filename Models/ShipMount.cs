namespace SpaceTradersWPF.Models;

public class ShipMount
{
    public string symbol { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int strength { get; set; }
    public string[] deposits { get; set; }
    public Requirements requirements { get; set; }
}