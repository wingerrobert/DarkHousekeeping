using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InventoryItems/Stats/Pike")]
public class PikeItemStats : ScriptableObject
{
    public float damage;
    public float range;
    public float attackSpeed;
    public float maxMassAttachable;
    public int maxItemsAttacked;
}