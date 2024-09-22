using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraControl : MonoBehaviour
{

    Vector2 rotation = Vector2.zero;
    public float speed = 3;
    public float yClamp = 30f;
    public float xClamp = 30f;

    void Update()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.y = Mathf.Clamp(rotation.y, - yClamp/2, yClamp/2);
        rotation.x = Mathf.Clamp(rotation.x, - xClamp*(3/4), xClamp/4);        
        transform.eulerAngles = (Vector2)rotation * speed;

    }
}
