using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;




public class ManagerMap : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;
    [SerializeField]
    GameObject _markerPrefab;
    [SerializeField]
    float _spawnScale = 100f;
    [SerializeField]
    public string location;
    Vector2d _location;
    GameObject instance = null;

    void OnEnable()
    {
       
            Debug.Log("PrintOnEnable: script was enabled");
            EventModel eventModel = Global.GetEventMap();
            location = Global.GetLocationEventMap().GetLatLonString();
            _markerPrefab.transform.Find("TextMesh").GetComponent<TextMesh>().text = eventModel.name;
            if(instance != null)
            {
                Destroy(instance);
            }
            instance = Instantiate(_markerPrefab);
            _map.Options.locationOptions.latitudeLongitude = location;
            print(location);
            _location = Conversions.StringToLatLon(location);
            instance.transform.localPosition = _map.GeoToWorldPosition(_location, true);
            instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        
    }
    private void Update()
    {
        instance.transform.localPosition = _map.GeoToWorldPosition(_location, true);
        instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
    }


}
