using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float speed;
    void Start()
    {
    }
    
    void Update()
    {
        if (!Input.GetKey(KeyCode.RightControl))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            float X = transform.eulerAngles.x - Input.GetAxis("Mouse Y") * speed;
            float Y = transform.eulerAngles.y + Input.GetAxis("Mouse X") * speed;
            transform.eulerAngles = new Vector3(X, Y, 0f);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}