using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;
    public Button purchaseButton;

    private FishingItem currentItem;
    private ShopManager shopManager;

    public void Initialize(FishingItem item, ShopManager manager)
    {
        currentItem = item;
        shopManager = manager;

        iconImage.sprite = item.icon;
        nameText.text = item.itemName;
        descriptionText.text = item.description;
        priceText.text = item.price.ToString() + " Coins";
        purchaseButton.onClick.AddListener(OnPurchaseClicked);
    }

    private void OnPurchaseClicked()
    {
        shopManager.PurchaseItem(currentItem);
    }

    private void OnDestroy()
    {
        purchaseButton.onClick.RemoveListener(OnPurchaseClicked);
    }

}

