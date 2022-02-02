using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InventoryItems/Item")]
public class InventoryItem : ScriptableObject
{
    public GlobalValues.EquippableCategory category;
    public ScriptableObject stats;
    public Sprite thumbnail;
    public string itemName;
    public string description;
    public float price;
    public int minLevel;                // The minimum level for the item to be unlocked
    public GameObject linkedObject;     // The prefab added to inventory when purchased
}
