using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class ManagerNotification : MonoBehaviour
{
    // Start is called before the first frame update
    // string idNotification = "fernando_zhunio";
    void Start()
    {
        GleyNotifications.Initialize(false);
    }

    public static void AddNotification(int id, string title, string text, DateTime time)
    {
        TimeSpan timeSpan = time - DateTime.Now.Date;
        GleyNotifications.SendNotification(title, text, timeSpan, "icon_small", "icon_large", id.ToString());
    }


}
