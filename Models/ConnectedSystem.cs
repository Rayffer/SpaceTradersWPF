namespace SpaceTradersWPF.Models;

internal class ConnectedSystem
{
    public string symbol { get; set; }
    public string sectorSymbol { get; set; }
    public string type { get; set; }
    public string factionSymbol { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public int distance { get; set; }
}