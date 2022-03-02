//using ARLocation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using ARLocation;
//[RequireComponent(typeof(PlaceAtLocation))]
public class VrManager : Conexion
{

    //public CustomDialogController dialogController;
   

    void Start()
    {
        
        //string url = $"events/{Global.getEventId()}/waypoints?lng=-79.95392&lat=-2.1200896";
        //print(url);
        StartCoroutine(getPosition());
        //dialogController.OnResult.AddListener(ResultDialog);
        //ShowDialog("Test", "Description");
    }

    public override void getResponse<T>(T data)
    {
        Debug.Log("waypoints:" + data);
        double[][,] waypoints = JsonUtility.FromJson<double[][,]>(data.ToString());
        //var newLocation = new Location()
        //{
        //    Altitude = 10,
        //    AltitudeMode = AltitudeMode.GroundRelative
        //};
        //var placeAtLocation = GetComponent<PlaceAtLocation>();
        //placeAtLocation.Location = newLocation;
    }

    void setLocations()
    {
        
    }

    IEnumerator getPosition()
    {
        // Check if the user has location service enabled.
        print("dentro de getLocation de location");

        if (!Input.location.isEnabledByUser)
        {
            print("la localizacion esta desabilitada");
            //Global.ShowAndroidToastMessage("Error", "Ubicacion desabilitada", NotificationType.danger);
            ShowDialog("Error", "Ubicacion desabilitada");
            yield break;
        }

        // Starts the location service.
        Input.location.Start();

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            //print("esperando activacio de location");
            //Global.ShowAndroidToastMessage("Info", "Esperando activacion de ubicacion", NotificationType.info);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            //print("Timed out");
            //Global.ShowAndroidToastMessage("Error", "Ubicacion desabilitada", NotificationType.danger);
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            //Global.ShowAndroidToastMessage("Error", "Ubicacion desabilitada", NotificationType.danger);
            ShowDialog("Error", "Ubicacion desabilitada tiempo agotado");
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            //print();
            string message = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
            ShowDialog("Success", message);

            //string url = $"events/{Global.getEventId()}/waypoints?lng={Input.location.lastData.longitude}&lat={Input.location.lastData.latitude}";
            //StartCoroutine(CallGetBackend(url));
        }

        // Stops the location service if there is no need to query location updates continuously.
        Input.location.Stop();
    }


    public void ShowDialog(string title, string message)
    {
        //if(dialogController)
        //{
        //    dialogController.title = title;
        //    dialogController.message = message;
        //    dialogController.Show();
        //}
    }




}
