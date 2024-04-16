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
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlaySFX(0);
        }
    }

    public void PlaySFX(int trackToPlay)
    {
        if (trackToPlay < SFX.Length)
        {
            SFX[trackToPlay].Play();
        }
    }
}
