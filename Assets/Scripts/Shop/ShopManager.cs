using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public GameObject shopMenu, buyPanel, sellPanel;

    [SerializeField] TextMeshProUGUI currentCoinText;

    public List<ItemsManager> itemsForSale;

    [SerializeField] GameObject itemBox;
    [SerializeField] Transform itemBuyParent;
    [SerializeField] Transform itemSellParent;

    [SerializeField] ItemsManager selectedItem;
    [SerializeField] TextMeshProUGUI buyItemName, buyItemDesc, buyItemValue;
    [SerializeField] TextMeshProUGUI sellItemName, sellItemDesc, sellItemValue;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenShop()
    {
        currentCoinText.text = "Coins: " + GameManager.instance.currentCoins;
        UpdateShop(itemBuyParent, itemsForSale);
        shopMenu.SetActive(true);
        GameManager.instance.shopOpen = true;            
        buyPanel.SetActive(true);
        
    }

    public void CloseShop()
    {
        ResetActive();
        shopMenu.SetActive(false);
        GameManager.instance.shopOpen = false;
    }

    public void OpenBuyPanel()
    {
        ResetActive();
        buyPanel.SetActive(true);
        sellPanel.SetActive(false);
        UpdateShop(itemBuyParent, itemsForSale);
    }    
    public void OpenSellPanel()
    {
        ResetActive();
        buyPanel.SetActive(false);
        sellPanel.SetActive(true);
        UpdateShop(itemSellParent, Inventory.instance.GetItemList());
    }

    private void UpdateShop(Transform itemBoxParent, List<ItemsManager> processedItems)
    {
        foreach (Transform itemSlot in itemBoxParent)
        {
            Destroy(itemSlot.gameObject);
        }

        foreach (ItemsManager item in processedItems)
        {
            RectTransform itemSlot = Instantiate(itemBox, itemBoxParent).GetComponent<RectTransform>();

            Image itemImage = itemSlot.Find("ItemImage").GetComponent<Image>();
            itemImage.sprite = item.itemImage;

            TextMeshProUGUI itemAmountText = itemSlot.Find("Amount").GetComponent<TextMeshProUGUI>();

            if (item.amountInInventory > 1)
            {
                itemAmountText.text = item.amountInInventory.ToString();
            }
            else
            {
                itemAmountText.text = "";
            }

            itemSlot.GetComponent<ItemButton>().itemOnButton = item;
        }
    }

    public void ActiveBuyItem(ItemsManager itemToBuy)
    {
        selectedItem = itemToBuy;
        buyItemName.text = selectedItem.itemName;
        buyItemDesc.text = selectedItem.itemDesc;
        buyItemValue.text = "Cost: " + selectedItem.itemValue;
    }
    
    public void ActiveSellItem(ItemsManager itemToSell)
    {
        selectedItem = itemToSell;
        sellItemName.text = selectedItem.itemName;
        sellItemDesc.text = selectedItem.itemDesc;
        sellItemValue.text = "Cost: " + selectedItem.itemValue;
    }

    public void ResetActive()
    {
        selectedItem = null;
        buyItemName.text = "";
        buyItemDesc.text = "";
        buyItemValue.text = "";
        sellItemName.text = "";
        sellItemDesc.text = "";
        sellItemValue.text = "";

    }

    public void BuyItem()
    {
        if (GameManager.instance.currentCoins >= selectedItem.itemValue)
        {
            GameManager.instance.currentCoins -= selectedItem.itemValue;
            Inventory.instance.AddItem(selectedItem);
            itemsForSale.Remove(selectedItem);

            currentCoinText.text = "Coins: " + GameManager.instance.currentCoins; 
            ResetActive();                      
        }
        UpdateShop(itemBuyParent, itemsForSale);
        OpenBuyPanel();
    }

    public void SellItem()
    {
        if (selectedItem)
        {
            GameManager.instance.currentCoins += selectedItem.itemValue;
            Inventory.instance.RemoveItem(selectedItem);
            itemsForSale.Add(selectedItem);

            currentCoinText.text = "Coins: " + GameManager.instance.currentCoins;
            ResetActive();

            OpenSellPanel();
        }       
    }
}
