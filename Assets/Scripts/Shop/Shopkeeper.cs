using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    private bool canOpenShop;

    [SerializeField] List<ItemsManager> shopItems;
    [SerializeField] int amountOfSoldItems;
    private bool recentlyOpened;

    void Start()
    {
        recentlyOpened = false; // Makes sure items available in shop don't get updated every time shop is opened
    }

    void Update()
    {
        if (canOpenShop && Input.GetKeyDown(KeyCode.E) && !Player.instance.movementDisabled && !ShopManager.instance.shopMenu.activeInHierarchy)
        {
            if (!recentlyOpened)
            {
                ShopManager.instance.itemsForSale = GenerateShopkeeperItems(shopItems);
                recentlyOpened = true;
            }            
            ShopManager.instance.OpenShop();             
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canOpenShop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canOpenShop = false;
        }
    }

    public List<ItemsManager> GenerateShopkeeperItems(List<ItemsManager> items) //Creates list of random shop items chosen from available items
    {
        List<ItemsManager> randomItems = new List<ItemsManager>();

        for (int i = 0; i < amountOfSoldItems; i++)
        {
            int randomItem = Random.Range(0, items.Count);
            randomItems.Add(items[randomItem]);
        }

        return randomItems;
    }

}
