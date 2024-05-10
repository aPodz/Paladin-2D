using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    [SerializeField] GameObject[] objectToActivate;
    [SerializeField] string questToCheck;
    [SerializeField] bool activateIfComplete;

    private void Update()
    {
        IsQuestCompleted();
    }

    public void IsQuestCompleted() // Activates/Deactivates game object if certain quest is completed
    {
        if (QuestManager.instance.CheckCompletion(questToCheck))
        {           
            for (int i = 0; i < objectToActivate.Length; i++)
            {
                objectToActivate[i].SetActive(activateIfComplete);
            }           
        }
    }
}
