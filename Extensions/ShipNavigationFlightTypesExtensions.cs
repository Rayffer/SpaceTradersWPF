using SpaceTradersWPF.Types;

namespace SpaceTradersWPF.Extensions;

internal static class ShipNavigationFlightTypesExtensions
{
    public static string ToApiString(this ShipNavigationStatus waypointTypes)
    {
        return waypointTypes switch
        {
            ShipNavigationStatus.InTransit => "IN_TRANSIT",
            ShipNavigationStatus.Docked => "DOCKED",
            ShipNavigationStatus.InOrbit => "IN_ORBIT",
            _ => "INVALID_SHIP_NAVIGATION_STATUS"
        };
    }

    public static ShipNavigationStatus FromApiString(this string apiString)
    {
        return apiString switch
        {
            "IN_TRANSIT" => ShipNavigationStatus.InTransit,
            "DOCKED" => ShipNavigationStatus.Docked,
            "IN_ORBIT" => ShipNavigationStatus.InOrbit,
            _ => ShipNavigationStatus.NotDefined
        };
    }
}
