using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class ManagerNotification : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public static void AddNotification(string title, string text, DateTime time)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.FireTime = time;

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
        // Para obtener detalles sobre otras propiedades que puede configurar, consulte AndroidNotification .
    }

}
