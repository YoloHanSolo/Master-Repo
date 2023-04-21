using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimatorController : MonoBehaviour
{
    bool playing = true;
    float speed = 1.0f;

    Slider m_Slider;
    Animator m_Animator;
    Button b_Play;
    Button b_Stop;

    void Start()
    {
        m_Animator = GameObject.Find("Cube").GetComponent<Animator>();

        m_Slider = GameObject.Find("AnimationSpeedSlider").GetComponent<Slider>();
        m_Slider.onValueChanged.AddListener(delegate {AnimatorSpeed(); });

        b_Play = GameObject.Find("Button_Play").GetComponent<Button>();
        b_Play.onClick.AddListener(AnimatorPlay);
        
        b_Stop = GameObject.Find("Button_Stop").GetComponent<Button>();
        b_Stop.onClick.AddListener(AnimatorStop);
    }

    void Update()
    {
        if (playing) {
            m_Animator.speed = speed;   
        }
    }

    void AnimatorStop(){
        playing = false;
        m_Animator.speed = 0.0f;
    }

    void AnimatorPlay(){
        playing = true;
        m_Animator.speed = speed;
    }

    void AnimatorSpeed(){
        speed = m_Slider.value * 10.0f;
    }

}
