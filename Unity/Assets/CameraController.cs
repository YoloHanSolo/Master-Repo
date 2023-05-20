using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    Camera mainCamera;
    RectTransform lynn;
    RectTransform transform_canvas;

    public float speed = 5.0f;
    public float sensitivity = 5.0f;

    Vector3 center;

    void Start()
    {
        transform_canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        lynn = GameObject.Find("Lynn").GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        if (Input.GetMouseButton(0) &&
            mouse.x < transform_canvas.sizeDelta.x / 2 && 
            mouse.y > transform_canvas.sizeDelta.y / 2 && 
            mouse.y < transform_canvas.sizeDelta.y) {
            mainCamera.transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y") * sensitivity, Input.GetAxis("Mouse X") * sensitivity, 0);
        }

    }

}