using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntry : MonoBehaviour
{
    public string transitionAreaName;
    // Start is called before the first frame update
    void Start()
    {
        if (transitionAreaName == Player.instance.playerTransition) 
        {
            Player.instance.transform.position = transform.position;      
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
