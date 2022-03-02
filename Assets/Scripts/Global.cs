using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Global : MonoBehaviour
{


    public const string keySession = "session";




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
            return "Bearer "+session.access_token;
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

    public static void SetMainCoordinate(string coordinate)
    {
        PlayerPrefs.SetString("coordinateMain", coordinate);
    }

    public static string GetMainCoordinate()
    {
        return PlayerPrefs.GetString("coordinateMain");
    }

}

