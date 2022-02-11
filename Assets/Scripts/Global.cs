using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Global : MonoBehaviour
{

    //private static Global instance = null;


    public static Color32 color_success = new Color32(16,144,22, 255) ;
    public static Color32 color_info = new Color32(16, 95, 144, 255);
    public static Color32 color_warning = new Color32(179, 161, 13, 255);
    public static Color32 color_danger = new Color32(239, 39, 24, 255);

    public const string keySession = "session";

    public static Hashtable colors = new Hashtable()
    {
        { "success", color_success},
        { "info", color_info},
        { "warning", color_warning },
        { "danger", color_danger }

    };
    public static void ShowAndroidToastMessage(string title, string message, NotificationType notificationType)
    {
        //AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        //if (unityActivity != null)
        //{
        //    AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
        //    unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        //    {
        //        AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
        //        toastObject.Call("show");
        //    }));
        //}
        NotificationController notificationController = NotificationController.Instance;
        notificationController.showNotification(title, message, notificationType);
    }

    //public static Global Instance
    //{
    //    get
    //    {
    //        return instance;
    //    }
    //}

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        //if (instance != null && instance != this)
        //{
        //    Destroy(this.gameObject);
        //}

        //instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }

    
    public static Vector2 getLocation()
    {
       float latitud = PlayerPrefs.GetFloat("latitudVr");
       float longitud = PlayerPrefs.GetFloat("longitudVr");
       return new Vector2(latitud, longitud);
    }

    public static string getEventId()
    {
        return PlayerPrefs.GetString("EventId");
    }

    public static void setEventId(int id)
    {
         PlayerPrefs.SetString("EventId", id.ToString());
    }

    public static void setLocation(float latitud, float longitud)
    {
        PlayerPrefs.SetFloat("latitudVr",latitud);
        PlayerPrefs.SetFloat("longitudVr", longitud);
    }

    public static void SetSession(SessionModel sessionModel)
    {
        PlayerPrefs.SetString(keySession, JsonUtility.ToJson(sessionModel));
        ManagerScene.LoadSceneHome();
        Debug.Log("Game data saved!");
    }

    public static SessionModel getSession()
    {
        if (PlayerPrefs.HasKey(keySession))
        {
            SessionModel session = JsonUtility.FromJson<SessionModel>(PlayerPrefs.GetString(keySession));
            return session;
        }
        return null;
    }

    public static string getToken()
    {
        if (PlayerPrefs.HasKey(keySession))
        {
            SessionModel session = JsonUtility.FromJson<SessionModel>(PlayerPrefs.GetString(keySession));
            return "bearer "+session.access_token;
        }
        return null;
    }

    public static bool IsAuthenticated()
    {
        if (PlayerPrefs.HasKey(keySession))
        {
            return true;
        }
        return false;
    }

    public static void logout()
    {
        PlayerPrefs.DeleteAll();
        ManagerScene.LoadSceneAuth();
    }
}

