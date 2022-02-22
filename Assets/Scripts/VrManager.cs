using ARLocation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using ARLocation;

//[RequireComponent(typeof(PlaceAtLocation))]
public class VrManager : Conexion
{
    //PlaceAtLocation placeAtLoacation;
    public LocationPath locationPath;
   

    void Start()
    {
        Vector2 location = Global.getLocation();
        //placeAtLoacation.Location = new Location(location.x, location.y);
        string url = $"events/{Global.getEventId()}/waypoints";
        //print(url);
        //placeAtLoacation = GetComponent<PlaceAtLocation>();
        var newLocation = new Location()
        {
            Latitude = location.x,
            Longitude = location.y,
            Altitude = 10,
            AltitudeMode = AltitudeMode.GroundRelative
        };

        var placeAtLocation = GetComponent<PlaceAtLocation>();
        placeAtLocation.Location = newLocation;
        StartCoroutine(getPosition());
    }

    public override void getResponse<T>(T data)
    {
        Debug.Log(data);
        double[][,] waypoints = JsonUtility.FromJson<double[][,]>(data.ToString());
        var newLocation = new Location()
        {
            //Latitude = location.x,
            //Longitude = location.y,
            Altitude = 10,
            AltitudeMode = AltitudeMode.GroundRelative
        };

        var placeAtLocation = GetComponent<PlaceAtLocation>();
        placeAtLocation.Location = newLocation;
    }

    void setLocations()
    {
        //placeAtLoacation.Location = new Location(location.x, location.y);

      //  double[,] coordinates = {
      //  {
      //     -79.896315,
      //     -2.181886
      //  },
      // {
      //     -79.896563,
      //     -2.181976
      // },

      // {
      //     -79.897145,
      //     -2.182231
      // },

      // {
      //     -79.897172,
      //     -2.182176
      // },

      // {
      //     -79.897332,
      //     -2.181813
      // },

      // {
      //     -79.89735,
      //     -2.181749
      // },

      // {
      //     -79.897319,
      //     -2.181692
      // },

      // {
      //     -79.897171,
      //     -2.181619
      // },

      // {
      //     -79.896875,
      //     -2.181494
      //      }
      //};
    }

    IEnumerator getPosition()
    {
        // Check if the user has location service enabled.
        print("dentro de getLocation de location");

        if (!Input.location.isEnabledByUser)
        {
            print("la localizacion esta desabilitada");
            yield break;
        }

        // Starts the location service.
        Input.location.Start();

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            print("esperando activacio de location");
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            string url = $"events/{Global.getEventId()}/waypoints?lng={Input.location.lastData.longitude}&lat={Input.location.lastData.latitude}";

            StartCoroutine(CallGetBackend(url));


        }

        // Stops the location service if there is no need to query location updates continuously.
        Input.location.Stop();
    }




}
