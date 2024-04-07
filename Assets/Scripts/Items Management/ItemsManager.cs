using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public enum ItemType { Item, Weapon, Armor}
    public ItemType itemType;
    public enum EffectType { HP, Energy, Strength, AP }
    public EffectType effectType;

    public string itemName, itemDesc;
    public int itemValue;
    public Sprite itemImage;
    public int itemEffect;
    
    public int weaponAP, weaponStr, armorDef;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("This item is:" + itemName);
            Inventory.instance.AddItem(this);
            SelfDestruct();
            
        }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
