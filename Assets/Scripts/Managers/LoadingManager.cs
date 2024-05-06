using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{

    [SerializeField] float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        if (waitTime > 0)
        {
            StartCoroutine(LoadScene());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));
        GameManager.instance.LoadPlayerData();
        QuestManager.instance.LoadQuestData();  
    }
}
