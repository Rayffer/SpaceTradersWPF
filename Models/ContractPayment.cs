using Newtonsoft.Json;

namespace SpaceTradersWPF.Models;

public class ContractPayment
{
    [JsonProperty("onAccepted")]
    public int OnAccepted { get; set; }

    [JsonProperty("onFulfilled")]
    public int OnFulfilled { get; set; }
}