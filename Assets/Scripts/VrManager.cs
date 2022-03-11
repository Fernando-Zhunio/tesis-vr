//using ARLocation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARLocation;
//[RequireComponent(typeof(PlaceAtLocation))]
public class VrManager : Conexion
{

    //public CustomDialogController dialogController;
    public PlaceAtLocation placeAtLocation;
    public Transform arrowObject;

    public Location locationUG = Global.GetLocationUG();

    void Start()
    {
        StartCoroutine(getPosition());
    }

    public override void getResponse<T>(T data)
    {
        Debug.Log("waypoints:" + data);
        double[][,] waypoints = JsonUtility.FromJson<double[][,]>(data.ToString());
    }

    void setLocations()
    {

    }

    IEnumerator getPosition()
    {
        // Check if the user has location service enabled.
        print("dentro de getLocation de location");

        if (!Input.location.isEnabledByUser)
        {
            print("la localizacion esta desabilitada");
            // ShowDialog("Error", "Ubicacion desabilitada");
            NotificationController.ShowToast("Ubicación deshabilitada");
            yield break;
        }

        // Starts the location service.
        Input.location.Start();

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            //print("esperando activacio de location");
            //Global.ShowAndroidToastMessage("Info", "Esperando activacion de ubicacion", NotificationType.info);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            //print("Timed out");
            //Global.ShowAndroidToastMessage("Error", "Ubicacion desabilitada", NotificationType.danger);
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            NotificationController.ShowToast("Ubicación deshabilitada tiempo agotado");
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            string message = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
            NotificationController.ShowToast(message);

            updatePointVr(Input.location.lastData.latitude, Input.location.lastData.longitude, Input.location.lastData.altitude);

            string url = $"events/{Global.getEventId()}/waypoints?lng={Input.location.lastData.longitude}&lat={Input.location.lastData.latitude}";
            StartCoroutine(CallGetBackend(url));
        }
        // Stops the location service if there is no need to query location updates continuously.
        Input.location.Stop();
    }

    public void updatePointVr(double lat, double lng, double alt)
    {
        var newLocation = new ARLocation.Location()
        {
            Latitude = lat,
            Longitude = lng,
            Altitude = 0,
            AltitudeMode = AltitudeMode.DeviceRelative
        };
        placeAtLocation.Location = newLocation;
    }

     private void Update() {
         float bearing = angleFromCoordinate(Input.location.lastData.latitude, Input.location.lastData.longitude,
             locationUG.latitudDouble,locationUG.longitudDouble);
         
         arrowObject.rotation = Quaternion.Slerp(arrowObject.rotation, Quaternion.Euler(0,  Input.compass.magneticHeading + bearing, 0), 100f);
    }

    private float angleFromCoordinate(double _lat1, double _long1, double _lat2, double _long2)
    {
        float lat1 = float.Parse(_lat1.ToString());
        float long1 = float.Parse(_long1.ToString());
        float lat2 = float.Parse(_lat2.ToString());
        float long2 = float.Parse(_long2.ToString());
        lat1 *= Mathf.Deg2Rad;
        lat2 *= Mathf.Deg2Rad;
        long1 *= Mathf.Deg2Rad;
        long2 *= Mathf.Deg2Rad;

        float dLon = (long2 - long1);
        float y = Mathf.Sin(dLon) * Mathf.Cos(lat2);
        float x = (Mathf.Cos(lat1) * Mathf.Sin(lat2)) - (Mathf.Sin(lat1) * Mathf.Cos(lat2) * Mathf.Cos(dLon));
        float brng = Mathf.Atan2(y, x);
        brng = Mathf.Rad2Deg * brng;
        brng = (brng + 360) % 360;
        brng = 360 - brng;
        return brng;
    }


}
