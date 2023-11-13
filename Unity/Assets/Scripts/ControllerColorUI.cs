using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class ControllerColorUI : MonoBehaviour
{

    public Color color_bg;
    public Color color_element;
    public Color color_btn;
    public Color color_slider_bg;
    public Color color_slider_fill;
    public Color color_bg_help;
    public Color color_text_help;

    public ColorBlock color_block; 

    Image background_CameraControls;
    Image background_Dictionary;
    Image background_PlayControls;
    Image background_DictionaryControls;
    Image background_OtherControls;
    Image background_SpeedControls;
    Image background_UI;


    Image background_HelpMirror;
    Image background_HelpMimic;
    Image background_HelpPerspective;
    Image background_HelpSpeed;

    TMP_Text text_HelpMirror;
    TMP_Text text_HelpMimic;
    TMP_Text text_HelpPerspective;
    TMP_Text text_HelpSpeed;

    Image slider_background_Play;
    Image slider_fill_Play;

    Image slider_background_Speed;
    Image slider_fill_Speed;

    Button button_perspective;
    Button button_mirror;
    Button button_mimic;
    Button button_play;
    Button button_stop;
    Button button_next;
    Button button_prev;

    void Start()
    {
        background_CameraControls = GameObject.Find("Background_CameraControls").GetComponent<Image>();
        background_Dictionary = GameObject.Find("Background_Dictionary").GetComponent<Image>();
        background_PlayControls = GameObject.Find("Background_PlayControls").GetComponent<Image>();
        background_DictionaryControls = GameObject.Find("Background_DictionaryControls").GetComponent<Image>();
        background_OtherControls = GameObject.Find("Background_OtherControls").GetComponent<Image>();
        background_SpeedControls = GameObject.Find("Background_SpeedControls").GetComponent<Image>();
        background_UI = GameObject.Find("Background_UI").GetComponent<Image>();

        background_HelpSpeed = GameObject.Find("Background_HelpSpeed").GetComponent<Image>();
        text_HelpSpeed = GameObject.Find("Text_HelpSpeed").GetComponent<TMP_Text>();

        background_HelpMimic = GameObject.Find("Background_HelpMimic").GetComponent<Image>();
        text_HelpMimic = GameObject.Find("Text_HelpMimic").GetComponent<TMP_Text>();

        background_HelpMirror = GameObject.Find("Background_HelpMirror").GetComponent<Image>();
        text_HelpMirror = GameObject.Find("Text_HelpMirror").GetComponent<TMP_Text>();

        background_HelpPerspective = GameObject.Find("Background_HelpPerspective").GetComponent<Image>();
        text_HelpPerspective = GameObject.Find("Text_HelpPerspective").GetComponent<TMP_Text>();

        slider_fill_Play = GameObject.Find("SliderPlayFill").GetComponent<Image>();
        slider_fill_Speed = GameObject.Find("SliderSpeedFill").GetComponent<Image>();

        slider_background_Play = GameObject.Find("Background_SliderSpeed").GetComponent<Image>();
        slider_background_Speed = GameObject.Find("Background_SliderPlay").GetComponent<Image>();

        button_perspective = GameObject.Find("Button_Perspective").GetComponent<Button>();
        button_mirror = GameObject.Find("Button_Mirror").GetComponent<Button>();
        button_mimic = GameObject.Find("Button_Mimic").GetComponent<Button>();
        button_play = GameObject.Find("Button_PlayPause").GetComponent<Button>();
        button_stop = GameObject.Find("Button_Stop").GetComponent<Button>();
        button_prev = GameObject.Find("Button_Prev").GetComponent<Button>();
        button_next = GameObject.Find("Button_Next").GetComponent<Button>();

        button_perspective.colors = color_block;
        button_mirror.colors = color_block;
        button_mimic.colors = color_block;
        button_next.colors = color_block;
        button_prev.colors = color_block;
        button_play.colors = color_block;
        button_stop.colors = color_block;

        background_CameraControls.color = color_element;
        background_Dictionary.color = color_element;
        background_PlayControls.color = color_element;
        background_DictionaryControls.color = color_element;
        background_OtherControls.color = color_element;
        background_SpeedControls.color = color_element;
        background_UI.color = color_bg;

        slider_fill_Play.color = color_slider_fill;
        slider_fill_Speed.color = color_slider_fill;

        slider_background_Play.color = color_slider_bg;
        slider_background_Speed.color = color_slider_bg;

        background_HelpPerspective.color = color_bg_help;
        text_HelpPerspective.color = color_text_help;

        background_HelpSpeed.color = color_bg_help;
        text_HelpSpeed.color = color_text_help;

        background_HelpMirror.color = color_bg_help;
        text_HelpMirror.color = color_text_help;

        background_HelpMimic.color = color_bg_help;
        text_HelpMimic.color = color_text_help;

        GameObject.Find("HelpSpeed").SetActive(false);
        GameObject.Find("HelpMirror").SetActive(false);
        GameObject.Find("HelpMimic").SetActive(false);
        GameObject.Find("HelpPerspective").SetActive(false);
    }

}
