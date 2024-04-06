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
    private bool dialogJustStarted;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        dialogText.text = dialogLines[currentLine];
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBox.activeInHierarchy)
        {               
            if (Input.GetButtonUp("Fire1"))
            {   
                if (!dialogJustStarted)
                {
                    currentLine++;
                    if (currentLine >= dialogLines.Length)
                    {
                        dialogBox.SetActive(false);
                        GameManager.instance.dialogBoxOpen = false;
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
        dialogJustStarted = true;
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
}
