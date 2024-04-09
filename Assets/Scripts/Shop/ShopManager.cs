using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public GameObject shopMenu, buyPanel, sellPanel;

    [SerializeField] TextMeshProUGUI currentCoinText;


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
    }    
    public void OpenSellPanel()
    {
        buyPanel.SetActive(false);
        sellPanel.SetActive(true);
    }
}
