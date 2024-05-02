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


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if  (!DialogController.instance.isDialogBoxActive() && !MenuManager.instance.menu.activeInHierarchy)
        {  
            if (autoActivate && canActivateDialog)
            {
                DialogController.instance.ActivateDialog(lines);
                autoActivate = false;
            }
            else if (!autoActivate && Input.GetButtonDown("Fire1") && canActivateDialog)
            {
                DialogController.instance.ActivateDialog(lines);
            }

            if (shouldActivateQuest)
            {
                DialogController.instance.ActivateQuest(questToMark, markAsComplete);
            }
        }
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
