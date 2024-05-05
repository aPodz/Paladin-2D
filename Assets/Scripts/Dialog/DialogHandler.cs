using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor;
using UnityEngine;

public class DialogHandler : MonoBehaviour
{
    public string[] lines;
    private bool canActivateDialog;
    [SerializeField] bool shouldActivateQuest;
    [SerializeField] string questToMark;
    [SerializeField] bool markAsComplete;
    [SerializeField] bool autoActivate;
    [SerializeField] bool dialogFinished;
    public GameObject dialogTrigger;
    public static DialogHandler instance;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if  (!DialogController.instance.isDialogBoxActive() && !MenuManager.instance.menu.activeInHierarchy)
        {  
            if (autoActivate && canActivateDialog && !dialogFinished)
            {
                DialogController.instance.dialogJustStarted = false;
                DialogController.instance.ActivateDialog(lines);
                autoActivate = false;
                dialogFinished = true;
            }
            else if (!autoActivate && Input.GetKeyDown(KeyCode.E) && canActivateDialog && !dialogFinished)
            {
                DialogController.instance.dialogJustStarted = false;
                DialogController.instance.ActivateDialog(lines);
                dialogFinished = true;
            }

            if (shouldActivateQuest)
            {
                DialogController.instance.ActivateQuest(questToMark, markAsComplete);
            }
        }
    }

    public void DestroyTrigger()
    {
        Destroy(dialogTrigger);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canActivateDialog = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            canActivateDialog = false;
    }
}
