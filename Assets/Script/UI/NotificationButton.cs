using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NotificationButton : MonoBehaviour
{
    void Start()
    {
        
    }
    public void HandleCloseNotify()
    {
        GameObject.Find("NotificationMessage").GetComponent<TextMeshProUGUI>().text = string.Empty;
        GameObject.Find("NotificationTitle").GetComponent<TextMeshProUGUI>().text = string.Empty;
        GameObject.Find("Notification").SetActive(false);
        
    }
}
