using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPages : MonoBehaviour
{
    public GameObject pageHome, pageProfile, pageMap, pageEvents;
    [Header("Gasmeobjects Headers")]
    public GameObject headerHome, headerProfile, headerMap, headerEvents;

    private Dictionary<string, GameObject> currentPage = new Dictionary<string, GameObject>();

    private Dictionary<string, GameObject> pagesDictionary = new Dictionary<string,GameObject>();

    private void Start()
    {
        pagesDictionary.Add("Home", pageHome);
        pagesDictionary.Add("Profile", pageProfile);
        pagesDictionary.Add("Map", pageMap);
        pagesDictionary.Add("Event", pageEvents);
        currentPage.Add("CurrentPage", pageHome);
    }

    public void changedPage(string latestPage = "Home")
    {
        if (currentPage["CurrentPage"] == pagesDictionary[latestPage])
        {
            return;
        } else
        {
            pagesDictionary[latestPage].SetActive(true);
            currentPage["CurrentPage"].SetActive(false);
        }
    }
}
