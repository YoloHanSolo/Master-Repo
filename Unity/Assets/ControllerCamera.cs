using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControllerCamera : MonoBehaviour
{
    Camera camera_default;
    Camera camera_hands;
    Camera camera_face;

    int button_select_index = 0;
    Button[] perspectives;

    Button button_perspective_default;
    Button button_perspective_hands;
    Button button_perspective_face;

    void Start()
    {
        button_perspective_default = GameObject.Find("Button_PerspectiveDefault").GetComponent<Button>();
        button_perspective_default.onClick.AddListener(HandlerDefault);  
        button_perspective_default.Select();

        button_perspective_hands = GameObject.Find("Button_PerspectiveHands").GetComponent<Button>();
        button_perspective_hands.onClick.AddListener(HandlerHands);

        button_perspective_face = GameObject.Find("Button_PerspectiveFace").GetComponent<Button>();
        button_perspective_face.onClick.AddListener(HandlerFace);

        perspectives = new Button[] {
            button_perspective_default,
            button_perspective_hands,
            button_perspective_face
        };

        camera_default = GameObject.Find("CameraDefault").GetComponent<Camera>();
        camera_hands = GameObject.Find("CameraHands").GetComponent<Camera>();
        camera_face = GameObject.Find("CameraFace").GetComponent<Camera>();

        camera_default.gameObject.SetActive(true);
        camera_hands.gameObject.SetActive(false);
        camera_face.gameObject.SetActive(false);
    }

    void Update() {

    }

    void HandlerDefault() {
        if (button_select_index != 0) {
            button_select_index = 0;
            perspectives[button_select_index].Select();
            
            camera_default.gameObject.SetActive(true);
            camera_hands.gameObject.SetActive(false);
            camera_face.gameObject.SetActive(false);
        }
    }

    void HandlerHands() {
        if (button_select_index != 1) {
            button_select_index = 1;
            perspectives[button_select_index].Select();

            camera_default.gameObject.SetActive(false);
            camera_hands.gameObject.SetActive(true);
            camera_face.gameObject.SetActive(false);
        }
    }

    void HandlerFace() {
        if (button_select_index != 2) {
            button_select_index = 2;
            perspectives[button_select_index].Select();

            camera_default.gameObject.SetActive(false);
            camera_hands.gameObject.SetActive(false);
            camera_face.gameObject.SetActive(true);
        }
    }

}