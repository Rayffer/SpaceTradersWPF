using SpaceTradersWPF.Types;

namespace SpaceTradersWPF.Extensions;

internal static class ShipRolesExtensions
{
    public static string ToApiString(this ShipRoles waypointTypes)
    {
        return waypointTypes switch
        {
            ShipRoles.Fabricator => "FABRICATOR",
            ShipRoles.Harvester => "HARVESTER",
            ShipRoles.Hauler => "HAULER",
            ShipRoles.Interceptor => "INTERCEPTOR",
            ShipRoles.Excavator => "EXCAVATOR",
            ShipRoles.Transport => "TRANSPORT",
            ShipRoles.Repair => "REPAIR",
            ShipRoles.Surveyor => "SURVEYOR",
            ShipRoles.Command => "COMMAND",
            ShipRoles.Carrier => "CARRIER",
            ShipRoles.Patrol => "PATROL",
            ShipRoles.Satellite => "SATELLITE",
            ShipRoles.Explorer => "EXPLORER",
            ShipRoles.Refinery => "REFINERY",
            _ => "INVALID_SHIP_ROLE"
        };
    }

    public static ShipRoles FromApiString(this string apiString)
    {
        return apiString switch
        {
            "FABRICATOR" => ShipRoles.Fabricator,
            "HARVESTER" => ShipRoles.Harvester,
            "HAULER" => ShipRoles.Hauler,
            "INTERCEPTOR" => ShipRoles.Interceptor,
            "EXCAVATOR" => ShipRoles.Excavator,
            "TRANSPORT" => ShipRoles.Transport,
            "REPAIR" => ShipRoles.Repair,
            "SURVEYOR" => ShipRoles.Surveyor,
            "COMMAND" => ShipRoles.Command,
            "CARRIER" => ShipRoles.Carrier,
            "PATROL" => ShipRoles.Patrol,
            "SATELLITE" => ShipRoles.Satellite,
            "EXPLORER" => ShipRoles.Explorer,
            "REFINERY" => ShipRoles.Refinery,
            _ => ShipRoles.NotDefined
        };
    }
}
