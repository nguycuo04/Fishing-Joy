using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFishingItem", menuName = "Shop/Fishing Item")]
public class FishingItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;
    public int price;
    public FishingItemType itemType;

    // Specific attributes based on item type
    // For example, Rod-specific
    public int castingDistance;
    public int durability;

    // Bait-specific
    public float fishAttraction;

    // Boat-specific
    public int capacity;

    // Upgrade-specific
    public string upgradeEffect;
}

