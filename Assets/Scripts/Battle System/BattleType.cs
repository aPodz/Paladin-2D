using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleType //Variables needed for battle setup in initiator
{
    public string[] enemies; //in case I want multiple enemies at some point
    public int XPReward;
    public ItemsManager[] rewardedItems;
    public int battleMusic;
}
