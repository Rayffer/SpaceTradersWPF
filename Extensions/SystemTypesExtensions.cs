using SpaceTradersWPF.Types;

namespace SpaceTradersWPF.Extensions;

internal static class SystemTypesExtensions
{
    public static string ToApiString(this SystemTypes waypointTypes)
    {
        return waypointTypes switch
        {
            SystemTypes.NeutronStar => "NEUTRON_STAR",
            SystemTypes.RedStar => "RED_STAR",
            SystemTypes.OrangeStar => "ORANGE_STAR",
            SystemTypes.BlueStar => "BLUE_STAR",
            SystemTypes.YoungStar => "YOUNG_STAR",
            SystemTypes.WhiteDwarf => "WHITE_DWARF",
            SystemTypes.BlackHole => "BLACK_HOLE",
            SystemTypes.Hypergiant => "HYPERGIANT",
            SystemTypes.Nebula => "NEBULA",
            SystemTypes.Unstable => "UNSTABLE",
            _ => "INVALID_SYSTEM_TYPE"
        };
    }

    public static SystemTypes FromApiString(this string apiString)
    {
        return apiString switch
        {
            "NEUTRON_STAR" => SystemTypes.NeutronStar,
            "RED_STAR" => SystemTypes.RedStar,
            "ORANGE_STAR" => SystemTypes.OrangeStar,
            "BLUE_STAR" => SystemTypes.BlueStar,
            "YOUNG_STAR" => SystemTypes.YoungStar,
            "WHITE_DWARF" => SystemTypes.WhiteDwarf,
            "BLACK_HOLE" => SystemTypes.BlackHole,
            "HYPERGIANT" => SystemTypes.Hypergiant,
            "NEBULA" => SystemTypes.Nebula,
            "UNSTABLE" => SystemTypes.Unstable,
            _ => SystemTypes.NotDefined
        };
    }
}
