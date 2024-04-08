using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public string playerName;

    public Sprite characterImage, defaultWeapon, defaultArmor;
    public int playerLevel = 1;
    public int currentXP;
    [SerializeField] int maxLevel = 21;
    public int[] xpPerLevel;
    [SerializeField] int baseLevelXP = 100;

    public int maxHP;
    public int currentHP;

    public int maxEnergy;
    public int currentEnergy;

    public int armor;
    public int strength;
    public int attackPower;

    public string equippedWeaponName;
    public string equippedArmorName;

    public int weaponAP;
    public int armorDef;

    public ItemsManager equippedWeapon, equippedArmor;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        xpPerLevel = new int[maxLevel];
        xpPerLevel[1] = baseLevelXP;

        for (int i = 2; i < xpPerLevel.Length; i++)
        {
            xpPerLevel[i] = baseLevelXP * i;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.L)) 
        {
            AddXP(100);
        }
    }

    public void AddXP(int addedXP)
    {
        currentXP += addedXP;
        if (currentXP >= xpPerLevel[playerLevel])
        {
            currentXP -= xpPerLevel[playerLevel];
            playerLevel++;

            if (playerLevel % 2 == 0)
            {
                maxHP += 20;
                strength += 2;
            }
            else
            {
                maxEnergy += 2;
            }

            currentHP = maxHP;
            currentEnergy = maxEnergy;

        }
    }

    public void AddHP(int addedHP)
    {
        currentHP += addedHP;
        
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void AddEnergy(int addedEP)
    {
        currentEnergy += addedEP;

        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
    }

    public void AddStr(int addedStr)
    {
        strength += addedStr;
    }

    public void EquipWeapon(ItemsManager weaponToEquip)
    {
        equippedWeapon = weaponToEquip;
        equippedWeaponName = equippedWeapon.itemName;
        weaponAP = equippedWeapon.weaponAP;

    }public void EquipArmor(ItemsManager armorToEquip)
    {
        equippedArmor = armorToEquip;
        equippedArmorName = equippedArmor.itemName;
        armorDef = equippedArmor.armorDef;
    }    
}
