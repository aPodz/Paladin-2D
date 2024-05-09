using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    //Starts music chosen in inspector
    //script set to Main Camera
    [SerializeField] int musicToPlay;
    private bool musicPlaying;

    void Update()
    {
        if (!musicPlaying)
        {
            musicPlaying = true;
            AudioManager.instance.PlayBackgroundMusic(musicToPlay);
        }
    }
}
