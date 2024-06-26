using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;



public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerStats[] playerStats;

    public bool gameMenuOpen, dialogBoxOpen, shopOpen, battleActive;

    public int currentCoins;

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

        playerStats = FindObjectsOfType<PlayerStats>().OrderBy(z => z.transform.position.z).ToArray(); //Finds characters with assigned PlayerStats script and sorts them
    }

    void Update() //disables movement if any UI is active
    {
        if (gameMenuOpen || dialogBoxOpen || shopOpen || battleActive)
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
        SavePlayerPosition();
        SavePlayerStats();
        SavePlayerItems();
        QuestManager.instance.SaveQuestData();

        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);

        MenuManager.instance.notification.Text("Game saved");
        MenuManager.instance.notification.Activate();
    }
        public void LoadPlayerData()
    {
        LoadPlayerPosition();
        LoadPlayerStats();
        LoadPlayerItems();
        QuestManager.instance.LoadQuestData();
    }

    private static void SavePlayerItems()
    {
        PlayerPrefs.SetInt("Number_Of_Items", Inventory.instance.GetItemList().Count);
        for (int i = 0; i < Inventory.instance.GetItemList().Count; i++)
        {
            ItemsManager itemInInventory = Inventory.instance.GetItemList()[i];
            PlayerPrefs.SetString("Item_" + i + "_Name", itemInInventory.itemName);

            if (itemInInventory.isStackable)
            {
                Debug.Log(itemInInventory.amountInInventory);
                PlayerPrefs.SetInt("Item_" + i + "_Amount", itemInInventory.amountInInventory);
            }
        }
    }

    private static void LoadPlayerItems()
    {
        Inventory.instance.ClearInventory();
        ItemAssets.instance.ResetAmounts();
        for (int i = 0; i < PlayerPrefs.GetInt("Number_Of_Items"); i++)
        {
            string itemName = PlayerPrefs.GetString("Item_" + i + "_Name");
            ItemsManager itemToAdd = ItemAssets.instance.GetItemAsset(itemName);
            int itemAmount = 0;

            if (PlayerPrefs.HasKey("Item_" + i + "_Amount"))
            {
                itemAmount = PlayerPrefs.GetInt("Item_" + i + "_Amount");
            }

            Inventory.instance.AddItem(itemToAdd);
            if (itemToAdd.isStackable && itemAmount > 1)
            {
                itemToAdd.amountInInventory = itemAmount;
            }           
        }
    }

    private static void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat("Player_Pos_X", Player.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Pos_Y", Player.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Pos_Z", Player.instance.transform.position.z);
    }



    private static void LoadPlayerPosition()
    {
        Player.instance.transform.position = new Vector3(
                    PlayerPrefs.GetFloat("Player_Pos_X"),
                    PlayerPrefs.GetFloat("Player_Pos_Y"),
                    PlayerPrefs.GetFloat("Player_Pos_Z")
                    );
    }

    private void SavePlayerStats()
    {
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

            PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_MaxEnergy", playerStats[i].maxMana);
            PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_CurrentEnergy", playerStats[i].currentMana);

            PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_Armor", playerStats[i].armor);
            PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_Strength", playerStats[i].strength);
            PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_AttackPower", playerStats[i].attackPower);

            PlayerPrefs.SetString("Player_" + playerStats[i].gameObject.name + "_EquippedWeapon", playerStats[i].equippedWeaponName);
            PlayerPrefs.SetString("Player_" + playerStats[i].gameObject.name + "_EquippedArmor", playerStats[i].equippedArmorName);

            PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_WeaponAP", playerStats[i].weaponAP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].gameObject.name + "_ArmorDefence", playerStats[i].armorDef);
        }
    }



    private void LoadPlayerStats()
    {
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

            playerStats[i].maxMana = PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_MaxEnergy");
            playerStats[i].currentMana = PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_CurrentEnergy");

            playerStats[i].armor = PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_Armor");
            playerStats[i].strength = PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_Strength");
            playerStats[i].attackPower = PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_AttackPower");

            playerStats[i].equippedWeaponName = PlayerPrefs.GetString("Player_" + playerStats[i].gameObject.name + "_EquippedWeapon");
            playerStats[i].equippedArmorName = PlayerPrefs.GetString("Player_" + playerStats[i].gameObject.name + "_EquippedArmor");            

            playerStats[i].weaponAP = PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_WeaponAP");
            playerStats[i].armorDef = PlayerPrefs.GetInt("Player_" + playerStats[i].gameObject.name + "_ArmorDefence");

            playerStats[i].equippedWeapon = ItemAssets.instance.GetItemAsset(playerStats[i].equippedWeaponName);
            playerStats[i].equippedArmor = ItemAssets.instance.GetItemAsset(playerStats[i].equippedArmorName);
        }
    }

    public void ResetPlayerStats()
    {
        gameMenuOpen = false; //To enable movement when going from game to main menu then to game again

        for (int i = 0; i < playerStats.Length; i++)
        {
            playerStats[i].maxHP = playerStats[i].baseHP;
            playerStats[i].maxMana = playerStats[i].baseMana;
            playerStats[i].currentHP = playerStats[i].maxHP;
            playerStats[i].currentMana = playerStats[i].maxMana;
            playerStats[i].strength = playerStats[i].baseStrength;
            playerStats[i].playerLevel = 1;
            playerStats[i].currentXP = 0;
            playerStats[i].equippedWeapon = null;
            playerStats[i].equippedArmor = null;
            playerStats[i].equippedArmorName = "";
            playerStats[i].equippedWeaponName = "";
            playerStats[i].weaponAP = 0;
            playerStats[i].armorDef = 0;
        }
        Inventory.instance.ClearInventory();
        ItemAssets.instance.ResetAmounts();
    }
}
