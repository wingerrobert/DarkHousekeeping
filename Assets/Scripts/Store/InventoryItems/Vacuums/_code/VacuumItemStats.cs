using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InventoryItems/Stats/Vacuum")]
public class VacuumItemStats : ScriptableObject
{
    public float suctionStrength;
    public float suctionSize;
    public float reservoirSize;
    public float shootDelay;
    public float shootForce;
}
