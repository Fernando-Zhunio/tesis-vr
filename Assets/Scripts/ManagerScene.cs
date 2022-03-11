using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManagerScene : MonoBehaviour
{
    

    public static void LoadSceneVr()
    {
        SceneManager.LoadScene("AR", LoadSceneMode.Additive);
    }

    public static void LoadSceneAuth()
    {
        SceneManager.LoadScene("Auth", LoadSceneMode.Single);

    }

    public static void LoadSceneHome()
    {
        SceneManager.LoadScene("Home");
    }

    public static void closeSceneAr() {
        SceneManager.UnloadSceneAsync("AR");
    }
}
