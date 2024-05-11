using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour // Work in progress, quests are not used in game yet
{
    [SerializeField] string[] questNames;
    [SerializeField] bool[] questMarkers;

    public static QuestManager instance;
    public GameObject questPanel;
    public TextMeshProUGUI questText;
    void Start()
    {
        instance = this;
        questMarkers = new bool[questNames.Length];       
    }

    public void ShowActiveQuest()
    {
        for (int i = 0; i < questNames.Length; i++)
        {
            if (questMarkers[i])
            {
                questText.text = questNames[i]; 
            }
        }
    }

    public void ResetQuests()
    {
        for (int i = 0; i < questNames.Length; ++i)
        {
            MarkQuestIncomplete(questNames[i]);
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

    public void UpdateQuestObjects()
    {
        QuestObject[] questObjects = FindObjectsOfType<QuestObject>();

        if (questObjects.Length > 0)
        {
            foreach (QuestObject questObject in questObjects)
            {
                questObject.IsQuestCompleted();           
            }
        }
    }

    public void MarkQuestComplete(string questToMark)
    {
        int questNumberToCheck = GetQuestNumber(questToMark);
        questMarkers[questNumberToCheck] = true;
        UpdateQuestObjects();
    }
    
    public void MarkQuestIncomplete(string questToMark)
    {
        int questNumberToCheck = GetQuestNumber(questToMark);
        questMarkers[questNumberToCheck] = false;

        UpdateQuestObjects();
    }

    public void SaveQuestData()
    {
        for (int i = 0; i < questNames.Length; i++)
        {
            if (questMarkers[i])
            {
                PlayerPrefs.SetInt("QuestMarker_" + questNames[i], 1);
            }
            else
            {
                PlayerPrefs.SetInt("QuestMarker_" + questNames[i], 0);
            }
            
        }
    }

    public void LoadQuestData()
    {
        for (int i = 0; i < questNames.Length; i++)
        {
            int valueToSet = 0;
            string keyToUse = "QuestMarker_" + questNames[i];

            if (PlayerPrefs.HasKey(keyToUse))
            {
                valueToSet = PlayerPrefs.GetInt(keyToUse);
            }

            if (valueToSet == 0)
            {
                questMarkers[i] = false;
                
            }
            else
            {
                questMarkers[i] = true;
            }
        }
    }


}
