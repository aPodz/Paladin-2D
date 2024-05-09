using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] float waitTime;

    void Start()
    {
        if (waitTime > 0)
        {
            StartCoroutine(LoadScene());
        }
    }

    public IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));
        GameManager.instance.LoadPlayerData();
        QuestManager.instance.LoadQuestData();  
    }
}
