using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{

    [SerializeField] Image imageToFade;
    [SerializeField] GameObject menu;
    public static MenuManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        { 
            if (menu.activeInHierarchy)
            {
                menu.SetActive(false);
                GameManager.instance.gameMenuOpen = false;
            }
            else
            {
                menu.SetActive(true);
                GameManager.instance.gameMenuOpen = true;
            }
        }
    }

    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("Start Fade");
    }


}
