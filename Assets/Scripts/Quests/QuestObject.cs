using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    [SerializeField] GameObject objectToActivate;
    [SerializeField] string questToCheck;
    [SerializeField] bool activateIfComplete;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            IsQuestCompleted();
        }
    }

    public void IsQuestCompleted()
    {
        if (QuestManager.instance.CheckCompletion(questToCheck))
        {
            objectToActivate.SetActive(activateIfComplete);
        }
    }


}
