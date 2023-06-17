using SpaceTradersWPF.ApiModels;

namespace SpaceTradersWPF.Events.Models;

internal class ShipCargoTransferEventArguments
{
    public Ship ShipToTransferFrom { get; set; }
}
