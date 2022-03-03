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

    public static Location getLocation()
    {
       double latitud = PlayerPrefs.GetFloat("latitudVr");
       double longitud = PlayerPrefs.GetFloat("longitudVr");
       return new Location(latitud, longitud);
    }

    public static string getEventId()
    {
        return PlayerPrefs.GetString("EventId");
    }

    public static void setEventId(int id)
    {
         PlayerPrefs.SetString("EventId", id.ToString());
    }



    public static void setLocation(Location location)
    {
        PlayerPrefs.SetString("locationVR", location.latitud + "," + location.longitud);
        // PlayerPrefs.SetString("latitudVr", location.latitud);
        // PlayerPrefs.SetString("longitudVr", location.longitud);
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

    public static void SetEventMap(EventModel _event)
    {
        PlayerPrefs.SetString("EventMap", JsonUtility.ToJson(_event));
        // string _location = location.latitud + "," + location.longitud;
        // PlayerPrefs.SetString("locationMain", _location);
    }

    public static Location GetLocationEventMap(){
        if (PlayerPrefs.HasKey("EventMap"))
        {
            EventModel eventModel = JsonUtility.FromJson<EventModel>(PlayerPrefs.GetString("EventMap"));
            return new Location(eventModel.position[1], eventModel.position[0]);
        }
        return null;
    }

    public static EventModel GetEventMap()
    {
        if (PlayerPrefs.HasKey("EventMap"))
        {
            EventModel _event = JsonUtility.FromJson<EventModel>(PlayerPrefs.GetString("EventMap"));
            return _event;
        }
        return null;
        // return PlayerPrefs.GetString("locationMain");
    }

}

