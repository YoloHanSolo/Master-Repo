using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimatorController : MonoBehaviour
{

    bool playing = true;
    float speed = 1.0f;

    Button b_Play;
    Button b_Stop;
    Button b_Reset;
    Slider m_Slider;

    Animator m_Animator;

    void Start()
    {

        // ANIMATOR CONTROLS

        b_Play = GameObject.Find("Button_Play").GetComponent<Button>();
        b_Play.onClick.AddListener(AnimatorPlay);
        
        b_Stop = GameObject.Find("Button_Stop").GetComponent<Button>();
        b_Stop.onClick.AddListener(AnimatorStop);

        b_Reset = GameObject.Find("Button_Reset").GetComponent<Button>();
        b_Reset.onClick.AddListener(AnimatorReset);

        m_Slider = GameObject.Find("AnimationSpeedSlider").GetComponent<Slider>();
        m_Slider.onValueChanged.AddListener(delegate {AnimatorSpeed(); });

        // ANIMATOR

        m_Animator = GameObject.Find("SampleCharacter").transform.GetChild(0).gameObject.GetComponent<Animator>();

    }

    void Update()
    {
        if (playing) {
            m_Animator.speed = speed;   
        }
    }

    void AnimatorReset() {

        Debug.Log("HEHEHE");
    }

    void AnimatorStop() {
        playing = false;
        m_Animator.speed = 0.0f;
    }

    void AnimatorPlay() {
        playing = true;
        m_Animator.speed = speed;
    }

    void AnimatorSpeed() {
        speed = m_Slider.value * 10.0f;
    }

}
