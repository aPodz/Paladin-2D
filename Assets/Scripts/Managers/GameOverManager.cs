using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayBackgroundMusic(6);       
        MenuManager.instance.gameObject.SetActive(false);
        BattleManager.instance.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMainMenu()
    {
        DestroySession();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLastSave()
    {
        GameManager.instance.LoadPlayerData();
        SceneManager.LoadScene("LoadingScene");
        Player.instance.movementDisabled = false;
    }

    public static void DestroySession()
    {
        Destroy(GameManager.instance.gameObject);       
        Destroy(MenuManager.instance.gameObject);
        Destroy(BattleManager.instance.gameObject);
    }
}
