using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AreaExit : MonoBehaviour
{

    [SerializeField] string sceneToLoad;
    [SerializeField] string transitionArea;
    [SerializeField] bool instantTransition;
    
    private void OnTriggerEnter2D(Collider2D collision) //Makes sure Player enters scene at the correct entry        
    {
        if (!Player.instance.movementDisabled)
        {
            if (collision.CompareTag("Player"))
            {
                if (!instantTransition)
                {
                    Player.instance.playerTransition = transitionArea;                   
                    StartCoroutine(LoadSceneCoroutine());                   
                }
                else
                {
                    Player.instance.playerTransition = transitionArea;
                    SceneManager.LoadScene(sceneToLoad);                   
                }
            }
        }             
    }

    IEnumerator LoadSceneCoroutine()
    {
        MenuManager.instance.FadeImage();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneToLoad);
    }
}
