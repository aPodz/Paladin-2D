using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    public string playerName;

    public Sprite characterImage;
    public int playerLevel = 1;
    public int currentXP;
    [SerializeField] int maxLevel = 21;
    public int[] xpPerLevel;
    [SerializeField] int baseLevelXP = 100;

    public int maxHP;
    public int currentHP;

    public int maxEnergy;
    public int currentEnergy;

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
}
