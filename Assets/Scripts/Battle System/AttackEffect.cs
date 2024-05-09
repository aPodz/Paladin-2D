using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    [SerializeField] float effectTime;
    [SerializeField] int AttacksSFX;

    void Start()
    {
        AudioManager.instance.PlaySFX(AttacksSFX);
    }

    void Update()
    {
        Destroy(gameObject, effectTime);
    }
}
