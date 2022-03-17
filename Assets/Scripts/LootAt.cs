using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootAt : MonoBehaviour
{
    [SerializeField]
    Transform my_transform;

    // Update is called once per frame

    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(my_transform);

        // Same as above, but setting the worldUp parameter to Vector3.left in this example turns the camera on its side
        transform.LookAt(my_transform, Vector3.left);
    }

    public void setLookTransform(Transform transform)
    {
        my_transform = transform;
    }

}
