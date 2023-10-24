using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ControllerRotate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public float rotation_sensitivity = 50.0f;

    public GameObject lynn;

    Vector3 mouse_now;
    Vector3 mouse_prev;
    float mouse_delta_x;

    bool mouse_over = false;

    void Start()
    {   
        //lynn = GameObject.Find("Lynn");
    }

    void Update() {
        mouse_now = Input.mousePosition;
        if (mouse_prev != null) {
            mouse_delta_x = mouse_now.x - mouse_prev.x;
        }
        mouse_prev = Input.mousePosition;

        if (mouse_over) {
            if (Input.GetMouseButton(0)) {
                lynn.transform.eulerAngles += new Vector3(0, -mouse_delta_x * rotation_sensitivity * Time.deltaTime, 0);

                if (lynn.transform.rotation[1] > 0.83) {
                    if (lynn.transform.rotation[3] > 0 ) {
                        lynn.transform.eulerAngles = new Vector3(0, 110, 0);
                    } else {
                        lynn.transform.eulerAngles = new Vector3(0, -110, 0);
                    }
                }
            }
            if (Input.GetMouseButton(1)) {
                lynn.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        mouse_over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        mouse_over = false;
    }
}
