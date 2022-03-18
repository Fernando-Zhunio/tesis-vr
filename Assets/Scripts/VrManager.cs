//using ARLocation;
using System.Collections;
using System;
using UnityEngine;
// using ARLocation;
// using TMPro;
using ARLocation.MapboxRoutes;
public class VrManager : Conexion
{

    public MapboxRoute mapBoxRoute;

    private void Start()
    {
        Location location = Global.GetLocation();
        print(location.latitud);
        RouteWaypoint waypointStart = new RouteWaypoint()
        {
            Type = RouteWaypointType.UserLocation,
        };
        RouteWaypoint waypointEnd = new RouteWaypoint()
        {
            Type = RouteWaypointType.Location,
            Location = new ARLocation.Location()
            {
                Latitude = location.latitudDouble,
                Longitude = location.longitudDouble,
                Altitude = 0,
                AltitudeMode = ARLocation.AltitudeMode.DeviceRelative
            }
        };
        mapBoxRoute.LoadRoute(waypointStart, waypointEnd);
    }
    public override void getResponse<T>(T data)
    {

    }













    // // public PlaceAtLocations placeAtLocations;
    // public PlaceAtLocation placeAtLocation;
    // // public Transform arrowObject;
    // public TMP_Text txt_myLocation;
    // public TMP_Text txt_distance;
    // public TMP_Text txt_info;
    // public Location locationUG = Global.GetLocationUG();
    // public LootAt lootAtArrow;
    // public Location goLocation = null;
    // public Location goLocationSecond = null;
    // private double distance = 1000;
    // public Transform targetTranform;
    // public Transform myTransform;
    // public GameObject gameObjectInstantiate;
    // PlaceAtLocation newPlaceAtLocation;




    // void Start()
    // {
    //     StartCoroutine(getPosition());
    //     // path = renderPathLine.PathSettings.LocationPath;
    //     // string lat = "-2.1609372664849325".ToString().Replace(",", ".");
    //     // string lng = "-79.89925870340669".ToString().Replace(",", ".");
    //     // // string url = $"events/{Global.getEventId()}/waypoints?lng={lng}&lat={lat}";
    //     // string url = $"events/2/waypoints?lng={lng}&lat={lat}";
    //     // txt_info.text = $"Info: {url}";
    //     // HttpGet(url, ResponseApi, ResponseFail);

    //     // StartCoroutine(CallGetBackend(url));
    // }

    // public override void getResponse<T>(T data)
    // {
    //     // Debug.Log("waypoints:" + data);
    //     // txt_info.text += "\n waypoints:" + data;
    //     // ResponseCustom response = JsonUtility.FromJson<ResponseCustom>(data.ToString());
    //     // print(response.success + " - " + response.data.start_location[0].ToString());

    //     // Location myLocation = new Location(Input.location.lastData.latitude, Input.location.lastData.longitude);
    //     // goLocation = new Location(response.data.start_location[1], response.data.start_location[0]);
    //     // locationVr.distance1 = Global.DistanceTo(Input.location.lastData.latitude, myLocation.longitudDouble, locationVr.location1.latitudDouble, locationVr.location1.longitudDouble);
    //     // if (response.data.end_location != null)
    //     // {
    //     //     locationVr.location2 = new Location(response.data.end_location[1], response.data.end_location[0]);
    //     // }
    //     // updatePointVr(goLocation.latitudDouble, goLocation.longitudDouble);
    //     // txt_info.text += $"punto del waypoint: {placeAtLocation.Location.Latitude}, {placeAtLocation.Location.Longitude}  "
    //     // txt_info.text += $"mi posicion: {locationVr.distance}";

    // }

    // public void ResponseApi(string data)
    // {
    //     Debug.Log("waypoints:" + data);
    //     txt_info.text += "\n waypoints:" + data;
    //     ResponseCustom response = JsonUtility.FromJson<ResponseCustom>(data.ToString());
    //     print(response.success + " - " + response.data.start_location[0].ToString());

    //     Location myLocation = getMyLocation();
    //     goLocation = new Location(response.data.start_location[1], response.data.start_location[0]);
    //     // distance = Global.DistanceTo(myLocation.latitudDouble, myLocation.longitudDouble, goLocation.latitudDouble, goLocation.longitudDouble);
    //     if (response.data.end_location != null)
    //     {
    //         goLocationSecond = new Location(response.data.end_location[1], response.data.end_location[0]);
    //         // instantiatePrefabLocation(goLocationSecond.latitudDouble, goLocationSecond.longitudDouble);
    //     }
    //     updatePointVr(goLocation.latitudDouble, goLocation.longitudDouble);
    //     // txt_distance.text = distance.ToString();
    //     txt_info.text += $"punto del waypoint: {placeAtLocation.Location.Latitude}, {placeAtLocation.Location.Longitude}";
    //     txt_myLocation.text += $"mi posición: {myLocation.latitud}, {myLocation.longitud}";



    //     // txt_info.text += "\n waypoints:" + data;
    //     // ResponseCustom response = JsonUtility.FromJson<ResponseCustom>(data.ToString());
    //     // print(response.success + " - " + response.data.start_location[0].ToString());

    //     // Location myLocation = getMyLocation();
    //     // goLocation = new Location(response.data.start_location[1], response.data.start_location[0]);
    //     // var newLocation = new PlaceAtLocation.LocationSettingsData()
    //     // {
    //     //     LocationInput = new LocationPropertyData()
    //     //     {
    //     //         LocationInputType = LocationPropertyData.LocationPropertyType.Location,
    //     //         Location = new ARLocation.Location()
    //     //         {
    //     //             Latitude = goLocation.latitudDouble,
    //     //             Longitude = goLocation.longitudDouble,
    //     //             Altitude = 0,
    //     //             AltitudeMode = AltitudeMode.DeviceRelative
    //     //         }
    //     //     }
    //     // };
    //     // placeAtLocations.Locations.Add(newLocation);
    //     // if (response.data.end_location != null)
    //     // {
    //     //     goLocationSecond = new Location(response.data.end_location[1], response.data.end_location[0]);
    //     //     var newLocationSecond = new PlaceAtLocation.LocationSettingsData()
    //     //     {
    //     //         LocationInput = new LocationPropertyData()
    //     //         {
    //     //             LocationInputType = LocationPropertyData.LocationPropertyType.Location,
    //     //             Location = new ARLocation.Location()
    //     //             {
    //     //                 Latitude = goLocationSecond.latitudDouble,
    //     //                 Longitude = goLocationSecond.longitudDouble,
    //     //                 Altitude = 0,
    //     //                 AltitudeMode = AltitudeMode.DeviceRelative
    //     //             }
    //     //         }
    //     //     };
    //     //     placeAtLocations.Locations.Add(newLocation);
    //     //     // instantiatePrefabLocation(goLocationSecond.latitudDouble, goLocationSecond.longitudDouble);
    //     // }


    // }

    // public void ResponseFail(string data)
    // {
    //     StartCoroutine(getPosition());
    // }


    // IEnumerator getPosition()
    // {
    //     // Check if the user has location service enabled.
    //     print("dentro de getLocation de location");

    //     if (!Input.location.isEnabledByUser)
    //     {
    //         NotificationController.ShowToast("Ubicación deshabilitada");
    //         yield break;
    //     }
    //     // Starts the location service.
    //     Input.location.Start();

    //     // Waits until the location service initializes
    //     int maxWait = 20;
    //     while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
    //     {
    //         yield return new WaitForSeconds(1);
    //         maxWait--;
    //     }
    //     // If the service didn't initialize in 20 seconds this cancels location service use.
    //     if (maxWait < 1)
    //     {
    //         yield break;
    //     }
    //     // If the connection failed this cancels location service use.
    //     if (Input.location.status == LocationServiceStatus.Failed)
    //     {
    //         NotificationController.ShowToast("Ubicación deshabilitada tiempo agotado");
    //         print("Unable to determine device location");
    //         yield break;
    //     }
    //     else
    //     {
    //         // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
    //         string message = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
    //         NotificationController.ShowToast(message);
    //         Location myLocation = getMyLocation();
    //         string url = $"events/{Global.getEventId()}/waypoints?lng={myLocation.longitud}&lat={myLocation.latitud}";
    //         txt_info.text = $"Info url: {url}";
    //         HttpGet(url, ResponseApi, ResponseFail);
    //         // StartCoroutine(CallGetBackend(url));
    //     }
    //     // Stops the location service if there is no need to query location updates continuously.
    //     // Input.location.Stop();
    // }

    // public void updatePointVr(double lat, double lng, double alt = 0)
    // {
    //     var newLocation = new ARLocation.Location()
    //     {
    //         Latitude = lat,
    //         Longitude = lng,
    //         Altitude = 0,
    //         AltitudeMode = AltitudeMode.DeviceRelative
    //     };
    //     placeAtLocation.Location = newLocation;
    // }

    // private Location getMyLocation()
    // {
    //     double latitude = Input.location.lastData.latitude;
    //     double longitude = Input.location.lastData.longitude;
    //     return new Location(latitude, longitude);
    // }

    // private void Update()
    // {
    //     Location myLocation = getMyLocation();
    //     // float bearing = angleFromCoordinate(myLocation.latitudDouble, myLocation.longitudDouble, goLocation.latitudDouble, goLocation.longitudDouble);
    //     // arrowObject.rotation = Quaternion.Slerp(arrowObject.rotation, Quaternion.Euler(0, Input.compass.magneticHeading + bearing, 0), 100f);
    //     // distance = Global.DistanceTo(myLocation.latitudDouble, myLocation.longitudDouble, goLocation.latitudDouble, goLocation.longitudDouble);


    //     // distance = Vector3.Distance(myTransform.position, targetTranform.position);
    //     float distance = placeAtLocation.SceneDistance;


    //     if (distance < 3)
    //     {
    //         if (goLocationSecond != null)
    //         {
    //             goLocation = goLocationSecond;
    //             goLocationSecond = null;
    //         }
    //         StartCoroutine(getPosition());
    //         // placeAtLocation = newPlaceAtLocation;
    //     }
    //     txt_distance.text = Convert.ToInt32(distance).ToString();
    //     txt_myLocation.text = $" {myLocation.latitud}, {myLocation.longitud}";
    // }

    // private float angleFromCoordinate(double _lat1, double _long1, double _lat2, double _long2)
    // {
    //     float lat1 = float.Parse(_lat1.ToString());
    //     float long1 = float.Parse(_long1.ToString());
    //     float lat2 = float.Parse(_lat2.ToString());
    //     float long2 = float.Parse(_long2.ToString());
    //     lat1 *= Mathf.Deg2Rad;
    //     lat2 *= Mathf.Deg2Rad;
    //     long1 *= Mathf.Deg2Rad;
    //     long2 *= Mathf.Deg2Rad;

    //     float dLon = (long2 - long1);
    //     float y = Mathf.Sin(dLon) * Mathf.Cos(lat2);
    //     float x = (Mathf.Cos(lat1) * Mathf.Sin(lat2)) - (Mathf.Sin(lat1) * Mathf.Cos(lat2) * Mathf.Cos(dLon));
    //     float brng = Mathf.Atan2(y, x);
    //     brng = Mathf.Rad2Deg * brng;
    //     brng = (brng + 360) % 360;
    //     brng = 360 - brng;
    //     return brng;
    // }

    // public void instantiatePrefabLocation(double lat, double lng)
    // {
    //     var loc = new ARLocation.Location()
    //     {
    //         Latitude = lat,
    //         Longitude = lng,
    //         Altitude = 0,
    //         AltitudeMode = AltitudeMode.GroundRelative
    //     };

    //     var opts = new PlaceAtLocation.PlaceAtOptions()
    //     {
    //         HideObjectUntilItIsPlaced = true,
    //         MaxNumberOfLocationUpdates = 2,
    //         MovementSmoothing = 0.1f,
    //         UseMovingAverage = false
    //     };

    //     newPlaceAtLocation = PlaceAtLocation.AddPlaceAtComponent(gameObjectInstantiate, loc, opts);
    // }

}

// [System.Serializable]
// public class LocationVr
// {
//     public Location location1;
//     public Location location2;
//     public double distance1;
//     public double distance2;
// }
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
