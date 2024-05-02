using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{

    [SerializeField] float effectTime;
    [SerializeField] int DeathSFX;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySFX(DeathSFX);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, effectTime);
    }
}
