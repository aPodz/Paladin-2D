using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<ItemsManager> itemsList;
    public static Inventory instance;

    void Start()
    {
        instance = this;
        itemsList = new List<ItemsManager>();
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
                    itemInInventory.amountInInventory += item.amount;
                    itemAlreadyInInventory = true;
                }
            }

            if(!itemAlreadyInInventory) 
            { 
                itemsList.Add(item);
                item.amountInInventory++;
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
                    itemInInventory.amountInInventory--;
                    inventoryItem = itemInInventory;
                }
            }

            if (inventoryItem != null && inventoryItem.amountInInventory <= 0)
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
