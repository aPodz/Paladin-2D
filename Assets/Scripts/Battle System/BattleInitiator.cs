using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInitiator : MonoBehaviour
{
    [SerializeField] BattleType[] battleToStart;
    [SerializeField] bool activateOnEntry;
    [SerializeField] bool canRunAway;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

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
                
            }
        }
    }

    private IEnumerator StartBattleCoroutine()
    {
        MenuManager.instance.FadeImage();
        AudioManager.instance.StopMusic();

        GameManager.instance.battleActive = true;       

        BattleManager.instance.itemsRewarded = battleToStart[0].rewardedItems;
        BattleManager.instance.XPRewardAmount = battleToStart[0].XPReward;

        yield return new WaitForSeconds(2);

        MenuManager.instance.FadeOut();
        AudioManager.instance.PlayBackgroundMusic(battleToStart[0].battleMusic);

        BattleManager.instance.StartBattle(battleToStart[0].enemies, canRunAway);
       
    }
}