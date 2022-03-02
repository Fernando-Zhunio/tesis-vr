using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PrefabNotification : MonoBehaviour
{
    public TextMeshProUGUI txt_title;
    public TextMeshProUGUI txt_description;
    public RawImage image;
    public GameObject gameObject;
    public void setTitle(string title)
    {
        txt_title.text = title;
    }

    public void setDescription(string description)
    {
        txt_description.text = description;
    }

    public void setImage(Sprite image)
    {
        this.image.texture = image.texture;
    }
}
