using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{

    private bool canOpenShop;

    [SerializeField] List<ItemsManager> shopItems;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpenShop && Input.GetKeyDown(KeyCode.E) && !Player.instance.movementDisabled && !ShopManager.instance.shopMenu.activeInHierarchy)
        {
            ShopManager.instance.itemsForSale = shopItems;
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
}
