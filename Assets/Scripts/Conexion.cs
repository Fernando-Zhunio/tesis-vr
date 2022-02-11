using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Conexion : MonoBehaviour
{
    UnityWebRequest www;
    public abstract void getResponse<T>(T data);

    

    public virtual void setHaderRequest(Dictionary<string, string> headers = null, bool isDefaultHeaders = true)
    {
        if (isDefaultHeaders)
        {
            www.SetRequestHeader("Accept", "application/json");
            if (Global.IsAuthenticated())
            {
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
         www = UnityWebRequest.Get(Enviroment.url+ uri);
        setHaderRequest();
        yield return www.SendWebRequest();
        Response(www);
    }

    protected IEnumerator CallPostBackend(string uri,WWWForm form = null)
    {
        print(Enviroment.url + uri);
         www = UnityWebRequest.Post(Enviroment.url+uri, form);
        setHaderRequest();
        yield return www.SendWebRequest();
        Response(www);
    }

    protected IEnumerator CallPutBackend(string uri,WWWForm form = null)
    {
         www = UnityWebRequest.Put(Enviroment.url+uri, form?.data);
        setHaderRequest();
        yield return www.SendWebRequest();
        Response(www);
    }

    protected IEnumerator CallDeleteBackend(string uri)
    {
         www = UnityWebRequest.Delete(Enviroment.url+uri);
        setHaderRequest();
        yield return www.SendWebRequest();
        Response(www);
    }

    private void Response(UnityWebRequest www)
    {
        if (www.result != UnityWebRequest.Result.Success)
        {

            ShowValidationErrors(www);
        }
        else
        {
            getResponse(www.downloadHandler.text);
        }
        HideLoading();
    }

    protected virtual void HideLoading() { }

    private void ShowValidationErrors(UnityWebRequest www)
    {
        Debug.Log(www.error);
        if (www.responseCode == 422)
        {
            ValidationErrorsModel validationErrorsModel = ValidationErrorsModel.CreateFromJSON(www.downloadHandler.text);
            Debug.Log(validationErrorsModel.errors);

            Global.ShowAndroidToastMessage("Error de permisos",validationErrorsModel.errors, NotificationType.danger);
            return;
        }
        Global.ShowAndroidToastMessage("Error", "Ups! Ocurrio un problema en el servidor, vuelve a intentarlo", NotificationType.danger);
    }

}
