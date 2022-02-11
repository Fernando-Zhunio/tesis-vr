
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System.Collections;
using UnityEngine.Networking;

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

    public float lat;
    public float lng;
    public bool isActive;
    public int id;

    public EventModel eventModel;
    public void SetData(EventModel data)
    {
        eventModel = data;
        title.text = eventModel.name;
        description.text = eventModel.description;
        date.text = eventModel.start_date;
        isActive = eventModel.status == 1 ? true : false;
        lat = eventModel.position[0];
        lng = eventModel.position[1];

        //btnVr.GetComponent<Button>().onClick.AddListener(() =>goVr());
    }


    public void goVr()
    {
        print("voy a vr");
        GameObject mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        GameObject canvasHome = GameObject.FindGameObjectWithTag("CanvasHome");
        canvasHome.SetActive(false);
        mainCam.SetActive(false);
        Global.setLocation(lat, lng);
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

