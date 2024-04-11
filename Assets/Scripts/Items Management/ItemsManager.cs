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
    
    public int weaponAP, armorDef;

    public bool isStackable;
    public int amount;
    
    public void UseItem(int characterToUse)
    {
        PlayerStats selectedCharacter = GameManager.instance.GetPlayerStats()[characterToUse];


        if (itemType == ItemType.Item)
        {
            if (effectType == EffectType.HP)
            {
                selectedCharacter.AddHP(itemEffect);
            }

            else if (effectType == EffectType.Energy)
            {
                selectedCharacter.AddEnergy(itemEffect);
            }

            else
            {
                selectedCharacter.AddStr(itemEffect);
            }
        }

        else if (itemType == ItemType.Weapon)
        {
            if (selectedCharacter.equippedWeaponName != "")
            {
                Inventory.instance.AddItem(selectedCharacter.equippedWeapon);
            }

            selectedCharacter.EquipWeapon(this);
        }

        else if (itemType == ItemType.Armor)
        {
            if (selectedCharacter.equippedArmorName != "")
            {
                Inventory.instance.AddItem(selectedCharacter.equippedArmor);
            }

            selectedCharacter.EquipArmor(this);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {            
            Inventory.instance.AddItem(this);
            SelfDestruct();
            
        }
    }

    public void SelfDestruct()
    {
        gameObject.SetActive(false);
    }
}
