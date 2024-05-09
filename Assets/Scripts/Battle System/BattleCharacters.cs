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

    public Sprite deadSprite;   

    void Update() //Fades image of defeated enemy
    {
        if (!isPlayer && isDead)
        {
            EnemyFade();            
        }
    }

    private void EnemyFade() //Fades image until color.a is 0, then deactivates the object
    {
        GetComponent<SpriteRenderer>().color = new Color(
            Mathf.MoveTowards(GetComponent<SpriteRenderer>().color.r, 1f, 0.3f * Time.deltaTime),
            Mathf.MoveTowards(GetComponent<SpriteRenderer>().color.g, 0f, 0.3f * Time.deltaTime),
            Mathf.MoveTowards(GetComponent<SpriteRenderer>().color.b, 0f, 0.3f * Time.deltaTime),
            Mathf.MoveTowards(GetComponent<SpriteRenderer>().color.a, 0f, 0.3f * Time.deltaTime)
            );
        
        if (GetComponent<SpriteRenderer>().color.a == 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void KillEnemy()
    {       
        isDead = true;
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
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }

    }

    private void AddHP(int itemEffect)
    {
        currentHP += itemEffect;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void KillFriendlyCharacter()
    {
        if (deadSprite)
        {
            GetComponent<SpriteRenderer>().sprite = deadSprite;
            isDead = true;
        }
    }
}
