using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ControllerRotate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    GameObject lynn;

    bool mouse_over = false;

    void Start()
    {   
        lynn = GameObject.Find("Lynn");
    }

    void Update() {        
        if (mouse_over) {
            if (Input.GetMouseButton(0)) {
                float new_ry = lynn.transform.rotation.y + 1;
                if (new_ry > 360) {
                    new_ry = 0;
                }

                lynn.transform.Rotate(0, new_ry, 0);
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
