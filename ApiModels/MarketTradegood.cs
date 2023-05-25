using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class MarketTradegood
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("tradeVolume")]
    public int TradeVolume { get; set; }

    [JsonProperty("supply")]
    public string Supply { get; set; }

    [JsonProperty("purchasePrice")]
    public int PurchasePrice { get; set; }

    [JsonProperty("sellPrice")]
    public int SellPrice { get; set; }
}
