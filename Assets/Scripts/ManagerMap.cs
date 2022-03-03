using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using Mapbox.Unity;
using Mapbox.Geocoding;



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

    GameObject instance;

    // ForwardGeocodeResource _resource = new ForwardGeocodeResource("");

    void OnEnable()
    {
        Debug.Log("PrintOnEnable: script was enabled");
        location = Global.GetLocationEventMap().GetLatLonString();
        EventModel eventModel = Global.GetEventMap();
        _markerPrefab.transform.Find("TextMesh").GetComponent<TextMesh>().text = eventModel.name;
        instance = Instantiate(_markerPrefab);
        _map.Options.locationOptions.latitudeLongitude = location;
        print(location);
        _location = Conversions.StringToLatLon(location);

        instance.transform.localPosition = _map.GeoToWorldPosition(_location, true);
        instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
    }


    // Update is called once per frame
    private void Update()
    {
        instance.transform.localPosition = _map.GeoToWorldPosition(_location, true);
        instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
    }


}
