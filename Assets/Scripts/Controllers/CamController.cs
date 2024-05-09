using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamController : MonoBehaviour
{
    private Player playerTarget;
    CinemachineVirtualCamera virtualCamera;

    void Start()
    {       
        playerTarget = FindObjectOfType<Player>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = playerTarget.transform;
    }

    void Update() //Looks for Player while it has nothing to follow
    {
         while (playerTarget == null)
        {
            playerTarget = FindObjectOfType<Player>();
            if (virtualCamera)
            {
                virtualCamera.Follow = playerTarget.transform;
            }
        }   
    }
}
