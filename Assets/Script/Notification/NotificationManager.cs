using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager 
{
 
  static  NotificationManager instance;


     Queue<Notification> listNotifications ;

    NotificationManager()
    {
        listNotifications = new Queue<Notification>();
    }

    public static NotificationManager GetInstance()
    {
        if(instance == null)
        {
            instance = new NotificationManager();
        }
        return  instance;
    }

    public  void AddNotification(Notification notification)
    {
        listNotifications.Enqueue(notification);
    } public Notification TakeNotification()
    {
        return listNotifications.Dequeue();
    }

    public int Count() {
       return  listNotifications.Count;
    }

}
