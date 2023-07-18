using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExceptionHandling : MonoBehaviour
{

   


    GameObject notificationUI ;
    TextMeshProUGUI message;
    TextMeshProUGUI title;

    NotificationManager notificationManager = NotificationManager.GetInstance();

     void Awake()
    {
        notificationUI = GameObject.Find("Notification");
        message = GameObject.Find("NotificationMessage").GetComponent<TextMeshProUGUI>();
        title = GameObject.Find("NotificationTitle").GetComponent<TextMeshProUGUI>();

    }

    // Start is called before the first frame update
    void Start()
    {
        notificationUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Camera.main.GetComponent
        if (!notificationUI.activeSelf && notificationManager.Count()>0)
        {
            ShowNotification();

        }

      
        
        //message.text = "abc" + notificationUI;

    }


    public void ShowNotification()
    {
        //message = GameObject.Find("NotificationMessage").GetComponent<TextMeshProUGUI>();
        //title = GameObject.Find("NotificationTitle").GetComponent<TextMeshProUGUI>();
        Notification notification = notificationManager.TakeNotification();

        title.text = notification.Title;
        message.text = notification.Message;
        notificationUI.SetActive(true);
    }
}

