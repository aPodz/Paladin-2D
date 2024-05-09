using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets instance;
    [SerializeField] ItemsManager[] itemsAvailable;

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public ItemsManager GetItemAsset(string itemToGet)
    {
        foreach (ItemsManager item in itemsAvailable)
        {
            if (item.itemName == itemToGet)
            {
                return item;
            }
        }
        return null;
    }

    public void ResetAmounts()
    {
        foreach(ItemsManager item in itemsAvailable)
        {
            item.amountInInventory = 0;
        }
    }
}
