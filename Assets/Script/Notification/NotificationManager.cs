using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NotificationManager 
{

    static Queue<Notification> listNotifications = new Queue<Notification>();

    public static void AddNotification(Notification notification)
    {
        listNotifications.Enqueue(notification);
    } public static Notification TakeNotification()
    {
        return listNotifications.Dequeue();
    }

    public static int Count() {
       return  listNotifications.Count;
    }

}
