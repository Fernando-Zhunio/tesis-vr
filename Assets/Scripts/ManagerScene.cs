using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManagerScene : MonoBehaviour
{



    public static IEnumerator  LoadSceneVr()
    {
        // async = SceneManager.LoadSceneAsync("AR2", LoadSceneMode.Additive);
        const string name = "AR3";
        AsyncOperation _async = new AsyncOperation();
        _async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        _async.allowSceneActivation = true;
 
        while (!_async.isDone) {
            yield return null;
        }
 
        Scene nextScene = SceneManager.GetSceneByName( name );
        if (nextScene.IsValid ()) {
            SceneManager.SetActiveScene (nextScene);
        }

    }

    public static void LoadSceneAuth()
    {
        SceneManager.LoadScene("Auth", LoadSceneMode.Single);

    }

    public static void LoadSceneHome()
    {
        SceneManager.LoadScene("Home");
    }

    // public static void closeSceneAr()
    // {
    //     SceneManager.UnloadSceneAsync("AR");
    // }
}
