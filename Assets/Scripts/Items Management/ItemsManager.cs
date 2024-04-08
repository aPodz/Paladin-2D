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

    public bool isStackable;
    public int amount;
    
    public void UseItem()
    {
        if (itemType == ItemType.Item)
        {
            if (effectType == EffectType.HP)
            {
                PlayerStats.instance.AddHP(itemEffect);
            }

            else if (effectType == EffectType.Energy)
            {
                PlayerStats.instance.AddEnergy(itemEffect);
            }

            else
            {
                PlayerStats.instance.AddStr(itemEffect);
            }
        }
        
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
        gameObject.SetActive(false);
    }
}
