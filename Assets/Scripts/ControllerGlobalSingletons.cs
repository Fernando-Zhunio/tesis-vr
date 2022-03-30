using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARLocation;
public class ControllerGlobalSingletons : MonoBehaviour
{
    // Start is called before the first frame update
    public static ControllerGlobalSingletons Instance { get; private set; }

    public GameObject mainCamera;
    public Canvas canvasUIHome;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void ActiveVr(int id, Location location)
    {
        // GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        // GameObject canvasHome = GameObject.FindGameObjectWithTag("CanvasHome");

        canvasUIHome.gameObject.SetActive(false);
        mainCamera.SetActive(false);
        // Location location = new Location(lat, lng);
        Global.SetLocation(location);
        Global.setEventId(id);
       StartCoroutine(ManagerScene.LoadSceneVr());
    }

    public void DesactiveVr()
    {
        // GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        // GameObject canvasHome = GameObject.FindGameObjectWithTag("CanvasHome");
        if (ARLocationManager.Instance == null)
        {
            ARLocationManager.Instance.ResetARSession((() =>
{
    Debug.Log("AR+GPS and AR Session were restarted!");
}));
            // return;
        }
        canvasUIHome.gameObject.SetActive(true);
        mainCamera.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("AR2");

    }
}
