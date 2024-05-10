using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogText, nameText;
    [SerializeField] GameObject dialogBox, nameBox;

    [SerializeField] string[] dialogLines;
    [SerializeField] int currentLine;

    public static DialogController instance;
    public bool dialogJustStarted;

    private string questToMark;
    private bool markQuestComplete;
    private bool shouldMarkQuest;

    void Start()
    {
        instance = this;
        dialogText.text = dialogLines[currentLine];
    }

    void Update()
    {
        if (dialogBox.activeInHierarchy)
        {          
            if (Input.GetButtonDown("Fire1"))
            {
                
                if (!dialogJustStarted)
                {
                    currentLine++;
                    if (currentLine >= dialogLines.Length)
                    {                       
                        dialogBox.SetActive(false);
                        GameManager.instance.dialogBoxOpen = false;                      

                        if(shouldMarkQuest)
                        {
                            shouldMarkQuest = false;
                            if (markQuestComplete)
                            {
                                QuestManager.instance.MarkQuestComplete(questToMark);
                            }
                            else
                            {
                                QuestManager.instance.MarkQuestIncomplete(questToMark);
                            }
                            
                        }
                    }
                    else
                    {
                        SetDialogName();
                        dialogText.text = dialogLines[currentLine];
                    }
                }
                else
                {                   
                    dialogJustStarted = false;
                }
            }
        }   
    }

    public void ActivateDialog(string[] newLines)
    {      
        dialogLines = newLines;
        currentLine = 0;

        SetDialogName();
        dialogText.text = dialogLines[currentLine];
        dialogBox.SetActive(true);        
        GameManager.instance.dialogBoxOpen = true;
    }

    public bool isDialogBoxActive()
    {
        return dialogBox.activeInHierarchy;
    }

    private void SetDialogName()
    {
        if (dialogLines[currentLine].StartsWith("#"))
        {
            nameText.text = dialogLines[currentLine].Replace("#", "");
            currentLine++;           
        }
    }

    public void ActivateQuest(string questName, bool markComplete)
    {
        questToMark = questName;
        markQuestComplete = markComplete;
        shouldMarkQuest = true;       
    }
}
