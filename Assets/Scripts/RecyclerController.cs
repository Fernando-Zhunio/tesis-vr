using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclerController : Conexion
{

    private List<ScrollerData> _data;
    public GameObject content;
    public EventCellView eventCellViewPrefab;
    public int pageCount;


    // private bool _loadingNew;

    // Start is called before the first frame update
    void Start()
    {
        _data = new List<ScrollerData>();
        NotificationController.ShowProgressDialog("Cargando eventos", "Espere un momento");
        StartCoroutine(CallGetBackend(Routes.events));
    }
    public override void getResponse<T>(T _data)
    {
        print(_data);
        ResponseSuccessModel<PaginateModel<EventModel>> data = JsonUtility.FromJson<ResponseSuccessModel<PaginateModel<EventModel>>>(_data.ToString());
        LoadData(data.data);
        print(JsonUtility.ToJson(data));
    }



    private void LoadData(PaginateModel<EventModel> paginate)
    {
        int size = paginate.data.Length;
        EventModel[] data = paginate.data;
        // add data to the list
        for (int i = 0; i < size; i++)
        {
            eventCellViewPrefab.SetData(data[i]);
            GameObject prefab = Instantiate(eventCellViewPrefab.gameObject, content.transform, false);
            prefab.GetComponent<EventCellView>().getImage(data[i].image);
        }
    }

    
}
