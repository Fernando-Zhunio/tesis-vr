using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;

public class RecyclerController : Conexion
{

    private List<ScrollerData> _data;
    public GameObject content;
    //public EnhancedScroller scroller;
    public EventCellView eventCellViewPrefab;

    public EventLoadingCellView loadingCellViewPrefab;

    //public int cellHeight;

    public int pageCount;

    private bool _loadingNew;

    // Start is called before the first frame update
    void Start()
    {
        //scroller.Delegate = this;
        //scroller.scrollerScrolled = ScrollerScrolled;
        print("RecyclerController");
        _data = new List<ScrollerData>();


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
            //ScrollerData scrollerData = new ScrollerData();
            //scrollerData.title = data[i].name;
            //scrollerData.description = data[i].description;
            //scrollerData.date = data[i].start_date;
            //scrollerData.imageUrl = data[i].image != "" ? data[i].image : null;
            //scrollerData.position = data[i].position;
            eventCellViewPrefab.SetData(data[i]);
            GameObject prefab = Instantiate(eventCellViewPrefab.gameObject, content.transform, false);
            prefab.GetComponent<EventCellView>().getImage(data[i].image);
        }
        _loadingNew = false;
    }
}
