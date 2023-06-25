using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundResponsive : MonoBehaviour
{
    RectTransform transform_canvas;
    RectTransform transform_element;
    
    void Start()
    {
        transform_canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        transform_element = gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        transform_element.position = new Vector3(0.0f, 0.0f, 0.0f);
        transform_element.sizeDelta = new Vector2(transform_canvas.sizeDelta.x, transform_canvas.sizeDelta.y);
    }

}