using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Routes : MonoBehaviour
{
    public string getEventsUg = "events";

    public static string login = "auth/login";
    public static string register = "auth/signup";
    public static string events = "events";

    public static string eventfavorite(int id)  { return "events/" + id + "/favorite"; }

    public static string getFavorites = "events/favorites";

    public static string eventWaypoints(int id) { return "events/" + id + "/waypoints"; }
    public static string home = "home";

    public static string eventLikeOrUnlike(int id) { return $"events/{id}/like-unlike"; }
}
