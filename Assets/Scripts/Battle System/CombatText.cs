using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] float lifeTime = 1f;
    [SerializeField] float movespeed = 1f;
    [SerializeField] float textVibration = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeTime);
        transform.position += new Vector3(0f, movespeed * Time.deltaTime);
    }

    public void DamageTextAmount(int damageAmount)
    {
        damageText.text = damageAmount.ToString();
        float jitterAmount = Random.Range(-textVibration, textVibration);
        transform.position += new Vector3(jitterAmount, jitterAmount);
    }
}
