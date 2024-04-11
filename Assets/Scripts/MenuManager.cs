using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.U2D;


public class MenuManager : MonoBehaviour
{

    [SerializeField] Image imageToFade;
    public GameObject menu;
    public static MenuManager instance;

    [SerializeField] GameObject[] statButtons;

    private PlayerStats[] playerStats;
    [SerializeField] TextMeshProUGUI[] nameText, hpText, epText, lvlText, xpText;
    [SerializeField] Slider[] xpSlider, epSlider, hpSlider;
    [SerializeField] Sprite[] characterImage;
    [SerializeField] GameObject[] characterPanel;

    [SerializeField] TextMeshProUGUI statName, statHP, statEP, statArm, statStr, statAP;
    [SerializeField] Image weaponImage, armorImage;
    [SerializeField] Image characterStatImage;

    [SerializeField] GameObject itemBox;
    [SerializeField] Transform itemBoxParent;

    public TextMeshProUGUI itemName, itemDesc;

    public ItemsManager activeItem;

    [SerializeField] GameObject characterChoicePanel;
    [SerializeField] TextMeshProUGUI[] itemCharacterChoiceName;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        { 
            if (menu.activeInHierarchy)
            {
                menu.SetActive(false);               
                GameManager.instance.gameMenuOpen = false;
            }
            else
            {
                if(!ShopManager.instance.shopMenu.activeInHierarchy)
                {
                    UpdateStats();
                    UpdateInventory();
                    menu.SetActive(true);
                    GameManager.instance.gameMenuOpen = true;
                }    
            }
        }
    }

    public void UpdateStats()
    {
        playerStats = GameManager.instance.GetPlayerStats();

        for (int i = 0; i < playerStats.Length; i++)
        {           
            characterPanel[i].SetActive(true);


            lvlText[i].text = playerStats[i].playerLevel.ToString();
            nameText[i].text = playerStats[i].playerName;
            hpText[i].text = playerStats[i].currentHP + "/" + playerStats[i].maxHP;
            epText[i].text = playerStats[i].currentEnergy + "/" + playerStats[i].maxEnergy;
            xpText[i].text = playerStats[i].currentXP + "/" + playerStats[i].playerLevel*100;

            xpSlider[i].maxValue = playerStats[i].playerLevel * 100;
            xpSlider[i].value = playerStats[i].currentXP;
            epSlider[i].maxValue = playerStats[i].maxEnergy;
            epSlider[i].value = playerStats[i].currentEnergy;
            hpSlider[i].maxValue = playerStats[i].maxHP;
            hpSlider[i].value = playerStats[i].currentHP;

            
        }
    }

    public void StatsMenu()
    {
        for (int i = 0; i < playerStats.Length; i++)
        {
            statButtons[i].SetActive(true);
        }

        StatsMenuUpdate(0);
    }

    public void StatsMenuUpdate(int playerNumber)
    {
        PlayerStats selectedPlayer = playerStats[playerNumber];
        

        statName.text = selectedPlayer.playerName;
        statHP.text = selectedPlayer.currentHP + "/" + selectedPlayer.maxHP;
        statEP.text = selectedPlayer.currentEnergy + "/" + selectedPlayer.maxEnergy;
        statStr.text = selectedPlayer.strength.ToString();
        statArm.text = selectedPlayer.armor.ToString() + "+" + selectedPlayer.armorDef.ToString();
        statAP.text = selectedPlayer.attackPower.ToString() + "+" + selectedPlayer.weaponAP.ToString();
        

        characterStatImage.sprite = selectedPlayer.characterImage;

        if (!selectedPlayer.equippedWeapon)
        {
            weaponImage.sprite = selectedPlayer.defaultWeapon;
        }
        else
        {
            weaponImage.sprite = selectedPlayer.equippedWeapon.itemImage;
        }

        if (!selectedPlayer.equippedArmor)
        {
            armorImage.sprite = selectedPlayer.defaultArmor;
        }
        else
        {
            armorImage.sprite = selectedPlayer.equippedArmor.itemImage;
        }    
    }

    public void UpdateInventory()
    {
        foreach(Transform itemSlot in itemBoxParent)
        {
            Destroy(itemSlot.gameObject);
        }
       
        foreach(ItemsManager item in Inventory.instance.GetItemList())
        {
            RectTransform itemSlot = Instantiate(itemBox, itemBoxParent).GetComponent<RectTransform>();

            Image itemImage = itemSlot.Find("ItemImage").GetComponent<Image>();
            itemImage.sprite = item.itemImage;

            TextMeshProUGUI itemAmountText = itemSlot.Find("Amount").GetComponent<TextMeshProUGUI>();

            if(item.amount > 1)
            {
                itemAmountText.text = item.amount.ToString(); 
            }
            else
            {
                itemAmountText.text = "";
            }

            itemSlot.GetComponent<ItemButton>().itemOnButton = item;
        }
    }

    public void DiscardItem()
    {
        Inventory.instance.RemoveItem(activeItem);
        UpdateInventory();
    }

    public void UseItem(int selectedCharacter)
    {
        activeItem.UseItem(selectedCharacter);
        OpenCharacterChoicePanel();
        DiscardItem();
        activeItem = null;
        itemName.text = null;
        itemDesc.text = null;
    }

    public void OpenCharacterChoicePanel()
    {
       
        if (activeItem)
        {
            characterChoicePanel.SetActive(true);
            for (int i = 0; i < playerStats.Length; i++)
            {
                PlayerStats activePlayer = GameManager.instance.GetPlayerStats()[i];
                itemCharacterChoiceName[i].text = activePlayer.playerName;

                bool playerAvailable = activePlayer.gameObject.activeInHierarchy;
                itemCharacterChoiceName[i].transform.parent.gameObject.SetActive(playerAvailable);
            }
        } 
    }

    public void CloseCharacterChoicePanel()
    {
        characterChoicePanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("We've quit the game.");
    }

    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("Start Fade");
    }


}
