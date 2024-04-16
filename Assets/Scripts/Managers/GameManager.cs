using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] PlayerStats[] playerStats;

    public bool gameMenuOpen, dialogBoxOpen, shopOpen;

    public int currentCoins;

    // Start is called before the first frame update
    void Start()
    {


        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        playerStats = FindObjectsOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SavePlayerData();
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            LoadPlayerData();
        }
        if (gameMenuOpen || dialogBoxOpen || shopOpen)
        {
            Player.instance.movementDisabled = true;
        }
        else
        {
            Player.instance.movementDisabled = false;
        }
    }

    public PlayerStats[] GetPlayerStats()
    {
        return playerStats;
    }

    public void SavePlayerData()
    {
        PlayerPrefs.SetFloat("Player_Pos_X", Player.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Pos_Y", Player.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Pos_Z", Player.instance.transform.position.z);

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_active", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_active", 0);
            }

            PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_Level", playerStats[i].playerLevel);
            PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_CurrentXP", playerStats[i].currentXP);

            PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_MaxHP", playerStats[i].maxHP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_CurrentHP", playerStats[i].currentHP);

            PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_MaxEnergy", playerStats[i].maxEnergy);
            PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_CurrentEnergy", playerStats[i].currentEnergy);

            PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_Armor", playerStats[i].armor);
            PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_Strength", playerStats[i].strength);
            PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_AttackPower", playerStats[i].attackPower);

            PlayerPrefs.SetString("Player_" + playerStats[i].gameObject.name + "_EquippedWeapon", playerStats[i].equippedWeaponName);
            PlayerPrefs.SetString("Player_" + playerStats[i].gameObject.name + "_EquippedArmor", playerStats[i].equippedArmorName);

            PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_WeaponAP", playerStats[i].weaponAP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_ArmorDefence", playerStats[i].armorDef);
        }
    }

    public void LoadPlayerData()
    {
        Player.instance.transform.position = new Vector3(
            PlayerPrefs.GetFloat("Player_Pos_X"),
            PlayerPrefs.GetFloat("Player_Pos_Y"),
            PlayerPrefs.GetFloat("Player_Pos_Z")
            );    

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_active") == 0)
            {
                playerStats[i].gameObject.SetActive(false);
            }
            else
            {
                playerStats[i].gameObject.SetActive(true);
            }

            playerStats[i].playerLevel = PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_Level");
            playerStats[i].currentXP = PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_CurrentXP");

            playerStats[i].maxHP = PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_MaxHP");
            playerStats[i].currentHP = PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_CurrentHP");

            playerStats[i].maxEnergy = PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_MaxEnergy");
            playerStats[i].currentEnergy = PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_CurrentEnergy");

            playerStats[i].armor = PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_Armor");
            playerStats[i].strength = PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_Strength");
            playerStats[i].attackPower = PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_AttackPower");

            playerStats[i].equippedWeaponName = PlayerPrefs.GetString("Player_" + playerStats[i].gameObject.name + "_EquippedWeapon");
            playerStats[i].equippedArmorName = PlayerPrefs.GetString("Player_" + playerStats[i].gameObject.name + "_EquippedArmor");

            playerStats[i].weaponAP = PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_WeaponAP");
            playerStats[i].armorDef = PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_ArmorDefence");


        }
    }
}
