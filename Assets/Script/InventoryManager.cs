using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryManager : MonoBehaviour
{
    public List<FishingItem> ownedItems = new List<FishingItem>();

    void Start()
    {
        LoadInventory();
    }

    public void AddItemToInventory(FishingItem item)
    {
        ownedItems.Add(item);
        // Optionally, handle item-specific logic (e.g., equip a new rod)
        Debug.Log("Added to Inventory: " + item.itemName);
    }

    // Save inventory to PlayerPrefs or a file
    void SaveInventory()
    {
        // Implementation depends on how you want to save data
    }

    // Load inventory from PlayerPrefs or a file
    void LoadInventory()
    {
        // Implementation depends on how you want to load data
    }

    // Optional: Methods to remove items, use items, etc.
}

