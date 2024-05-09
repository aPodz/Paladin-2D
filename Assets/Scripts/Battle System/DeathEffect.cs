using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    //Not used for now
    [SerializeField] float effectTime;
    [SerializeField] int DeathSFX;
    void Start()
    {
        AudioManager.instance.PlaySFX(DeathSFX);
    }

    void Update()
    {
        Destroy(gameObject, effectTime);
    }
}
