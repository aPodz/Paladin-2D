using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.PlayBackgroundMusic(6);       
        MenuManager.instance.gameObject.SetActive(false);
        BattleManager.instance.gameObject.SetActive(false);
    }

    public void GoToMainMenu()
    {
        StartCoroutine(LoadSceneCoroutine());
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLastSave()
    {       
        SceneManager.LoadScene("LoadingScene");      
    }
    private IEnumerator LoadSceneCoroutine()
    {
        MenuManager.instance.FadeImage();
        yield return new WaitForSeconds(1);
    }


}
