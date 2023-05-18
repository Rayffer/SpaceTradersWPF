using SpaceTradersWPF.Types;

namespace SpaceTradersWPF.Extensions;

internal static class WaypointTypesExtensions
{
    public static string ToApiString(this WaypointTypes waypointTypes)
    {
        return waypointTypes switch
        {
            WaypointTypes.Planet => "PLANET",
            WaypointTypes.GasGiant => "GAS_GIANT",
            WaypointTypes.Moon => "MOON",
            WaypointTypes.OrbitalStation => "ORBITAL_STATION",
            WaypointTypes.JumpGate => "JUMP_GATE",
            WaypointTypes.AsteroidField => "ASTEROID_FIELD",
            WaypointTypes.Nebula => "NEBULA",
            WaypointTypes.DebrisField => "DEBRIS_FIELD",
            WaypointTypes.GravityWell => "GRAVITY_WELL",
            _ => "INVALID_PLANET_TYPE"
        };
    }

    public static WaypointTypes FromApiString(this string apiString)
    {
        return apiString switch
        {
            "PLANET" => WaypointTypes.Planet,
            "GAS_GIANT" => WaypointTypes.GasGiant,
            "MOON" => WaypointTypes.Moon,
            "ORBITAL_STATION" => WaypointTypes.OrbitalStation,
            "JUMP_GATE" => WaypointTypes.JumpGate,
            "ASTEROID_FIELD" => WaypointTypes.AsteroidField,
            "NEBULA" => WaypointTypes.Nebula,
            "DEBRIS_FIELD" => WaypointTypes.DebrisField,
            "GRAVITY_WELL" => WaypointTypes.GravityWell,
            _ => WaypointTypes.NotDefined
        };
    }
}
