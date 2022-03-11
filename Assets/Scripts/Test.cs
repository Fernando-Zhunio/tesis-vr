using UnityEngine;
using System;

public class Test : MonoBehaviour
{
    private Location location_ug_center = new Location(-2.181452614962342, -79.89844529968079);

    public void goVrApproved()
    {
        double distance = DistanceTo(-2.1780861981138884, -79.90192144256308,location_ug_center.latitudDouble, location_ug_center.longitudDouble);
        if (distance > 500) {
            print("no estas en la universidad, distancia: " + distance);
        }

            // NotificationController.ShowToast("No se encuentra en el área de cobertura, distancia: " + distance + " metros");
            return;

    }
    public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
    {
        double rlat1 = Math.PI * lat1 / 180;
        double rlat2 = Math.PI * lat2 / 180;
        double theta = lon1 - lon2;
        double rtheta = Math.PI * theta / 180;
        double dist =
            Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
            Math.Cos(rlat2) * Math.Cos(rtheta);
        dist = Math.Acos(dist);
        dist = dist * 180 / Math.PI;
        dist = dist * 60 * 1.1515;
        dist = dist * 1.609344;
        dist = dist * 1000;
        dist = Convert.ToInt32(dist);

        return dist;
    }
}
