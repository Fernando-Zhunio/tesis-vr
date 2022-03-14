//using ARLocation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARLocation;
using TMPro;
//[RequireComponent(typeof(PlaceAtLocation))]
public class VrManager : Conexion
{
    public PlaceAtLocation placeAtLocation;
    public Transform arrowObject;
    public TMP_InputField txt_lat_lng;
    public TMP_Text txt_distance;
    public Location locationUG = Global.GetLocationUG();
    public LocationVr locationVr = new LocationVr();

    public RenderPathLine renderPathLine;

    LocationPath path;




    void Start()
    {
        StartCoroutine(getPosition());
    }

    public override void getResponse<T>(T data)
    {
        Debug.Log("waypoints:" + data);
        ResponseSuccessModel<double[][]> response = JsonUtility.FromJson<ResponseSuccessModel<double[][]>>(data.ToString());
        double[][] _location = response.data;
        locationVr.location1 = new Location(_location[0][0], _location[0][1]);
        locationVr.distance1 = Global.DistanceTo(Input.location.lastData.latitude, Input.location.lastData.longitude, locationUG.latitudDouble, locationUG.longitudDouble);
        if (_location.Length > 1)
        {
            locationVr.location2 = new Location(_location[1][1], _location[1][0]);
        }
        List<ARLocation.Location> locations = new List<ARLocation.Location>();
        for (int i = 0; i < _location.Length; i++)
        {
            locations.Add(new ARLocation.Location(_location[i][1], _location[i][0]));
        }

        path.Locations = locations.ToArray();
        
        updatePointVr(locationVr.location1.latitudDouble, locationVr.location1.longitudDouble);
        txt_distance.text += "fist point" + placeAtLocation.Location.Latitude+ ","+ placeAtLocation.Location.Longitude + "second distance" + locationVr.distance2;

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
            string url = $"events/{Global.getEventId()}/waypoints?lng={Input.location.lastData.longitude}&lat={Input.location.lastData.latitude}";
            txt_distance.text += "Info:" + url;
            StartCoroutine(CallGetBackend(url));
        }
        // Stops the location service if there is no need to query location updates continuously.
        Input.location.Stop();
    }

    public void updatePointVr(double lat, double lng, double alt = 0)
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

    private ARLocation.Location generateArLocation(double lat, double lng){
        return new ARLocation.Location()
        {
            Latitude = lat,
            Longitude = lng,
            Altitude = 0,
            AltitudeMode = AltitudeMode.DeviceRelative
        };

    }

    private void Update()
    {
        double latitude = Input.location.lastData.latitude;
        double longitude = Input.location.lastData.longitude;

        double latitudeGo = locationVr.location1.latitudDouble;
        double longitudeGo = locationVr.location1.longitudDouble;

        float bearing = angleFromCoordinate(latitude, longitude,latitudeGo, longitudeGo);
        arrowObject.rotation = Quaternion.Slerp(arrowObject.rotation, Quaternion.Euler(0, Input.compass.magneticHeading + bearing, 0), 100f);
        // print("coordenadas"+Input.location.lastData.latitude+" - "+ Input.location.lastData.longitude);
        locationVr.distance1 = Global.DistanceTo(latitude, longitude, latitudeGo, longitudeGo);
        if (locationVr.distance1 < 5)
        {
            StartCoroutine(getPosition());
        }
        txt_distance.text = $"{Global.DistanceTo(Input.location.lastData.latitude, Input.location.lastData.longitude,  locationVr.location1.latitudDouble, locationVr.location1.longitudDouble)} m";
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

    public void newLocationGo()
    {
        string stringLatLng = txt_lat_lng.text;
        string[] latLng = stringLatLng.Split(',');
        locationUG = new Location(double.Parse(latLng[0]), double.Parse(latLng[1]));
    }
}

[System.Serializable]
public class LocationVr
{
    public Location location1;
    public Location location2;
    public double distance1;
    public double distance2;
}
