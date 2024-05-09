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
    public bool completeQuest;
    public string questToComplete;

    void Start()
    {
        instance = this;
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
        BattleManager.instance.battleScene.SetActive(false);
        BattleManager.instance.activeCharacters.Clear();
        BattleManager.instance.currentTurn = 0;

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

        if (completeQuest)
        {
            QuestManager.instance.MarkQuestComplete(questToComplete);
        }
    }
}
