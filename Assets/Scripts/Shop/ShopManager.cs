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
        shopMenu.SetActive(true);
        GameManager.instance.shopOpen = true;
        currentCoinText.text = "Coins: " + GameManager.instance.currentCoins;
        buyPanel.SetActive(true);
        UpdateShop(itemBuyParent, itemsForSale);
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
        GameManager.instance.shopOpen = false;
    }

    public void OpenBuyPanel()
    {
        buyPanel.SetActive(true);
        sellPanel.SetActive(false);
        UpdateShop(itemBuyParent, itemsForSale);
    }    
    public void OpenSellPanel()
    {
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

            if (item.amount > 1)
            {
                itemAmountText.text = "";
            }
            else
            {
                itemAmountText.text = "";
            }

            itemSlot.GetComponent<ItemButton>().itemOnButton = item;
        }
    }
}
