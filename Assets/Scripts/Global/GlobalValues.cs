using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValues : MonoBehaviour
{
    public static readonly int MAX_PARTICLE_SYSTEMS = 50;
    public static readonly int MAX_SUCTION_OBJECTS = 10;

    public enum Tags 
    {
        Player,
        Dust,
        Suction,
        SurfaceCleaner
    };

    public enum EquippableHoldType
    { 
        StandingVacuum,
        SmallHandHeldVacuum,
        Cloth
    }

    public enum EquippableHandedness
    { 
        Right,
        Left
    }

    public enum EquippableType 
    {
        OlBetsy,
        Dusterator,
        DustyRag
    }

    public static Dictionary<EquippableType, string> EquippableAddressableNameMap = new Dictionary<EquippableType, string>()
    {
        { EquippableType.OlBetsy, "Vacuum_OlBetsy" },
        { EquippableType.Dusterator, "Vacuum_Dusterator" },
        { EquippableType.DustyRag, "Cloth_DustyRag" }
    };

    public static Dictionary<Tags, string> TagValues = new Dictionary<Tags, string>() 
    {
        { Tags.Player, "Player" },
        { Tags.Dust, "Dust" },
        { Tags.Suction, "Suction" },
        { Tags.SurfaceCleaner, "SurfaceCleaner" }
    };
}
