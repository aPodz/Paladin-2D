using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource[] SFX, backgroundMusic;
    public static AudioManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySFX(int trackToPlay)
    {
        if (trackToPlay < SFX.Length)
        {
            SFX[trackToPlay].Play();
        }
    }

    public void PlayBackgroundMusic(int trackToPlay)
    {
        StopMusic();

        if (trackToPlay < backgroundMusic.Length)
        {
            backgroundMusic[trackToPlay].Play();
        }
    }

    public void StopMusic()
    {
        foreach (AudioSource music in backgroundMusic)
        {
            music.Stop();
        }
    }
}
