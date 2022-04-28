
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
    public TextMeshProUGUI date_end;
    public RawImage image;

    [Header("Buttons")]
    public Button btnVr;
    public Button btnSuscripted;
    public Button btnDetail;
    public Location location;
    public int id;
    // private bool isFavorite = false;

    public Image imageFavorite;
    public Sprite favorite;
    public Sprite notFavorite;


    public Image imageLike;
    public Sprite spriteLike;
    public Sprite spriteNotLike;


    public EventModel eventModel;

    public GameObject dropdownContent;

    private bool like;


    public void SetData(EventModel data)
    {
        eventModel = data;
        title.text = data.name;
        description.text = data.description;
        date.text = data.start_date;
        date_end.text = data.end_date;
        id = data.id;
        // isFavorite = data.is_favorite;
        if (data.is_favorite)
        {
            imageFavorite.sprite = favorite;
        }
        else
        {
            imageFavorite.sprite = notFavorite;
        }

        if (data.has_liked)
        {
            imageLike.sprite = spriteLike;
        }
        else
        {
            imageLike.sprite = spriteNotLike;
        }
        location = new Location(data.position[1], data.position[0]);
    }

    public void goMap()
    {
        Global.SetEventMap(eventModel);
        ManagerPages managerPages = GameObject.FindObjectOfType<ManagerPages>().GetComponent<ManagerPages>();
        managerPages.changedPage("Map");
    }


    public void goVr()
    {
        ControllerGlobalSingletons.Instance.ActiveVr(id, location);
    }

    public void Subscription()
    {
        if (verifyContainAlert(eventModel.id))
        {
            NotificationController.ShowToast("Ya estas suscrito a este evento");
            print("Ya estas suscrito a este evento");
            return;
        }
        if (isTranscurrentEvent())
        {
            NotificationController.ShowToast("No puedes alertar a un evento que ya ha comenzado");
            print("No puedes alertar a un evento que ya ha comenzado");
            return;
        }
        print("Suscrito");
        DateTime date_event = Convert.ToDateTime(eventModel.start_date).Subtract(new System.TimeSpan(0, 2, 0, 0));
        DateTime now = DateTime.Now;
        System.TimeSpan res = date_event.Subtract(now);
        Global.SetEventAlert(eventModel.id);
        NotificationController.ShowToast("El evento comienza en " + res.Days + " días " + res.Hours + " horas " + res.Minutes + " minutos");
        GleyNotifications.SendNotification("Un evento esta por comenzar!", $"{eventModel.name} - Inicio dentro de dos horas", res, null, null, "Opened Notification");
    }

    private bool verifyContainAlert(int id)
    {
            return Global.GetEventAlert().Contains(id);
    }

    public bool isTranscurrentEvent()
    {
        DateTime now = DateTime.Now;
        DateTime start = Convert.ToDateTime(eventModel.start_date);
        DateTime end = Convert.ToDateTime(eventModel.end_date);

        if (now > start && now < end)
        {
            return true;
        }

        return false;
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

    public void doLikeOrUnlike()
    {
        string url = Routes.eventLikeOrUnlike(id);
        // print(url);
        // StartCoroutine(CallPostBackend(url, null));
        HttpPost(url, null, responseDoLikeOrUnlike);
    }

    private void responseDoLikeOrUnlike(string text)
    {
        ResponseSuccessModel<bool> response = JsonUtility.FromJson<ResponseSuccessModel<bool>>(text);
        if (response.success)
        {
            like = response.data;
            if (like)
            {
                imageLike.sprite = spriteLike;
            }
            else
            {
                imageLike.sprite = spriteNotLike;
            }
        }
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
        Global.SetLocation(location);
        Global.setEventId(id);
        ManagerScene.LoadSceneVr();

    }


    public override void getResponse<T>(T data)
    {
        Debug.Log("favorite:" + data);
        ResponseSuccessModel<bool> response = JsonUtility.FromJson<ResponseSuccessModel<bool>>(data as string);

        if (response.data)
        {
            // isFavorite = false;
            // eventModel.is_favorite = false;
            imageFavorite.sprite = favorite;
            HomeController homeController = GameObject.FindObjectOfType<HomeController>().GetComponent<HomeController>();
            homeController.deleteEventFavorite(id);
        }
        else
        {
            // isFavorite = true;
            // eventModel.is_favorite = true;
            imageFavorite.sprite = notFavorite;
            HomeController homeController = GameObject.FindObjectOfType<HomeController>().GetComponent<HomeController>();
            homeController.putEventFavorite(this.gameObject);
        }
    }

    public void openMenuMore()
    {
        dropdownContent.SetActive(true);
    }

    public void closeMenuMore()
    {
        dropdownContent.SetActive(false);
    }
}

