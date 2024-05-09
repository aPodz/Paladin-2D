using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public enum ItemType { Item, Weapon, Armor }
    public ItemType itemType;
    public enum EffectType { HP, Mana, Strength, AP }
    public EffectType effectType;

    public string itemName, itemDesc;
    public int itemValue;
    public Sprite itemImage;
    public int itemEffect;
    
    public int weaponAP, armorDef;

    public bool isStackable;
    public int amount;
    public int amountInInventory;

    public void UseItem(int characterToUse) //Uses item based on it's itemType (equipment or consumable)
    {
        PlayerStats selectedCharacter = GameManager.instance.GetPlayerStats()[characterToUse];

        AudioManager.instance.PlaySFX(2);
        if (itemType == ItemType.Item)
        {          
            if (effectType == EffectType.HP)
            {
                selectedCharacter.AddHP(itemEffect);
            }

            else if (effectType == EffectType.Mana)
            {
                selectedCharacter.AddMana(itemEffect);
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

    private void OnTriggerEnter2D(Collider2D collision) //Adds item to inventory if collected
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
