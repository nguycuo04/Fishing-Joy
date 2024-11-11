using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsUI : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemPrice;
    public Button purchaseButton;

    private FishingItem currentItem; // The item currently being displayed
    private ShopManager shopManager;

    void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();
        //purchaseButton.onClick.AddListener(OnPurchaseButtonClick);
    }

    // Function to show item details
    public void ShowItemDetails(FishingItem item)
    {
        currentItem = item;
        itemImage.sprite = item.icon;
        itemName.text = item.name;
        itemDescription.text = item.description;
        itemPrice.text = "Price: " + item.price.ToString();

        //gameObject.SetActive(true); // Show the panel
    }

    // Handle the purchase button click
    private void OnPurchaseButtonClick()
    {
        if (shopManager != null && currentItem != null)
        {
            shopManager.BuyItem(currentItem);
            //gameObject.SetActive(false); // Hide the panel after purchase
        }
    }

    // Function to hide the panel
    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
}

