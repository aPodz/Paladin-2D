using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AreaExit : MonoBehaviour
{

    [SerializeField] string sceneToLoad;
    [SerializeField] string transitionArea;
    [SerializeField] bool instantTransition;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!instantTransition)
            {
                Player.instance.playerTransition = transitionArea;
                MenuManager.instance.FadeImage();
                StartCoroutine(LoadSceneCoroutine());
            }
            else
            {
                SceneManager.LoadScene(sceneToLoad);
            }
                     
        }
        
    }

    IEnumerator LoadSceneCoroutine()
    {        
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneToLoad);
    }
}
