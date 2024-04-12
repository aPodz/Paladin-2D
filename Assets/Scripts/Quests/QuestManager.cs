using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] string[] questNames;
    [SerializeField] bool[] questMarkers;
    // Start is called before the first frame update
    void Start()
    {
        questMarkers = new bool[questNames.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            print(CheckCompletion("Find a town"));
            MarkQuestComplete("Go to the blacksmith");
            MarkQuestIncomplete("Find a helper");
        }
    }

    public int GetQuestNumber(string questToFind)
    {
        for (int i = 0; i < questNames.Length; i++)
        {
            if (questNames[i] == questToFind)
            {
                return i;
            }

        }
        Debug.LogWarning("Quest: " + questToFind + "does not exist");
        return 0;
    }

    public bool CheckCompletion(string questToCheck)
    {
        int questNumberToCheck = GetQuestNumber(questToCheck);

        if (questNumberToCheck != 0)
        {
            return questMarkers[questNumberToCheck];
        }

        return false;
    }

    public void MarkQuestComplete(string questToMark)
    {
        int questNumberToCheck = GetQuestNumber(questToMark);
        questMarkers[questNumberToCheck] = true;
    }
    
    public void MarkQuestIncomplete(string questToMark)
    {
        int questNumberToCheck = GetQuestNumber(questToMark);
        questMarkers[questNumberToCheck] = false;
    }


}
