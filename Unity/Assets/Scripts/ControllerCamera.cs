using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControllerCamera : MonoBehaviour
{

    public Sprite sprite_view_default;
    public Sprite sprite_view_face;

    Camera camera_default;
    Camera camera_face;

    Button button_perspective;

    GameObject avatar;

    bool view_default = true;

    void Start()
    {
        avatar = GameObject.Find("Avatar");

        button_perspective = GameObject.Find("Button_Perspective").GetComponent<Button>();
        button_perspective.onClick.AddListener(Handle_Button);  

        camera_default = GameObject.Find("CameraDefault").GetComponent<Camera>();
        camera_face = GameObject.Find("CameraFace").GetComponent<Camera>();

        camera_default.gameObject.SetActive(true);
        camera_face.gameObject.SetActive(false);
        button_perspective.image.sprite = sprite_view_face;
    }

    void Update() {

    }

    void Handle_Button() {
        if (view_default) {
            view_default = false;
            camera_default.gameObject.SetActive(false);
            camera_face.gameObject.SetActive(true);
            avatar.transform.eulerAngles = new Vector3(0, 0, 0);
            button_perspective.image.sprite = sprite_view_default;
        } else {
            view_default = true;
            camera_default.gameObject.SetActive(true);
            camera_face.gameObject.SetActive(false);
            avatar.transform.eulerAngles = new Vector3(0, 0, 0);
            button_perspective.image.sprite = sprite_view_face;
        }

    }

}