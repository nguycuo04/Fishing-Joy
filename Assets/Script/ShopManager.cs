using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI currencyText;
    public Transform itemsParent; // Content of the ScrollView
    public GameObject shopItemPrefab;

    [Header("Categories")]
    public List<FishingItem> rodItems;
    public List<FishingItem> baitItems;
    public List<FishingItem> accessoryItems;
    public List<FishingItem> boatItems;
    public List<FishingItem> upgradeItems;

    private List<FishingItem> currentCategoryItems = new List<FishingItem>();

    private int playerCurrency;

    [Header("Inventory")]
    public InventoryManager inventoryManager;

    void Start()
    {
        LoadPlayerData();
        ShowCategory(FishingItemType.Rod); // Default category
    }

    private void Update()
    {
        UpdateCurrencyDisplay();
        SavePlayerData();
    }

    public void BuyItem(FishingItem item)
    {
        if (playerCurrency >= item.price)
        {
            playerCurrency -= item.price;
            // Implement logic to add the item to the player’s inventory
            Debug.Log("Purchased " + item.name);
        }
        else
        {
            Debug.Log("Not enough currency for " + item.name);
        }
    }

    // Load player currency and inventory from saved data
    void LoadPlayerData()
    {
        playerCurrency = PlayerPrefs.GetInt("PlayerCurrency", 1000); // Default to 1000
        // Load inventory if implemented
    }

    // Save player currency and inventory
    void SavePlayerData()
    {
        PlayerPrefs.SetInt("PlayerCurrency", playerCurrency);
        // Save inventory if implemented
    }

    public void ShowCategory(FishingItemType category)
    {
        ClearCurrentItems();
        switch (category)
        {
            case FishingItemType.Rod:
                currentCategoryItems = rodItems;
                break;
            case FishingItemType.Bait:
                currentCategoryItems = baitItems;
                break;
            case FishingItemType.Accessory:
                currentCategoryItems = accessoryItems;
                break;
            case FishingItemType.Boat:
                currentCategoryItems = boatItems;
                break;
            case FishingItemType.Upgrade:
                currentCategoryItems = upgradeItems;
                break;
        }

        PopulateItems();
    }

    void ClearCurrentItems()
    {
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }
    }

    void PopulateItems()
    {
        foreach (FishingItem item in currentCategoryItems)
        {
            GameObject newItem = Instantiate(shopItemPrefab, itemsParent);
            ItemDetailsUI itemUI = newItem.GetComponent<ItemDetailsUI>();
            itemUI.ShowItemDetails(item);
        }
    }

    public void PurchaseItem(FishingItem item)
    {
        if (playerCurrency >= item.price)
        {
            playerCurrency -= item.price;
            UpdateCurrencyDisplay();
            inventoryManager.AddItemToInventory(item);
            SavePlayerData();
            // Optional: Provide feedback to the player (e.g., sound, animation)
        }
        else
        {
            // Optional: Notify the player about insufficient funds
            Debug.Log("Not enough currency to purchase " + item.itemName);
        }
    }

    void UpdateCurrencyDisplay()
    {
        currencyText.text = "Coins: " + playerCurrency.ToString();
    }

    // Optional: Methods to add currency, handle special purchases, etc.
}

