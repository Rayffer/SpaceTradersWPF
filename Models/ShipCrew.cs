namespace SpaceTradersWPF.Models;

public class ShipCrew
{
    public int current { get; set; }
    public int required { get; set; }
    public int capacity { get; set; }
    public string rotation { get; set; }
    public int morale { get; set; }
    public int wages { get; set; }
}