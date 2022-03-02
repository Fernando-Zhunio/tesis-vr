using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Examples;


public class ManagerPages : MonoBehaviour
{
    [SerializeField]
    [Header("-- Gameobjects and Headers --")]
    public GameObject MapContainer;
    public Image backgroundImage;
    public List<ItemPage> pages;
    private ItemPage currentPage = new ItemPage();
    public Dictionary<string, ItemPage> pagesDictionary = new Dictionary<string, ItemPage>();
    private void Awake()
    {
        MapContainer.SetActive(false);
        int size = pages.Count;
        for (int i = 0; i < size; i++)
        {
            pages[i].gameObject.SetActive(false);
            pages[i].image.color = Color.white;
            pages[i].name = pages[i].gameObject.name;
            pagesDictionary.Add(pages[i].gameObject.name, pages[i]);
        }
        currentPage.gameObject = pagesDictionary["Home"].gameObject;
        currentPage.image = pagesDictionary["Home"].image;
        currentPage.name = "Home";
        currentPage.gameObject.SetActive(true);
        currentPage.image.color = Color.yellow;
    }

    public void changedPage(string latestPage = "Home")
    {
        if (currentPage.name == latestPage)
        {
            return;
        }
        else
        {
            pagesDictionary[latestPage].gameObject.SetActive(true);
            pagesDictionary[latestPage].image.color = Color.yellow;

            print(latestPage);
            if (latestPage == "Map")
            {
                print(Global.GetMainCoordinate());
                if (!string.IsNullOrEmpty(Global.GetMainCoordinate()) )
                {
                    print("Mapa");
                    string coordinate = Global.GetMainCoordinate();
                    EnableMap(coordinate);
                } else {
                    NotificationController.ShowToast("No hay eventos por el momento");
                }
            }
            if (currentPage.name == "Map")
            {
                DisableMap();
            }
        currentPage.gameObject.SetActive(false);
        currentPage.image.color = Color.white;

        currentPage = pagesDictionary[latestPage];
    }
}

public void EnableMap(string coordinate)
{
    backgroundImage.enabled = false;
    MapContainer.SetActive(true);
}

private void DisableMap()
{
    backgroundImage.enabled = true;
    MapContainer.SetActive(false);
}

}


