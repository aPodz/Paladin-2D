using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] int musicToPlay;
    private bool musicPlaying;
    // Start is called before the first frame update
    void Start()
    {
        
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
