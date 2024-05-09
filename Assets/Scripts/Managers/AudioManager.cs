using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource[] SFX, backgroundMusic, battleMusic, battleSFX;
    public static AudioManager instance;

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

    public void PlaySFX(int trackToPlay)
    {
        if (trackToPlay < SFX.Length)
        {
            SFX[trackToPlay].Play();
        }
    }
    
    public void PlayBattleSFX(int trackToPlay)
    {
        if (trackToPlay < battleSFX.Length)
        {
            battleSFX[trackToPlay].Play();
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

    public void PlayBattleMusic(int trackToPlay)
    {
        if (trackToPlay < battleMusic.Length)
        {
            battleMusic[trackToPlay].Play();
        }
    }

    public void StopMusic()
    {
        foreach (AudioSource music in backgroundMusic)
        {
            music.Stop();
        }
        foreach (AudioSource music in battleMusic)
        {
            music.Stop();
        }

    }
}
