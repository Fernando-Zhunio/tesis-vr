
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System.Collections;
using UnityEngine.Networking;
using System;
// using KDTree;
// using System.Device.Location;

public class EventCellView : Conexion
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI date;
    public RawImage image;

    [Header("Buttons")]
    public Button btnVr;
    public Button btnSuscripted;
    public Button btnDetail;
    private Location location_ug_center = new Location(-2.181452614962342, -79.89844529968079);
    // public double lat;
    // public double lng;
    // public bool isActive;
    public Location location;
    public int id;
    private bool isFavorite = false;
    // public EventModel eventModel;

    public Image imageFavorite;
    public Sprite notFavorite;

    public Sprite favorite;

    public void SetData(EventModel data)
    {
        // eventModel = data;
        title.text = data.name;
        description.text = data.description;
        date.text = data.start_date;
        id = data.id;
        isFavorite = data.is_favorite;
        print(isFavorite);
        if (isFavorite)
        {
            imageFavorite.sprite = favorite;
        }
        else
        {
            imageFavorite.sprite = notFavorite;
        }
        location = new Location(data.position[0], data.position[1]);
    }


    public void goVr()
    {
        // StartCoroutine(getPosition());
        double lat = -2.1609372664849325;
        double lng = -79.89925870340669;
        goVrApproved(lat, lng);

    }



    public void getImage(string url)
    {
        StartCoroutine(DownloadImage(url));
    }

    public void doFavorite()
    {
        string url = Routes.eventfavorite(id);
        print(url);
        StartCoroutine(CallPostBackend(url, null));
    }

    IEnumerator DownloadImage(string url)
    {
        Debug.Log("Download");
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            image.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }

    IEnumerator getPosition()
    {
        // Check if the user has location service enabled.
        print("dentro de getLocation de location");

        if (!Input.location.isEnabledByUser)
        {
            print("la localizacion esta desabilitada");
            NotificationController.ShowAlert("Por favor habilite la ubicación para continuar y conseda permiso de ubicación", "Ubicación deshabilitada", null);
            yield break;
        }

        // Starts the location service.
        Input.location.Start();

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            NotificationController.ShowToast("Esperando activación de ubicacion");
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            NotificationController.ShowToast("Ubicación deshabilitada tiempo agotado");
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
            goVrApproved(Input.location.lastData.latitude, Input.location.lastData.longitude);
        }

        // Stops the location service if there is no need to query location updates continuously.
        Input.location.Stop();
    }


    public void goVrApproved(double lat, double lng)
    {
        // double distance = DistanceTo(-2.1780861981138884, -79.90192144256308,location_ug_center.latitudDouble, location_ug_center.longitudDouble);
        // if (distance > 500) {
        //     print("no estas en la universidad, distancia: " + distance);
        //     NotificationController.ShowToast("No se encuentra en el área de cobertura, distancia: " + distance + " metros");
        //     return;
        // }
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        GameObject canvasHome = GameObject.FindGameObjectWithTag("CanvasHome");
        canvasHome.SetActive(false);
        mainCamera.SetActive(false);
        Location location = new Location(lat, lng);
        Global.setLocation(location);
        Global.setEventId(id);
        ManagerScene.LoadSceneVr();
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

    public override void getResponse<T>(T data)
    {
        Debug.Log(data);
        if (isFavorite)
        {
            isFavorite = false;
            imageFavorite.sprite = notFavorite;
        }
        else
        {
            isFavorite = true;
            imageFavorite.sprite = favorite;
        }
    }
}

