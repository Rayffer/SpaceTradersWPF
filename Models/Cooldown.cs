using System;

namespace SpaceTradersWPF.Models;

public class Cooldown
{
    public string shipSymbol { get; set; }
    public int totalSeconds { get; set; }
    public int remainingSeconds { get; set; }
    public DateTime expiration { get; set; }
}