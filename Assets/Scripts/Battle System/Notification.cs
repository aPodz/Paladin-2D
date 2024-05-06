using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    [SerializeField] float notificationUptime;
    [SerializeField] TextMeshProUGUI notificationText;

    
    public void Text(string text)
    {
        notificationText.text = text;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        StartCoroutine(NotifUptime());
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    IEnumerator NotifUptime()
    {       
        yield return new WaitForSeconds(notificationUptime);

        gameObject.SetActive(false);
    }

}
