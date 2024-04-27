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
        Player.instance.gameObject.SetActive(false);        
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
        DestroySession();
        SceneManager.LoadScene("LoadingScene");
    }

    public static void DestroySession()
    {
        Destroy(GameManager.instance.gameObject);
        Destroy(Player.instance.gameObject);       
        Destroy(BattleManager.instance.gameObject);
        
    }
}
