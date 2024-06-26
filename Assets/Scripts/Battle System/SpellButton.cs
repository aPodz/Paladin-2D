using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour
{
    public string spellName;
    public int spellCost;

    public TextMeshProUGUI spellCostText;
    public Image spellIcon;

    public void PressButton()
    {
        if (BattleManager.instance.ActiveCharacters().currentMana >= spellCost)
        {
            BattleManager.instance.spellPanel.SetActive(false);
            BattleManager.instance.ActiveCharacters().currentMana -= spellCost;
            BattleManager.instance.PlayerAttack(spellName);
            
        }
        else
        {
            BattleManager.instance.battleNotification.Text("NOT ENOUGH MANA");
            BattleManager.instance.battleNotification.Activate();
            BattleManager.instance.spellPanel.SetActive(false);
        }

        
    }
}
