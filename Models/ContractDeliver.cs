namespace SpaceTradersWPF.Models;

public class ContractDeliver
{
    public string tradeSymbol { get; set; }
    public string destinationSymbol { get; set; }
    public int unitsRequired { get; set; }
    public int unitsFulfilled { get; set; }
}
