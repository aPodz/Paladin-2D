using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacters : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] string[] attacksAvailable;

    public string characterName;
    public int currentHP, maxHP, currentMana, maxMana, strength, armor, attackPower, weaponPower, armorDef;
    public bool isDead;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsCharacterAPlayer()
    {
        return isPlayer;
    }

    public string[] AttacksAvailable()
    { 
        return attacksAvailable;
    }

    public void TakeDamage(int damageReceived)
    {
        currentHP -= damageReceived;

        if (currentHP < 0)
        {
            currentHP = 0;
        }
    }

    public void UseItemInBattle(ItemsManager itemToUse)
    {
        if (itemToUse.itemType == ItemsManager.ItemType.Item)
        {
            if (itemToUse.effectType == ItemsManager.EffectType.HP)
            {
                AddHP(itemToUse.itemEffect);
            }
            else if (itemToUse.effectType == ItemsManager.EffectType.Mana)
            {
                AddMana(itemToUse.itemEffect);
            }
        }
    }

    private void AddMana(int itemEffect)
    {
        currentMana += itemEffect;
    }

    private void AddHP(int itemEffect)
    {
        currentHP += itemEffect;
    }
}
