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
}
