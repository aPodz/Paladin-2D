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
        if (BattleManager.instance.battleEnding)
        {
            BattleEnd();
        }
        
        if (!musicPlaying)
        {
            musicPlaying = true;
            AudioManager.instance.PlayBackgroundMusic(musicToPlay);
        }

    }
    public void BattleEnd() //resets music back to background music after battle ends
    {
        BattleManager.instance.battleEnding = false;
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayBackgroundMusic(musicToPlay);       
    }
}
