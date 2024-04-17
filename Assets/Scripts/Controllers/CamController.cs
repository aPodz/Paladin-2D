using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamController : MonoBehaviour
{
    private Player playerTarget;
    CinemachineVirtualCamera virtualCamera;

    [SerializeField] int musicToPlay;
    private bool musicPlaying;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.StopMusic();
        playerTarget = FindObjectOfType<Player>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = playerTarget.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!musicPlaying)
        {
            musicPlaying = true;
            AudioManager.instance.PlayBackgroundMusic(musicToPlay);
        }
    }
}
