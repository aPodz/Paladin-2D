using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string newGameScene;
    [SerializeField] GameObject continueButton;

    void Start()
    {       
        if (PlayerPrefs.HasKey("Player_Pos_X")) //Checks for example of saved data
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    public void NewGameButton()
    {
        GameManager.instance.ResetPlayerStats();
        QuestManager.instance.ResetQuests();
        StartCoroutine(LoadSceneCoroutine());
        SceneManager.LoadScene(newGameScene);
        Player.instance.transform.position = new Vector3(-50, 40, 1);       
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game has been closed");        
    }

    public void ContinueButton()
    {
        GameManager.instance.gameMenuOpen = false;
        SceneManager.LoadScene("LoadingScene");
    }

    private IEnumerator LoadSceneCoroutine()
    {
        MenuManager.instance.FadeImage();
        yield return new WaitForSeconds(1);       
    }
}
