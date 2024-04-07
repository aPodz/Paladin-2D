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
        print(item.itemName + " has been added to inventory.");
        itemsList.Add(item);
        print(itemsList.Count);
    }

    public List<ItemsManager> GetItemList()
    {
        return itemsList;
    }
}
