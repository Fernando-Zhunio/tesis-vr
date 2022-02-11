using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.ModernUIPack;
using System;
public class NotificationController : MonoBehaviour
{
    public Image background;
    public NotificationManager notificationManager;
    public Sprite success_icon;
    public Sprite info_icon;
    public Sprite warning_icon;
    public Sprite danger_icon;

    public static Hashtable hashtable_icons = new Hashtable();



    public void Start()
    {
        hashtable_icons.Add("success", success_icon);
        hashtable_icons.Add("info", info_icon);
        hashtable_icons.Add("warning", warning_icon);
        hashtable_icons.Add("danger", danger_icon);
    }
    
    public void setColorBackground(NotificationType notificationType)
    {
        Color32 _color = (Color32)Global.colors[notificationType.ToString()];
        //print(nameof(notificationType));
        background.color = _color;
    }

    public void showNotification(string title, string description,NotificationType notificationType)
    {
        notificationManager.title = title;
        notificationManager.description = description;
        //print(Enum.GetName( typeof(NotificationType),notificationType));
        //print(notificationType.ToString());
        notificationManager.icon = hashtable_icons[notificationType.ToString()] as Sprite;
        setColorBackground(notificationType);
        notificationManager.OpenNotification();
    }


    private static NotificationController instance = null;

    // Game Instance Singleton
    public static NotificationController Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}

public enum NotificationType
{
    success,info,warning,danger
}

