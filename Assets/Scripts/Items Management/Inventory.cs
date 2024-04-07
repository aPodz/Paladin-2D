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
        Debug.Log("New inventory was created");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ItemsManager item)
    {
        itemsList.Add(item);
    }
}
