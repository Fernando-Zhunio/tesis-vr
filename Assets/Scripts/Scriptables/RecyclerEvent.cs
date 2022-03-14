using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class RecyclerEvent : ScriptableObject
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI date;
    public RawImage image;

    [Header("Buttons")]
    public Button btnVr;
    public Button btnSuscripted;
    public Button btnDetail;

    public Location location;
    private Location location_ug_center = new Location(-2.181452614962342, -79.89844529968079);

    RecyclerEvent(EventModel _event)
    {
        title.text = _event.name;
        description.text = _event.description;
        date.text = _event.start_date;
        location = new Location(_event.position[0], _event.position[1]);
        // isActive = _event.status == 1 ? true : false;
        // lat = _event.position[0];
        // lng = _event.position[1];

    }
    public void goVr()
    {
        // StartCoroutine(getPosition());
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
