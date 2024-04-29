using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleRewards : MonoBehaviour
{

    public static BattleRewards instance;

    [SerializeField] TextMeshProUGUI XPText;
    [SerializeField] GameObject rewardPanel;
    [SerializeField] ItemsManager[] rewardedItems;
    [SerializeField] int XPReward;
    [SerializeField] Image[] awardedItems;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OpenRewardPanel(XPReward, rewardedItems);
        }
    }

    public void OpenRewardPanel(int xp, ItemsManager[] items)
    {
        XPReward = xp;
        rewardedItems = items;

        XPText.text = xp + " XP";

        for (int i = 0; i < rewardedItems.Length; i++)
        {
            awardedItems[i].sprite = rewardedItems[i].itemImage;
        }

        rewardPanel.SetActive(true);
    }

    public void CloseRewardPanel()
    {
        foreach (PlayerStats activeplayer in GameManager.instance.GetPlayerStats())
        {
            if (activeplayer.gameObject.activeInHierarchy)
            {
                activeplayer.AddXP(XPReward);
            }
        }

        foreach (ItemsManager rewardedItem in rewardedItems)
        {
            Inventory.instance.AddItem(rewardedItem);
        }
        rewardPanel.SetActive(false);
        GameManager.instance.battleActive = false;
    }
}
