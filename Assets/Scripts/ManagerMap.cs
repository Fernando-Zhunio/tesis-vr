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
    string location;
    Vector2d _location;

    GameObject instance;

    // ForwardGeocodeResource _resource = new ForwardGeocodeResource("");

    void OnEnable()
    {
        Debug.Log("PrintOnEnable: script was enabled");
        instance = Instantiate(_markerPrefab);
        _location = Conversions.StringToLatLon(location);

        instance.transform.localPosition = _map.GeoToWorldPosition(_location, true);
        instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);

        // string locations = Global.GetMainCoordinate();
        // _resource.Query = locations;
        // MapboxAccess.Instance.Geocoder.Geocode(_resource,)
    }

    public void SetLocation(string location)
    {
        _location = Conversions.StringToLatLon(location);
    }

    // Update is called once per frame
    private void Update()
    {
        instance.transform.localPosition = _map.GeoToWorldPosition(_location, true);
        instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);

    }

    void HandleGeocoderResponse(ForwardGeocodeResponse res)
		{
			// // _hasResponse = true;
			// if (null == res)
			// {
			// 	_inputField.text = "no geocode response";
			// }
			// else if (null != res.Features && res.Features.Count > 0)
			// {
			// 	var center = res.Features[0].Center;
			// 	_coordinate = res.Features[0].Center;
			// }
			// Response = res;
			// OnGeocoderResponse(res);
		}


}
