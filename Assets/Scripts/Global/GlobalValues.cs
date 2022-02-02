using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValues : MonoBehaviour
{
    public static readonly int MAX_PARTICLE_SYSTEMS = 50;
    public static readonly int MAX_SUCTION_OBJECTS = 10;

    public enum ComputerUIScreen
    { 
        Computer,
        Store,
        TextFile
    }

    public enum CursorValue
    { 
        Active,
        Invalid,
        Idle
    };

    public enum Tags 
    {
        Player,
        Dust,
        Suction,
        SurfaceCleaner,
        WipableSurface,
        Door,
        PikeEnd,
        GarbageArea,
        Cursor,
        CursorController,
        ReservoirUI
    };

    public enum Layers
    {
        Enemy,
        Ground,
        Trash,
        GrimeDecal,
        Outlinable
    };

    public enum EquippableHoldType
    { 
        StandingVacuum,
        SmallHandHeldVacuum,
        Cloth,
        Pike
    }

    public enum EquippableHandedness
    { 
        Right,
        Left
    }

    public enum EquippableCategory
    { 
        Vacuum,
        Pike,
        Mop
    }

    public enum EquippableItem 
    {
        /* Vacuums */
        OlBetsy,
        Dusterator,

        /* Cloths */
        DustyRag,

        /* Pikes */
        BasicPike,

        /* Mops */
        Sweeper
    }

    public enum EnemyType
    { 
        JuicerGrub
    }

    public static Dictionary<EnemyType, string> EnemyAddressableNameMap = new Dictionary<EnemyType, string>()
    {
        { EnemyType.JuicerGrub, "Enemy_JuicerGrub" }
    };

    public static Dictionary<EquippableItem, string> EquippableAddressableNameMap = new Dictionary<EquippableItem, string>()
    {
        { EquippableItem.OlBetsy, "Vacuum_OlBetsy" },
        { EquippableItem.Dusterator, "Vacuum_Dusterator" },
        { EquippableItem.DustyRag, "Cloth_DustyRag" },
        { EquippableItem.BasicPike, "Pike_BasicPike" },
        { EquippableItem.Sweeper, "Mop_Sweeper" }
    };

    public static Dictionary<Tags, string> TagValues = new Dictionary<Tags, string>()
    {
        { Tags.Player, "Player" },
        { Tags.Dust, "Dust" },
        { Tags.Suction, "Suction" },
        { Tags.SurfaceCleaner, "SurfaceCleaner" },
        { Tags.WipableSurface, "WipableSurface" },
        { Tags.Door, "Door" },
        { Tags.PikeEnd, "PikeEnd" },
        { Tags.GarbageArea, "GarbageArea" },
        { Tags.Cursor, "Cursor" },
        { Tags.CursorController, "CursorController" },
        { Tags.ReservoirUI, "ReservoirUI" }
    };

    public static Dictionary<Layers, string> LayerValues = new Dictionary<Layers, string>()
    {
        { Layers.Enemy, "Enemy" },
        { Layers.Ground, "Ground" },
        { Layers.Trash, "Trash" },
        { Layers.GrimeDecal, "GrimeDecal" },
        { Layers.Outlinable, "Outlinable" }
    };

    public static Dictionary<EquippableCategory, string> CategoryNames = new Dictionary<EquippableCategory, string>()
    {
        { EquippableCategory.Mop, "Mops" },
        { EquippableCategory.Pike, "Pikes" },
        { EquippableCategory.Vacuum, "Vacuums" },
    };
}
