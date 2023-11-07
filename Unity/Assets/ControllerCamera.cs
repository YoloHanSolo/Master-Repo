using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControllerCamera : MonoBehaviour
{
    Camera camera_default;
    Camera camera_hands;
    Camera camera_face;

    Button button_perspective_default;
    Button button_perspective_hands;
    Button button_perspective_face;

    ColorBlock color_enabled;
    ColorBlock color_disabled;

    GameObject avatar;

    void Start()
    {
        button_perspective_default = GameObject.Find("Button_PerspectiveDefault").GetComponent<Button>();
        button_perspective_default.onClick.AddListener(Handle_Button_Default);  
        button_perspective_default.Select();

        button_perspective_hands = GameObject.Find("Button_PerspectiveHands").GetComponent<Button>();
        button_perspective_hands.onClick.AddListener(Handle_Button_Hands);

        button_perspective_face = GameObject.Find("Button_PerspectiveFace").GetComponent<Button>();
        button_perspective_face.onClick.AddListener(Handle_Button_Face);

        camera_default = GameObject.Find("CameraDefault").GetComponent<Camera>();
        camera_hands = GameObject.Find("CameraHands").GetComponent<Camera>();
        camera_face = GameObject.Find("CameraFace").GetComponent<Camera>();

        camera_default.gameObject.SetActive(true);
        camera_hands.gameObject.SetActive(false);
        camera_face.gameObject.SetActive(false);

        avatar = GameObject.Find("Avatar");
    }

    void Update() {

    }

    void Handle_Button_Default() {
        button_perspective_default.Select();
        camera_default.gameObject.SetActive(true);
        camera_hands.gameObject.SetActive(false);
        camera_face.gameObject.SetActive(false);
        avatar.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    void Handle_Button_Hands() {
        button_perspective_hands.Select();
        camera_default.gameObject.SetActive(false);
        camera_hands.gameObject.SetActive(true);
        camera_face.gameObject.SetActive(false);
        avatar.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    void Handle_Button_Face() {
        button_perspective_face.Select();
        camera_default.gameObject.SetActive(false);
        camera_hands.gameObject.SetActive(true);
        camera_face.gameObject.SetActive(true);
        avatar.transform.eulerAngles = new Vector3(0, 0, 0);
    }

}