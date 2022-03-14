using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Global : MonoBehaviour
{


    public const string keySession = "session";
    private static Location location_ug_center = new Location(-2.181452614962342, -79.89844529968079);




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
    }

    public static Location GetLocationUG()
    {
       return location_ug_center;
    }

    public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
    {
        double rlat1 = Math.PI * lat1 / 180;
        double rlat2 = Math.PI * lat2 / 180;
        double theta = lon1 - lon2;
        double rtheta = Math.PI * theta / 180;
        double dist =
            Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
            Math.Cos(rlat2) * Math.Cos(rtheta);
        dist = Math.Acos(dist);
        dist = dist * 180 / Math.PI;
        dist = dist * 60 * 1.1515;
        dist = dist * 1.609344;
        dist = dist * 1000;
        dist = Convert.ToInt32(dist);

        return dist;
    }

}

