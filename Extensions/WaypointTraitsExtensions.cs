﻿using SpaceTradersWPF.Types;

namespace SpaceTradersWPF.Extensions;

internal static class WaypointTraitsExtensions
{
    public static string ToApiString(this WaypointTraits waypointTypes)
    {
        return waypointTypes switch
        {
            WaypointTraits.Uncharted => "UNCHARTED",
            WaypointTraits.Marketplace => "MARKETPLACE",
            WaypointTraits.Shipyard => "SHIPYARD",
            WaypointTraits.Outpost => "OUTPOST",
            WaypointTraits.ScatteredSettlements => "SCATTERED_SETTLEMENTS",
            WaypointTraits.SprawlingCities => "SPRAWLING_CITIES",
            WaypointTraits.MegaStructures => "MEGA_STRUCTURES",
            WaypointTraits.Overcrowded => "OVERCROWDED",
            WaypointTraits.HighTech => "HIGH_TECH",
            WaypointTraits.Corrupt => "CORRUPT",
            WaypointTraits.Bureaucratic => "BUREAUCRATIC",
            WaypointTraits.TradingHub => "TRADING_HUB",
            WaypointTraits.Industrial => "INDUSTRIAL",
            WaypointTraits.BlackMarket => "BLACK_MARKET",
            WaypointTraits.ResearchFacility => "RESEARCH_FACILITY",
            WaypointTraits.MilitaryBase => "MILITARY_BASE",
            WaypointTraits.SurveillanceOutpost => "SURVEILLANCE_OUTPOST",
            WaypointTraits.ExplorationOutpost => "EXPLORATION_OUTPOST",
            WaypointTraits.MineralDeposits => "MINERAL_DEPOSITS",
            WaypointTraits.CommonMetalDeposits => "COMMON_METAL_DEPOSITS",
            WaypointTraits.PreciousMetalDeposits => "PRECIOUS_METAL_DEPOSITS",
            WaypointTraits.RareMetalDeposits => "RARE_METAL_DEPOSITS",
            WaypointTraits.MethanePools => "METHANE_POOLS",
            WaypointTraits.IceCrystals => "ICE_CRYSTALS",
            WaypointTraits.ExplosiveGases => "EXPLOSIVE_GASES",
            WaypointTraits.StrongMagnetosphere => "STRONG_MAGNETOSPHERE",
            WaypointTraits.VibrantAuroras => "VIBRANT_AURORAS",
            WaypointTraits.SaltFlats => "SALT_FLATS",
            WaypointTraits.Canyons => "CANYONS",
            WaypointTraits.PerpetualDaylight => "PERPETUAL_DAYLIGHT",
            WaypointTraits.PerpetualOvercast => "PERPETUAL_OVERCAST",
            WaypointTraits.DrySeabeds => "DRY_SEABEDS",
            WaypointTraits.MagmaSeas => "MAGMA_SEAS",
            WaypointTraits.Supervolcanoes => "SUPERVOLCANOES",
            WaypointTraits.AshClouds => "ASH_CLOUDS",
            WaypointTraits.VastRuins => "VAST_RUINS",
            WaypointTraits.MutatedFlora => "MUTATED_FLORA",
            WaypointTraits.Terraformed => "TERRAFORMED",
            WaypointTraits.ExtremeTemperatures => "EXTREME_TEMPERATURES",
            WaypointTraits.ExtremePressure => "EXTREME_PRESSURE",
            WaypointTraits.DiverseLife => "DIVERSE_LIFE",
            WaypointTraits.ScarceLife => "SCARCE_LIFE",
            WaypointTraits.Fossils => "FOSSILS",
            WaypointTraits.WeakGravity => "WEAK_GRAVITY",
            WaypointTraits.StrongGravity => "STRONG_GRAVITY",
            WaypointTraits.CrushingGravity => "CRUSHING_GRAVITY",
            WaypointTraits.ToxicAtmosphere => "TOXIC_ATMOSPHERE",
            WaypointTraits.CorrosiveAtmosphere => "CORROSIVE_ATMOSPHERE",
            WaypointTraits.BreathableAtmosphere => "BREATHABLE_ATMOSPHERE",
            WaypointTraits.Jovian => "JOVIAN",
            WaypointTraits.Rocky => "ROCKY",
            WaypointTraits.Volcanic => "VOLCANIC",
            WaypointTraits.Frozen => "FROZEN",
            WaypointTraits.Swamp => "SWAMP",
            WaypointTraits.Barren => "BARREN",
            WaypointTraits.Temperate => "TEMPERATE",
            WaypointTraits.Jungle => "JUNGLE",
            WaypointTraits.Ocean => "OCEAN",
            WaypointTraits.Stripped => "STRIPPED",
            _ => "INVALID_WAYPOINT_TRAIT"
        };
    }

    public static WaypointTraits FromApiString(this string apiString)
    {
        return apiString switch
        {
            "UNCHARTED" => WaypointTraits.Uncharted,
            "MARKETPLACE" => WaypointTraits.Marketplace,
            "SHIPYARD" => WaypointTraits.Shipyard,
            "OUTPOST" => WaypointTraits.Outpost,
            "SCATTERED_SETTLEMENTS" => WaypointTraits.ScatteredSettlements,
            "SPRAWLING_CITIES" => WaypointTraits.SprawlingCities,
            "MEGA_STRUCTURES" => WaypointTraits.MegaStructures,
            "OVERCROWDED" => WaypointTraits.Overcrowded,
            "HIGH_TECH" => WaypointTraits.HighTech,
            "CORRUPT" => WaypointTraits.Corrupt,
            "BUREAUCRATIC" => WaypointTraits.Bureaucratic,
            "TRADING_HUB" => WaypointTraits.TradingHub,
            "INDUSTRIAL" => WaypointTraits.Industrial,
            "BLACK_MARKET" => WaypointTraits.BlackMarket,
            "RESEARCH_FACILITY" => WaypointTraits.ResearchFacility,
            "MILITARY_BASE" => WaypointTraits.MilitaryBase,
            "SURVEILLANCE_OUTPOST" => WaypointTraits.SurveillanceOutpost,
            "EXPLORATION_OUTPOST" => WaypointTraits.ExplorationOutpost,
            "MINERAL_DEPOSITS" => WaypointTraits.MineralDeposits,
            "COMMON_METAL_DEPOSITS" => WaypointTraits.CommonMetalDeposits,
            "PRECIOUS_METAL_DEPOSITS" => WaypointTraits.PreciousMetalDeposits,
            "RARE_METAL_DEPOSITS" => WaypointTraits.RareMetalDeposits,
            "METHANE_POOLS" => WaypointTraits.MethanePools,
            "ICE_CRYSTALS" => WaypointTraits.IceCrystals,
            "EXPLOSIVE_GASES" => WaypointTraits.ExplosiveGases,
            "STRONG_MAGNETOSPHERE" => WaypointTraits.StrongMagnetosphere,
            "VIBRANT_AURORAS" => WaypointTraits.VibrantAuroras,
            "SALT_FLATS" => WaypointTraits.SaltFlats,
            "CANYONS" => WaypointTraits.Canyons,
            "PERPETUAL_DAYLIGHT" => WaypointTraits.PerpetualDaylight,
            "PERPETUAL_OVERCAST" => WaypointTraits.PerpetualOvercast,
            "DRY_SEABEDS" => WaypointTraits.DrySeabeds,
            "MAGMA_SEAS" => WaypointTraits.MagmaSeas,
            "SUPERVOLCANOES" => WaypointTraits.Supervolcanoes,
            "ASH_CLOUDS" => WaypointTraits.AshClouds,
            "VAST_RUINS" => WaypointTraits.VastRuins,
            "MUTATED_FLORA" => WaypointTraits.MutatedFlora,
            "TERRAFORMED" => WaypointTraits.Terraformed,
            "EXTREME_TEMPERATURES" => WaypointTraits.ExtremeTemperatures,
            "EXTREME_PRESSURE" => WaypointTraits.ExtremePressure,
            "DIVERSE_LIFE" => WaypointTraits.DiverseLife,
            "SCARCE_LIFE" => WaypointTraits.ScarceLife,
            "FOSSILS" => WaypointTraits.Fossils,
            "WEAK_GRAVITY" => WaypointTraits.WeakGravity,
            "STRONG_GRAVITY" => WaypointTraits.StrongGravity,
            "CRUSHING_GRAVITY" => WaypointTraits.CrushingGravity,
            "TOXIC_ATMOSPHERE" => WaypointTraits.ToxicAtmosphere,
            "CORROSIVE_ATMOSPHERE" => WaypointTraits.CorrosiveAtmosphere,
            "BREATHABLE_ATMOSPHERE" => WaypointTraits.BreathableAtmosphere,
            "JOVIAN" => WaypointTraits.Jovian,
            "ROCKY" => WaypointTraits.Rocky,
            "VOLCANIC" => WaypointTraits.Volcanic,
            "FROZEN" => WaypointTraits.Frozen,
            "SWAMP" => WaypointTraits.Swamp,
            "BARREN" => WaypointTraits.Barren,
            "TEMPERATE" => WaypointTraits.Temperate,
            "JUNGLE" => WaypointTraits.Jungle,
            "OCEAN" => WaypointTraits.Ocean,
            "STRIPPED" => WaypointTraits.Stripped,
            _ => WaypointTraits.NotDefined
        };
    }
}
