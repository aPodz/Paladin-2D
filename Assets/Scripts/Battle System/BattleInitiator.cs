using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInitiator : MonoBehaviour
{
    [SerializeField] BattleType[] battleToStart;
    [SerializeField] bool activateOnEntry;
    [SerializeField] bool canRunAway;
    [SerializeField] bool questObjective;
    public string questToComplete;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (activateOnEntry)
            {
                StartCoroutine(StartBattleCoroutine());
            }
            else
            {
                //Might be used later for some pre-battle dialog/effects
            }
        }
    }

    private IEnumerator StartBattleCoroutine() //Starts the battle that is set up in the inspector
    {
        MenuManager.instance.FadeImage();
        AudioManager.instance.StopMusic();

        GameManager.instance.battleActive = true;       

        BattleManager.instance.itemsRewarded = battleToStart[0].rewardedItems;
        BattleManager.instance.XPRewardAmount = battleToStart[0].XPReward;

        //BattleRewards.instance.completeQuest = questObjective; - will be used later
        //BattleRewards.instance.questToComplete = questToComplete; - will be used later

        yield return new WaitForSeconds(2);

        MenuManager.instance.FadeOut();
        AudioManager.instance.PlayBackgroundMusic(battleToStart[0].battleMusic);

        BattleManager.instance.StartBattle(battleToStart[0].enemies, canRunAway);
       
    }
}
