using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Michsky.LSS;
public class ManagerScene : MonoBehaviour
{
    public LoadingScreenManager lsm; // Your LSM variable

    public void Awake()
    {
        //lsm.LoadScene("Your Scene Name"); // Load a new scene via LSM

        // Alternate loading
        if (Global.getSession() == null)
        {
            //LoadingScreen.prefabName = "Playful"; // Change the preferred prefab using the class
            //LoadingScreen.LoadScene("Auth");
            //LoadSceneAuth();
            lsm.LoadScene("Auth");
        }
        else
        {
            //LoadSceneHome();
            //LoadingScreen.prefabName = "Playful"; // Change the preferred prefab using the class
            //LoadingScreen.LoadScene("Home");
            lsm.LoadScene("Home"); 

        }
    }

    public static void LoadSceneVr()
    {
        SceneManager.LoadScene("VR", LoadSceneMode.Additive);
    }

    public static void LoadSceneAuth()
    {
        SceneManager.LoadScene("Auth", LoadSceneMode.Single);

    }

    public static void LoadSceneHome()
    {
        SceneManager.LoadScene("Home");
    }
}
