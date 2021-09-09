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
        Suction
    };

    public enum EquippableHoldType
    { 
        StandingVacuum,
        SmallHandHeldVacuum
    }

    public enum EquippableHandedness
    { 
        Right,
        Left
    }

    public enum EquippableType 
    {
        OlBetsy,
        Dusterator
    }

    public static Dictionary<EquippableType, string> EquippableResourceMap = new Dictionary<EquippableType, string>()
    {
        { EquippableType.OlBetsy, "Prefabs/Equippable/Vacuums/OlBetsy" },
        { EquippableType.Dusterator, "Prefabs/Equippable/Vacuums/Dusterator" },
    };

    public static Dictionary<Tags, string> TagValues = new Dictionary<Tags, string>() 
    {
        { Tags.Player, "Player" },
        { Tags.Dust, "Dust" },
        { Tags.Suction, "Suction" }
    };
}
