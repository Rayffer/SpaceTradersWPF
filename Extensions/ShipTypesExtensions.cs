using SpaceTradersWPF.Types;

namespace SpaceTradersWPF.Extensions;

internal static class ShipTypesExtensions
{
    public static string ToApiString(this ShipTypes shipType)
    {
        return shipType switch
        {
            ShipTypes.ShipCommandFrigate => "SHIP_COMMAND_FRIGATE",
            ShipTypes.ShipExplorer => "SHIP_EXPLORER",
            ShipTypes.ShipHeavyFreighter => "SHIP_HEAVY_FREIGHTER",
            ShipTypes.ShipInterceptor => "SHIP_INTERCEPTOR",
            ShipTypes.ShipLightHauler => "SHIP_LIGHT_HAULER",
            ShipTypes.ShipLightShuttle => "SHIP_LIGHT_SHUTTLE",
            ShipTypes.ShipMiningDrone => "SHIP_MINING_DRONE",
            ShipTypes.ShipOreHound => "SHIP_ORE_HOUND",
            ShipTypes.ShipProbe => "SHIP_PROBE",
            ShipTypes.ShipRefiningFreighter => "SHIP_REFINING_FREIGHTER",
            _ => "SHIP_INVALID"
        };
    }

    public static ShipTypes ShipTypesFromApiString(this string apiString)
    {
        return apiString switch
        {
            "SHIP_COMMAND_FRIGATE" => ShipTypes.ShipCommandFrigate,
            "SHIP_EXPLORER" => ShipTypes.ShipExplorer,
            "SHIP_HEAVY_FREIGHTER" => ShipTypes.ShipHeavyFreighter,
            "SHIP_INTERCEPTOR" => ShipTypes.ShipInterceptor,
            "SHIP_LIGHT_HAULER" => ShipTypes.ShipLightHauler,
            "SHIP_LIGHT_SHUTTLE" => ShipTypes.ShipLightShuttle,
            "SHIP_MINING_DRONE" => ShipTypes.ShipMiningDrone,
            "SHIP_ORE_HOUND" => ShipTypes.ShipOreHound,
            "SHIP_PROBE" => ShipTypes.ShipProbe,
            "SHIP_REFINING_FREIGHTER" => ShipTypes.ShipRefiningFreighter,
            _ => ShipTypes.NotDefined
        };
    }
}