using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<ItemsManager> itemsList;
    public static Inventory instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        itemsList = new List<ItemsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ItemsManager item)
    {      
        if (item.isStackable)
        {
            bool itemAlreadyInInventory = false;

            foreach (ItemsManager itemInInventory in itemsList)
            {
                if(itemInInventory.itemName == item.itemName)
                {
                    itemInInventory.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }

            if(!itemAlreadyInInventory) 
            { 
                itemsList.Add(item);
            }
        }
        else
        {
            itemsList.Add(item);
        }
        
    }

    public void RemoveItem(ItemsManager item)
    {      

        if (item.isStackable)
        {
            ItemsManager inventoryItem = null;

            foreach (ItemsManager itemInInventory in itemsList)
            {
                if (itemInInventory.itemName == item.itemName)
                {
                    itemInInventory.amount--;
                    inventoryItem = itemInInventory;
                }
            }

            if (inventoryItem != null && inventoryItem.amount <= 0)
            {
                GetItemList().Remove(inventoryItem);
            }
        }

        else
        {
            itemsList.Remove(item);
        }
    }

    public List<ItemsManager> GetItemList()
    {
        return itemsList;
    }
}
