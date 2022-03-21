using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ARLocation.MapboxRoutes;

public class VrManager2 : MonoBehaviour
{
    public TMP_Text txt_error;
    // public TMP_Text txt_info;
    // public MapboxRoute mapBoxRoute;

    // private void Start()
    // {
    //     Location location = Global.GetLocation();
    //     print(location.latitud +"-"+ location.longitud);
    //     RouteWaypoint waypointStart = new RouteWaypoint
    //     {
    //         Type = RouteWaypointType.UserLocation,
    //     };
    //     RouteWaypoint waypointEnd = new RouteWaypoint
    //     {
    //         Type = RouteWaypointType.Location,
    //         Location = new ARLocation.Location()
    //         {
    //             Latitude = location.latitudDouble,
    //             Longitude = location.longitudDouble,
    //             Altitude = 0,
    //             AltitudeMode = ARLocation.AltitudeMode.DeviceRelative
    //         }
    //     };

    //     StartCoroutine(mapBoxRoute.LoadRoute(waypointStart, waypointStart, getErrors));
    // }

    public void getErrors(string errors)
    {
        print("error---------"+errors);
        txt_error.text = errors;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Application.Quit();
            // UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("AR2");
            ControllerGlobalSingletons.Instance.DesactiveVr();
        }

    }


}
