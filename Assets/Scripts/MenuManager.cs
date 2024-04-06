using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.U2D;


public class MenuManager : MonoBehaviour
{

    [SerializeField] Image imageToFade;
    [SerializeField] GameObject menu;
    public static MenuManager instance;

    private PlayerStats[] playerStats;
    [SerializeField] TextMeshProUGUI[] nameText, hpText, epText, lvlText, xpText;
    [SerializeField] Slider[] xpSlider, epSlider, hpSlider;
    [SerializeField] Sprite[] characterImage;
    [SerializeField] GameObject[] characterPanel;

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
                UpdateStats();
                menu.SetActive(true);
                GameManager.instance.gameMenuOpen = true;
            }
        }
    }

    public void UpdateStats()
    {
        playerStats = GameManager.instance.GetPlayerStats();

        for (int i = 0; i < playerStats.Length; i++)
        {           
            characterPanel[i].SetActive(true);

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

    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("Start Fade");
    }


}
