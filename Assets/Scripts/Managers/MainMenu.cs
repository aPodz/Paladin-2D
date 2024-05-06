using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] string newGameScene;
    [SerializeField] GameObject continueButton;
    // Start is called before the first frame update
    void Start()
    {       
        if (PlayerPrefs.HasKey("Player_Pos_X"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGameButton()
    {
        GameManager.instance.ResetPlayerStats();
        SceneManager.LoadScene(newGameScene);
        Player.instance.transform.position = new Vector3(-50, 40, 1);
    }

    public void ExitButton()
    {
        Debug.Log("Game has been closed");
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene("LoadingScene");
    }

}
