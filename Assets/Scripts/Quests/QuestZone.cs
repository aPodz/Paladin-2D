using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestZone : MonoBehaviour
{
    [SerializeField] string questToMark;
    [SerializeField] bool markAsComplete;
    [SerializeField] bool markOnEnter;
  
    public bool deactivateOnMark;

    public void MarkQuest() //Marks quest complete/incomplete when triggered
    {
        if (markAsComplete)
        {
            QuestManager.instance.MarkQuestComplete(questToMark);
        }
        else
        {
            QuestManager.instance.MarkQuestIncomplete(questToMark);
            QuestManager.instance.questText.text = questToMark;
        }

        gameObject.SetActive(!deactivateOnMark);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (markOnEnter)
            {
                MarkQuest();
            }
            else
            {
                
            }
        }
    }
}
