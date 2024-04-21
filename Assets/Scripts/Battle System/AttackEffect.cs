using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    [SerializeField] float effectTime;
    [SerializeField] int AttacksSFX;


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySFX(AttacksSFX);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, effectTime);
    }
}
