using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Conexion : MonoBehaviour
{
    UnityWebRequest www;
    public abstract void getResponse<T>(T data);

    public virtual void setHaderRequest(Dictionary<string, string> headers = null, bool isDefaultHeaders = true)
    {
        NotificationController.ShowToast("Cargando espere ...");
        if (isDefaultHeaders)
        {
            www.SetRequestHeader("Accept", "application/json");
            if (Global.IsAuthenticated())
            {
                print("token: " + Global.getToken());
                www.SetRequestHeader("Authorization", Global.getToken());
            }
        }
        if (headers != null)
        {
            foreach (KeyValuePair<string, string> entry in headers)
            {
                www.SetRequestHeader(entry.Key, entry.Value);
            }
        }
    }

    protected IEnumerator CallGetBackend(string uri)
    {
        print("CallGetBackend: " + uri);
        www = UnityWebRequest.Get(Enviroment.url + uri);
        setHaderRequest();
        yield return www.SendWebRequest();
        Response(www);
    }

    protected IEnumerator CallPostBackend(string uri, WWWForm form = null)
    {
        print(Enviroment.url + uri);
        www = UnityWebRequest.Post(Enviroment.url + uri, form);
        setHaderRequest();
        yield return www.SendWebRequest();
        Response(www);
    }



    protected IEnumerator CallPutBackend(string uri, WWWForm form = null)
    {
        www = UnityWebRequest.Put(Enviroment.url + uri, form?.data);
        setHaderRequest();
        yield return www.SendWebRequest();
        Response(www);
    }

    protected IEnumerator CallDeleteBackend(string uri)
    {
        www = UnityWebRequest.Delete(Enviroment.url + uri);
        setHaderRequest();
        yield return www.SendWebRequest();
        Response(www);
    }

    private void Response(UnityWebRequest www)
    {
        if (www.result != UnityWebRequest.Result.Success)
        {
            ValidationErrors(www);
            print("Error: " + www.error);

        }
        else
        {
            getResponse(www.downloadHandler.text);
        }
        HideLoading();
    }

    protected virtual void HideLoading() { }

    private void ValidationErrors(UnityWebRequest www)
    {
        Debug.Log(www.downloadHandler.text);
        if (www.responseCode == 401)
        {
            NotificationController.ShowToast("Usuario o contraseña incorrectos");
            Global.logout();
        }
        else if (www.responseCode == 422)
        {
            NotificationController.ShowToast(www.downloadHandler.text);
        }
        else if (www.responseCode == 500)
        {
            NotificationController.ShowToast("Error de servidor");
        }
        else if (www.responseCode == 404)
        {
            NotificationController.ShowToast("Recurso no encontrado");
        }
        // else if (www.responseCode == 200)
        // {
        //     NotificationController.ShowToast("Registro exitoso, inicie sesion");
        // }
    }


    protected IEnumerator CallGetBackend(string url, Action<string> callback, Action<string> callbackError)
    {

        www = UnityWebRequest.Get(url);
        setHaderRequest();
        yield return www.SendWebRequest();
        Response(www, callback, callbackError);
    }

    protected void HttpGet(string path, Action<string> callback, Action<string> callbackError = null)
    {
        string url = Enviroment.url + path;
        print("HttpGet: " + url);
        StartCoroutine(CallGetBackend(url, callback, callbackError));
    }

    protected IEnumerator CallPostBackend(string uri, WWWForm form, Action<string> callback, Action<string> callbackError = null)
    {
        print(Enviroment.url + uri);
        www = UnityWebRequest.Post(Enviroment.url + uri, form);
        setHaderRequest();
        yield return www.SendWebRequest();
        Response(www, callback, callbackError);
    }

    protected void HttpPost(string path, WWWForm form, Action<string> callback, Action<string> callbackError = null)
    {
        // string url = Enviroment.url + path;
        // print("HttpGet: " + url);
        StartCoroutine(CallPostBackend(path,form, callback, callbackError));
    }

    private void Response(UnityWebRequest www, Action<string> callback, Action<string> callbackError = null)
    {

        if (www.result != UnityWebRequest.Result.Success)
        {
            ValidationErrors(www);
            // ShowValidationErrors(www);
            print("Error: " + www.error);
            NotificationController.ShowToast(www.error);
            if (callbackError != null)
            {
                // callbackError(www.error);
            callbackError(www.downloadHandler.text);
            }

        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            callback(www.downloadHandler.text);
        }
        HideLoading();
    }
}
