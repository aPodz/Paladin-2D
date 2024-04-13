using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestZone : MonoBehaviour
{
    [SerializeField] string questToMark;
    [SerializeField] bool markAsComplete;
    [SerializeField] bool markOnEnter;

    private bool canMark;

    public bool deactivateOnMark;

    private void Update()
    {
        if (canMark && Input.GetButtonDown("Fire1"))
        {
            canMark = false;
            MarkQuest();          
        }
    }

    public void MarkQuest()
    {
        if (markAsComplete)
        {
            QuestManager.instance.MarkQuestComplete(questToMark);
        }
        else
        {
            QuestManager.instance.MarkQuestIncomplete(questToMark);
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
                canMark = true;
            }
        }
    }
}
