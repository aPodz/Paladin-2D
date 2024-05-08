using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{

    private bool canOpenShop;

    [SerializeField] List<ItemsManager> shopItems;
    [SerializeField] int amountOfSoldItems;
    private bool recentlyOpened;
    // Start is called before the first frame update
    void Start()
    {
        recentlyOpened = false;
    }

    // Update is called once per frame
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


    public List<ItemsManager> GenerateShopkeeperItems(List<ItemsManager> items)
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
