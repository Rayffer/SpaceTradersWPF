using System;

namespace SpaceTradersWPF.Models;

public class ContractTerms
{
    public DateTime deadline { get; set; }
    public ContractPayment payment { get; set; }
    public ContractDeliver[] deliver { get; set; }
}