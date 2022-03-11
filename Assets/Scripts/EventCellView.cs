
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System.Collections;
using UnityEngine.Networking;
using KDTree;

public class EventCellView : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI date;
    public RawImage image;
    [Header("Buttons")]
    public Button btnVr;
    public Button btnSuscripted;
    public Button btnDetail;

    public double lat;
    public double lng;
    public bool isActive;
    public int id;

    public EventModel eventModel;

    // public GameObject mainCamera;
    // public GameObject canvasHome;
    // public GameObject canvasAr;
    public void SetData(EventModel data)
    {
        eventModel = data;
        title.text = eventModel.name;
        description.text = eventModel.description;
        date.text = eventModel.start_date;
        isActive = eventModel.status == 1 ? true : false;
        lat = eventModel.position[0];
        lng = eventModel.position[1];
    }


    public void goVr()
    {
        // print("voy a vr");
        // DistanceFunctions distance = new DistanceFunctions();
        // double _distance = distance.Distance(lat, lng);
        
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        GameObject canvasHome = GameObject.FindGameObjectWithTag("CanvasHome");
        // GameObject canvasAr = GameObject.FindGameObjectWithTag("CanvasAr");
        // canvasAr.SetActive(true);
        canvasHome.SetActive(false);
        mainCamera.SetActive(false);
        Location location = new Location(lat, lng);
        Global.setLocation(location);
        Global.setEventId(eventModel.id);
        ManagerScene.LoadSceneVr();
    }

    public void getImage(string url)
    {
        StartCoroutine(DownloadImage(url));
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
}

