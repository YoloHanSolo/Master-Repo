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

    ColorBlock color_active;
    ColorBlock color_disabled;

    void Start()
    {
        button_perspective_default = GameObject.Find("Button_PerspectiveDefault").GetComponent<Button>();
        button_perspective_default.onClick.AddListener(Handle_Button_Default);  
        button_perspective_default.Select();

        button_perspective_hands = GameObject.Find("Button_PerspectiveHands").GetComponent<Button>();
        button_perspective_hands.onClick.AddListener(Handle_Button_Hands);

        button_perspective_face = GameObject.Find("Button_PerspectiveFace").GetComponent<Button>();
        button_perspective_face.onClick.AddListener(Handle_Button_Face);

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


        color_active.disabledColor = new Color(1, 1, 1, 1);
        color_active.highlightedColor = new Color(1, 1, 1, 1);
        color_active.normalColor = new Color(1, 1, 1, 1);
        color_active.pressedColor = new Color(1, 1, 1, 1);
        color_active.selectedColor = new Color(1, 1, 1, 1);
        color_active.colorMultiplier = 1.0f;

        color_disabled.disabledColor = new Color(1, 1, 1, 1);
        color_disabled.highlightedColor = new Color(1, 1, 1, 1);
        color_disabled.normalColor = new Color(0.5f, 0.5f, 0.5f, 1);
        color_disabled.pressedColor = new Color(1, 1, 1, 1);
        color_disabled.selectedColor = new Color(1, 1, 1, 1);
        color_disabled.colorMultiplier = 1.0f;

        button_perspective_hands.colors = color_disabled;
        button_perspective_face.colors = color_disabled;
    }

    void Update() {

    }

    void Handle_Button_Default() {
        if (button_select_index != 0) {

            button_select_index = 0;
            perspectives[button_select_index].Select();

            button_perspective_default.colors = color_active;
            button_perspective_hands.colors = color_disabled;
            button_perspective_face.colors = color_disabled;

            camera_default.gameObject.SetActive(true);
            camera_hands.gameObject.SetActive(false);
            camera_face.gameObject.SetActive(false);
        }
    }

    void Handle_Button_Hands() {
        if (button_select_index != 1) {

            button_select_index = 1;
            perspectives[button_select_index].Select();

            button_perspective_default.colors = color_disabled;
            button_perspective_hands.colors = color_active;
            button_perspective_face.colors = color_disabled;

            camera_default.gameObject.SetActive(false);
            camera_hands.gameObject.SetActive(true);
            camera_face.gameObject.SetActive(false);
        }
    }

    void Handle_Button_Face() {
        if (button_select_index != 2) {

            button_select_index = 2;
            perspectives[button_select_index].Select();

            button_perspective_default.colors = color_disabled;
            button_perspective_hands.colors = color_disabled;
            button_perspective_face.colors = color_active;

            camera_default.gameObject.SetActive(false);
            camera_hands.gameObject.SetActive(false);
            camera_face.gameObject.SetActive(true);
        }
    }

}