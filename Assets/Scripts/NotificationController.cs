using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.ModernUIPack;
using System;
[Serializable]
public class NotificationController : MonoBehaviour
{
    public Image background;
    public NotificationManager notificationManager;
    public Sprite success_icon;
    public Sprite info_icon;
    public Sprite warning_icon;
    public Sprite danger_icon;

    public GameObject prefab_notification;




    public static Hashtable hashtable_icons = new Hashtable();



    public void Start()
    {
        hashtable_icons.Add("success", success_icon);
        hashtable_icons.Add("info", info_icon);
        hashtable_icons.Add("warning", warning_icon);
        hashtable_icons.Add("danger", danger_icon);
    }


    // public void showNotification(string title, string description, NotificationType notificationType)
    // {
    // AndroidNativeFunctions.ShowAlert(title, description, "Aceptar", "Cancelar", "Cerrar", responseAlert);
    //PrefabNotification prefabNotification = prefab_notification.GetComponent<PrefabNotification>();
    //prefabNotification.setTitle(title);
    //prefabNotification.setDescription(description);
    //prefabNotification.setImage(hashtable_icons[notificationType.ToString()] as Sprite);

    //GameObject parentCanvas = GameObject.FindGameObjectWithTag("Canvas");
    //var clone = Instantiate(prefab_notification, parentCanvas.transform);
    //Destroy(clone, 4.0f);

    //notificationManager.title = title;
    //notificationManager.description = description;
    //notificationManager.icon = hashtable_icons[notificationType.ToString()] as Sprite;
    //setColorBackground(notificationType);
    //notificationManager.OpenNotification();
    // }

    public void responseAlert(DialogInterface dialogInterface)
    {
        print("responseAlert");
        string res = dialogInterface == DialogInterface.Positive ? "Aceptar" : "Cancelar";
#if DEVELOPMENT_BUILD
        AndroidNativeFunctions.ShowToast(res);
#endif
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


    public static void ShowToast(string message)
    {
#if DEVELOPMENT_BUILD
        AndroidNativeFunctions.ShowToast(message);
#endif 

    }

    public static void ShowToast(string message, bool shortDuration)
    {
#if DEVELOPMENT_BUILD
        AndroidNativeFunctions.ShowToast(message, shortDuration);
#endif 

    }

    public static void HideProgressDialog()
    {
#if DEVELOPMENT_BUILD
        AndroidNativeFunctions.HideProgressDialog();
#endif 

    }

    public static void ShowProgressDialog(string message, string title = "")
    {
#if DEVELOPMENT_BUILD
        AndroidNativeFunctions.ShowProgressDialog(message, title);
#endif 
    }


}

public enum NotificationType
{
    success, info, warning, danger
}

