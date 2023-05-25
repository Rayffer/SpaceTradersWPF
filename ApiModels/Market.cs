using Newtonsoft.Json;

namespace SpaceTradersWPF.ApiModels;

public class Market
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("exports")]
    public MarketExport[] Exports { get; set; }

    [JsonProperty("imports")]
    public MarketImport[] Imports { get; set; }

    [JsonProperty("exchange")]
    public MarketExchange[] Exchange { get; set; }

    [JsonProperty("transactions")]
    public MarketTransaction[] Transactions { get; set; }

    [JsonProperty("tradeGoods")]
    public MarketTradegood[] TradeGoods { get; set; }
}
