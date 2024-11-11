using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopItemButton : MonoBehaviour
{
    public FishingItem item; // Assign this in the Inspector
    [SerializeField] GameObject detailsPanel;
    [SerializeField] ItemDetailsUI itemDetailsUI;
    [SerializeField] ShopManager shopManager; 

    private void Start()
    {
        detailsPanel.SetActive(false);
        shopManager = GameObject.Find("ShopManager").GetComponent<ShopManager>();
        //itemDetailsUI = GetComponent<ItemDetailsUI>();
    }

    public void OnItemButtonClick()
    {
        detailsPanel.SetActive(true);
        itemDetailsUI.ShowItemDetails(item);
   
    }
    public void OnPurchaseButtonClick()
    {
        shopManager.BuyItem(item);
    }
}
