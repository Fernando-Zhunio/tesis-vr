using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Android;
using UnityEngine.UI;

public class HomeController : Conexion
{
    private List<ScrollerData> _data;
    public GameObject content;
    public GameObject contentFavorite;
    public EventCellView eventCellViewPrefab;
    [Header("Sections")]
    public ProfileUser profileUser;
    public BestEvent bestEvent;
    public StadisticsEvents stadisticsEvents;
    public GameObject mainCam;
    public GameObject canvasHome;

    public Image imageFavoriteMain;
    EventModel mainEvent;
    int click;

    void Awake()
    {
        if (!Global.IsAuthenticated())
        {
            Global.logout();
            return;
        }
        _data = new List<ScrollerData>();
        NotificationController.ShowToast("Cargando eventos, espere un momento...");
        // StartCoroutine(CallGetBackend(Routes.home));
        HttpGet(Routes.home, fillDataHome);
    }

    void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            click++;
            NotificationController.ShowToast("Presione de nuevo para salir");

            StartCoroutine(ClickTime());

            if (click > 1)
            {
                print("salir");
                Application.Quit();
            }
        }
    }

    IEnumerator ClickTime()
    {
        yield return new WaitForSeconds(1f);
        click = 0;
    }
    private void LoadData(HomeModel homeData)
    {
        stadisticsEvents.SetData(homeData.eventsFavoriteCount, homeData.eventsActivesCount);
        int size = homeData.events.Length;
        if (size > 0)
        {
            EventModel[] data = homeData.events;
            for (int i = 0; i < size; i++)
            {
                eventCellViewPrefab.SetData(data[i]);
                GameObject prefab = Instantiate(eventCellViewPrefab.gameObject, content.transform, false);
                prefab.GetComponent<EventCellView>().getImage(data[i].image);
            }
        }
        else
        {
            NotificationController.ShowToast("No hay eventos disponibles");
        }


    }

    //! ya no vale --------------------------------------------------------------
    public override void getResponse<T>(T _data)
    {
        // print(_data);
        // ResponseSuccessModel<HomeModel> data = JsonUtility.FromJson<ResponseSuccessModel<HomeModel>>(_data.ToString());
        // LoadData(data.data);
        // print(JsonUtility.ToJson(data));
    }

    private void fillDataHome(string response)
    {
        print(response);
        ResponseSuccessModel<HomeModel> homeModel = JsonUtility.FromJson<ResponseSuccessModel<HomeModel>>(response.ToString());
        getFavorite();
        print(JsonUtility.ToJson("home:  " + homeModel.data.events.Length));
        LoadData(homeModel.data);
    }


    public void closeSession()
    {
        Global.logout();
    }

    // public void closeSceneVr()
    // {
    //     canvasHome.SetActive(true);
    //     mainCam.SetActive(true);
    //     ManagerScene.closeSceneAr();
    // }

    public void goMap()
    {
         Global.SetEventMap(mainEvent);
        ManagerPages managerPages = GameObject.FindObjectOfType<ManagerPages>().GetComponent<ManagerPages>();
        managerPages.changedPage("Map");
    }

    public void getFavorite()
    {
        HttpGet(Routes.getFavorites, getFavoriteResponse);
    }


    private void getFavoriteResponse(string _data)
    {
        print(_data);
        ResponseSuccessModel<EventModel[]> data = JsonUtility.FromJson<ResponseSuccessModel<EventModel[]>>(_data);
        if (data.data.Length > 0)
        {
            int size = data.data.Length;
            print(size);
            for (int i = 0; i < size; i++)
            {
                eventCellViewPrefab.SetData(data.data[i]);
                GameObject prefab = Instantiate(eventCellViewPrefab.gameObject, contentFavorite.transform, false);
                prefab.GetComponent<EventCellView>().getImage(data.data[i].image);
            }
        }
        else
        {
            NotificationController.ShowToast("No hay eventos favoritos");
        }
    }

    public void putEventFavorite(GameObject instanceGameobject)
    {
        GameObject prefab = Instantiate(instanceGameobject, contentFavorite.transform, false);
        stadisticsEvents.addCountFavorite();
    }

    public void deleteEventFavorite(int id)
    {
        EventCellView[] eventCellViews = contentFavorite.GetComponentsInChildren<EventCellView>();
        foreach (EventCellView eventCellView in eventCellViews)
        {
            if (eventCellView.id == id)
            {
                Destroy(eventCellView.gameObject);
                stadisticsEvents.subCountFavorite();
            }
        }
        // Destroy(instanceGameobject);
    }
}



[System.Serializable]
public class HomeModel
{
    public EventModel[] events;
    public UserModel user;
    public int eventsFavoriteCount;
    public int eventsActivesCount;
}


[System.Serializable]
public class ProfileUser
{
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI textEmail;
    public TextMeshProUGUI txtVerifiedEmail;
    public TextMeshProUGUI txtIAm;
    public TextMeshProUGUI txtBirthday;

    public void SetData(UserModel user)
    {
        txtName.text = user.name;
        textEmail.text = user.email;
        txtVerifiedEmail.text = string.IsNullOrEmpty(user.email_verified_at) ? "No verificado" : "Verificado";
        txtIAm.text = user.is_student ? "Estudiante" : "Profesor";
        txtBirthday.text = user.birthday;
    }
}

[System.Serializable]
public class BestEvent
{
    public TextMeshProUGUI txtTitle;
    public TextMeshProUGUI txtDescription;
    public TextMeshProUGUI txtDate;

    public void SetData(string title = "Sin titulo", string description = "Nulo", string date = "Nulo")
    {
        txtTitle.text = title;
        txtDescription.text = description;
        txtDate.text = date;
    }
}

[System.Serializable]
public class StadisticsEvents
{
    public TextMeshProUGUI txtCountFavorite;
    public TextMeshProUGUI txtCountEventsActive;
    private int countFavorite;

    public void SetData(int countFavorite = 0, int countEventsActive = 0)
    {
        txtCountFavorite.text = countFavorite.ToString();
        this.countFavorite = countFavorite;
        txtCountEventsActive.text = countEventsActive.ToString();
    }

    public void setFavoriteCount(int count)
    {
        txtCountFavorite.text = count.ToString();
    }

    public void addCountFavorite()
    {
        countFavorite++;
        txtCountFavorite.text = countFavorite.ToString();
    }

    public void subCountFavorite()
    {
        countFavorite--;
        txtCountFavorite.text = countFavorite.ToString();
    }
}
