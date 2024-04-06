using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] string playerName;

    [SerializeField] int playerLevel = 1;
    [SerializeField] int currentXP;
    [SerializeField] int maxLevel = 21;
    [SerializeField] int[] xpPerLevel;
    [SerializeField] int baseLevelXP = 100;

    [SerializeField] int maxHP;
    [SerializeField] int currentHP;

    [SerializeField] int maxEnergy;
    [SerializeField] int currentEnergy;

    [SerializeField] int armor;
    [SerializeField] int strength;
    // Start is called before the first frame update
    void Start()
    {
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
        if (currentXP > xpPerLevel[playerLevel])
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
}
