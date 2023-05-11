namespace SpaceTradersWPF.Models;

public class Market
{
    public string symbol { get; set; }
    public MarketExport[] exports { get; set; }
    public MarketImport[] imports { get; set; }
    public MarketExchange[] exchange { get; set; }
    public MarketTransaction[] transactions { get; set; }
    public MarketTradegood[] tradeGoods { get; set; }
}