using System;

using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class ContractTerms
{
    [JsonProperty("deadline")]
    public DateTime Deadline { get; set; }

    [JsonProperty("payment")]
    public ContractPayment Payment { get; set; }

    [JsonProperty("deliver")]
    public ContractDeliver[] Deliver { get; set; }
}