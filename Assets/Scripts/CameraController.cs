using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private Transform cameraP;

    // Start is called before the first frame update
    void Start()
    {   
        cameraP = transform.Find("Main Camera");
        Cursor.lockState = CursorLockMode.Locked;
        offset = transform.position - player.transform.position;   
    }

    // Update is called once per frame
    void LateUpdate()

    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");
        
        if (hor != 0)
        {
            transform.Rotate(Vector3.up * hor);
        }
        if (ver != 0)
        {
            //transform.Rotate(Vector3.left * ver);
            float angle = cameraP.localEulerAngles.x - ver;
            cameraP.localEulerAngles = Vector3.right * angle;
        }
        transform.position = player.transform.position + offset;
    }
}
