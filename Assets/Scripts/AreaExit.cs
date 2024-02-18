using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{

    [SerializeField] string sceneToLoad;
    [SerializeField] string transitionArea;
    [SerializeField] AreaEntry areaEntry;
    // Start is called before the first frame update
    void Start()
    {
        areaEntry.transitionAreaName = transitionArea;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.instance.playerTransition = transitionArea;
            SceneManager.LoadScene(sceneToLoad);
        }
        
    }
}
