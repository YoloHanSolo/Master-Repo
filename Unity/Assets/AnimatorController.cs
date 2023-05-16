using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimatorController : MonoBehaviour
{

    bool playing = true;
    float speed = 1.0f;


    float elapsed_time_normalized = 0.0f;

    Button b_Play;
    Button b_Stop;
    Button b_Reset;

    Slider slider_animation_speed;
    Slider slider_animation_elapsed;

    Animator m_Animator;
    AnimatorStateInfo animator_state_info;

    void Start()
    {

        // ANIMATOR CONTROLS

        b_Play = GameObject.Find("Button_Play").GetComponent<Button>();
        b_Play.onClick.AddListener(AnimatorPlay);
        
        b_Stop = GameObject.Find("Button_Stop").GetComponent<Button>();
        b_Stop.onClick.AddListener(AnimatorStop);

        b_Reset = GameObject.Find("Button_Reset").GetComponent<Button>();
        b_Reset.onClick.AddListener(AnimatorReset);

        slider_animation_speed = GameObject.Find("Slider_AnimationSpeed").GetComponent<Slider>();
        slider_animation_speed.onValueChanged.AddListener(delegate {AnimatorSpeed(); });

        slider_animation_elapsed = GameObject.Find("Slider_AnimationElapsed").GetComponent<Slider>();

        // ANIMATOR

        m_Animator = GameObject.Find("SampleCharacter").transform.GetChild(0).gameObject.GetComponent<Animator>();
        animator_state_info = m_Animator.GetCurrentAnimatorStateInfo(0);

    }

    void Update()
    {
        if (playing) {
            m_Animator.speed = speed;   
        }
        animator_state_info = m_Animator.GetCurrentAnimatorStateInfo(0);
        if (animator_state_info.normalizedTime > 0.0f && animator_state_info.normalizedTime < 1.0f) {
            slider_animation_elapsed.value = animator_state_info.normalizedTime;
        }
    }

    void AnimatorReset() {
        Debug.Log("AnimatorReset");
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
        speed = slider_animation_speed.value * 10.0f;
    }

    void Slider_AnimationElapsed_Event() {
    }

}
