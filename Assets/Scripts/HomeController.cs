using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Android;

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
        StartCoroutine(CallGetBackend(Routes.home));
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

    public void GoVrMainEvent()
    {
        Location location = new Location(mainEvent.position[1], mainEvent.position[0]);
        ControllerGlobalSingletons.Instance.ActiveVr(mainEvent.id, location);
    }
    IEnumerator ClickTime()
    {
        yield return new WaitForSeconds(1f);
        click = 0;
    }


    private void LoadData(HomeModel homeData)
    {
        stadisticsEvents.SetData(homeData.eventsFavoriteCount, 0);
        int size = homeData.events.Length;
        if (size > 0)
        {
            EventModel[] data = homeData.events;
            for (int i = 1; i < size; i++)
            {
                eventCellViewPrefab.SetData(data[i]);
                GameObject prefab = Instantiate(eventCellViewPrefab.gameObject, content.transform, false);
                prefab.GetComponent<EventCellView>().getImage(data[i].image);
            }
             mainEvent = homeData.events[0];
            profileUser.SetData(homeData.user);
            bestEvent.SetData(mainEvent.name, mainEvent.description, mainEvent.start_date);
            // Si no se asigna no se muestra el mapa
            Location location = new Location(mainEvent.position[1], mainEvent.position[0]);
            Global.SetEventMap(mainEvent);
        }
        else
        {
            NotificationController.ShowToast("No hay eventos disponibles");
        }


    }

    public override void getResponse<T>(T _data)
    {
        print(_data);
        ResponseSuccessModel<HomeModel> data = JsonUtility.FromJson<ResponseSuccessModel<HomeModel>>(_data.ToString());
        LoadData(data.data);
        print(JsonUtility.ToJson(data));
    }


    public void closeSession()
    {
        Global.logout();
    }

    public void closeSceneVr()
    {
        canvasHome.SetActive(true);
        mainCam.SetActive(true);
        ManagerScene.closeSceneAr();
    }

    public void getFavorite()
    {
        // isGetFavorite = true;
        // StartCoroutine(CallGetBackend(Routes.getFavorites));
        HttpGet(Routes.getFavorites, getFavoriteResponse);
    }

    private void getFavoriteResponse(string _data)
    {
        ResponseSuccessModel<EventModel[]> data = JsonUtility.FromJson<ResponseSuccessModel<EventModel[]>>(_data);
        print(JsonUtility.ToJson(data));
        if (data.data.Length > 0)
        {
            // contentCurrent = contentFavorite;
            int size = data.data.Length;
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
}



[System.Serializable]
public class HomeModel
{
    public EventModel[] events;
    public UserModel user;
    public int eventsFavoriteCount;
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
    public TextMeshProUGUI txtCountSubscription;

    public void SetData(int countFavorite = 0, int countSubscription = 0)
    {
        txtCountFavorite.text = countFavorite.ToString();
        txtCountSubscription.text = countSubscription.ToString();
    }
}
