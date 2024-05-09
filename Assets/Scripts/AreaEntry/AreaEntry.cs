using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntry : MonoBehaviour
{
    public string transitionAreaName;

    void Start() 
    //Sets player position to the area entrance position
    {
        if (transitionAreaName == Player.instance.playerTransition) 
        {
            Player.instance.transform.position = transform.position;      
        }       
    }
}
