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
        bool itemAlreadyInInventory = false;

        if(item.isStackable)
        {
            foreach(ItemsManager itemInInventory in itemsList)
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

    public List<ItemsManager> GetItemList()
    {
        return itemsList;
    }
}
