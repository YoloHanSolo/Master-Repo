using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    Camera mainCamera;

    public float speed = 5.0f;
    public float sensitivity = 5.0f;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        mainCamera.transform.position += transform.forward * Input.GetAxis("Mouse X") * speed * Time.deltaTime;
        mainCamera.transform.position += transform.right * Input.GetAxis("Mouse Y") * speed * Time.deltaTime;     
        //mainCamera.transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y") * sensitivity, Input.GetAxis("Mouse X") * sensitivity, 0);
    }

}