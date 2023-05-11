using System;

namespace SpaceTradersWPF.Models;

public class Route
{
    public Destination destination { get; set; }
    public Departure departure { get; set; }
    public DateTime departureTime { get; set; }
    public DateTime arrival { get; set; }
}