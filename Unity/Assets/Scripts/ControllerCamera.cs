using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControllerCamera : MonoBehaviour
{

    public Sprite sprite_view_default;
    public Sprite sprite_view_top;
    public Sprite sprite_view_face;

    Camera camera_default;
    Camera camera_top;
    Camera camera_face;

    Button button_perspective;

    GameObject avatar;

    int view_state = 0;

    void Start()
    {
        avatar = GameObject.Find("Avatar");

        button_perspective = GameObject.Find("Button_Perspective").GetComponent<Button>();
        button_perspective.onClick.AddListener(Handle_Button);  

        camera_default = GameObject.Find("CameraDefault").GetComponent<Camera>();
        camera_top = GameObject.Find("CameraTop").GetComponent<Camera>();
        camera_face = GameObject.Find("CameraFace").GetComponent<Camera>();

        camera_default.gameObject.SetActive(true);
        camera_top.gameObject.SetActive(false);
        camera_face.gameObject.SetActive(false);

        button_perspective.image.sprite = sprite_view_default;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            view_state = (view_state + 1) % 3;
            Handle_Button();
        } 
    }

    void Handle_Button() {
        if (view_state == 0) {
            view_state = 1;
            camera_default.gameObject.SetActive(false);
            camera_top.gameObject.SetActive(true);
            camera_face.gameObject.SetActive(false);
            avatar.transform.eulerAngles = new Vector3(0, 0, 0);
            button_perspective.image.sprite = sprite_view_top;
        } else if (view_state == 1) {
            view_state = 2;
            camera_default.gameObject.SetActive(false);
            camera_top.gameObject.SetActive(false);
            camera_face.gameObject.SetActive(true);
            avatar.transform.eulerAngles = new Vector3(0, 0, 0);
            button_perspective.image.sprite = sprite_view_face;
        } else if (view_state == 2) {
            view_state = 0;
            camera_default.gameObject.SetActive(true);
            camera_top.gameObject.SetActive(false);
            camera_face.gameObject.SetActive(false);
            avatar.transform.eulerAngles = new Vector3(0, 0, 0);
            button_perspective.image.sprite = sprite_view_default;
        }
    }

}