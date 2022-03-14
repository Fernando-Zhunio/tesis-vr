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
    public TMP_Text txt_info;
    public Location locationUG = Global.GetLocationUG();
    public LocationVr locationVr = new LocationVr();

    // public RenderPathLine renderPathLine;

    // LocationPath path;




    void Start()
    {
        // StartCoroutine(getPosition());
        // path = renderPathLine.PathSettings.LocationPath;
        string lat = "-2.1609372664849325".ToString().Replace(",", ".");
        string lng = "-79.89925870340669".ToString().Replace(",", ".");
        string url = $"events/{Global.getEventId()}/waypoints?lng={lng}&lat={lat}";
        txt_info.text = $"Info: {url}";
        HttpGet(url, ResponseApi, ResponseFail);

        // StartCoroutine(CallGetBackend(url));
    }

    public override void getResponse<T>(T data)
    {
        Debug.Log("waypoints:" + data);
        txt_info.text += "\n waypoints:" + data;
        ResponseCustom response = JsonUtility.FromJson<ResponseCustom>(data.ToString());
        print(response.success + " - " + response.data.start_location[0].ToString());
        locationVr.location1 = new Location(response.data.start_location[1], response.data.start_location[0]);
        locationVr.distance1 = Global.DistanceTo(Input.location.lastData.latitude, Input.location.lastData.longitude, locationVr.location1.latitudDouble, locationVr.location1.longitudDouble);
        if (response.data.end_location != null)
        {
            locationVr.location2 = new Location(response.data.end_location[1], response.data.end_location[0]);
        }
        // List<ARLocation.Location> locations = new List<ARLocation.Location>();
        // for (int i = 0; i < _location.Count; i++)
        // {
        //     locations.Add(new ARLocation.Location(_location[i][1], _location[i][0]));
        // }
        // path.Locations = locations.ToArray();
        updatePointVr(locationVr.location1.latitudDouble, locationVr.location1.longitudDouble);
        txt_info.text += "fist point" + placeAtLocation.Location.Latitude + "," + placeAtLocation.Location.Longitude + "second distance" + locationVr.distance2;

    }

    public void ResponseApi(string data)
    {
        Debug.Log("waypoints:" + data);
        txt_info.text += "\n waypoints:" + data;
        ResponseCustom response = JsonUtility.FromJson<ResponseCustom>(data.ToString());
        print(response.success + " - " + response.data.start_location[0].ToString());
        locationVr.location1 = new Location(response.data.start_location[1], response.data.start_location[0]);
        locationVr.distance1 = Global.DistanceTo(Input.location.lastData.latitude, Input.location.lastData.longitude, locationVr.location1.latitudDouble, locationVr.location1.longitudDouble);
        if (response.data.end_location != null)
        {
            print("end_location");
            // locationVr.location2 = new Location(response.data.end_location[1], response.data.end_location[0]);
            // ARLocation.Location[] locations = { new ARLocation.Location(locationVr.location1.latitudDouble, locationVr.location1.longitudDouble), new ARLocation.Location(locationVr.location2.latitudDouble, locationVr.location2.longitudDouble) };
            // path.Locations = locations;
        }
        updatePointVr(locationVr.location1.latitudDouble, locationVr.location1.longitudDouble);



        txt_info.text += "\n point" + placeAtLocation.Location.Latitude + "," + placeAtLocation.Location.Longitude + " distance" + locationVr.distance2;
    }

    public void ResponseFail(string data)
    {
        StartCoroutine(getPosition());
    }


    IEnumerator getPosition()
    {
        // Check if the user has location service enabled.
        print("dentro de getLocation de location");

        if (!Input.location.isEnabledByUser)
        {
            print("la localizacion esta desabilitada");
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
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            //print("Timed out");
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
            string lat = Input.location.lastData.latitude.ToString().Replace(",", ".");
            string lng = Input.location.lastData.longitude.ToString().Replace(",", ".");
            string url = $"events/{Global.getEventId()}/waypoints?lng={lng}&lat={lat}";
            txt_info.text = $"Info: {url}";
            HttpGet(url, ResponseApi, ResponseFail);
            // StartCoroutine(CallGetBackend(url));
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

    private ARLocation.Location generateArLocation(double lat, double lng)
    {
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

        float bearing = angleFromCoordinate(latitude, longitude, latitudeGo, longitudeGo);
        arrowObject.rotation = Quaternion.Slerp(arrowObject.rotation, Quaternion.Euler(0, Input.compass.magneticHeading + bearing, 0), 100f);
        // print("coordenadas"+Input.location.lastData.latitude+" - "+ Input.location.lastData.longitude);
        locationVr.distance1 = Global.DistanceTo(latitude, longitude, latitudeGo, longitudeGo);
        if (locationVr.distance1 < 5)
        {
            StartCoroutine(getPosition());
        }
        txt_distance.text = $"Distancia para el waypoint: {Global.DistanceTo(Input.location.lastData.latitude, Input.location.lastData.longitude, locationVr.location1.latitudDouble, locationVr.location1.longitudDouble)} m";
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
[System.Serializable]
public class ResponseCustom
{
    public bool success;
    public ResponseDataCustom data;
}
[System.Serializable]
public class ResponseDataCustom
{
    public double[] start_location;
    public double[] end_location;
    public bool is_end;
}

// [System.Serializable]
// public static class JsonUtil {

//     ///< summary > Convert objects to JSON strings < / summary >
//     ///< param name = "obj" > object < / param >
//     public static string ToJson<T>(T obj) {
//         if (obj == null) return "null";

//         if (typeof(T).GetInterface("IList") != null) {
//             Pack<T> pack = new Pack<T>();
//             pack.data = obj;
//             string json = JsonUtility.ToJson(pack);
//             return json.Substring(8, json.Length - 9);
//         }

//         return JsonUtility.ToJson(obj);
//     }

//     ///< summary > parse JSON < / summary >
//     ///< typeparam name = "t" > type < / typeparam >
//     ///< param name = "JSON" > JSON string < / param >
//     public static T FromJson<T>(string json) {
//         if (json == "null" && typeof(T).IsClass) return default(T);

//         if (typeof(T).GetInterface("IList") != null) {
//             json = "{\"data\":{data}}".Replace("{data}", json);
//             Pack<T> Pack = JsonUtility.FromJson<Pack<T>>(json);
//             return Pack.data;
//         }

//         return JsonUtility.FromJson<T>(json);
//     }

//     ///< summary > inner wrapper class < / summary >
//     private class Pack<T> {
//         public T data;
//     }

// }