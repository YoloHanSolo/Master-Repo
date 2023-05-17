using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimatorController : MonoBehaviour
{

    Button button_play;
    Button button_stop;
    Button button_pause;

    Slider slider_animation_speed;
    Slider slider_animation_elapsed;

    Animator animator;
    AnimatorStateInfo animator_state_info;

    void Start()
    {
        button_play = GameObject.Find("Button_Play").GetComponent<Button>();
        button_play.onClick.AddListener(PlayHandler);  
        button_stop = GameObject.Find("Button_Stop").GetComponent<Button>();
        button_stop.onClick.AddListener(StopHandler);
        button_pause = GameObject.Find("Button_Pause").GetComponent<Button>();
        button_pause.onClick.AddListener(PauseHandler);

        slider_animation_speed = GameObject.Find("Slider_AnimationSpeed").GetComponent<Slider>();
        slider_animation_speed.onValueChanged.AddListener(delegate {AnimatorSpeed(); });

        slider_animation_elapsed = GameObject.Find("Slider_AnimationElapsed").GetComponent<Slider>();

        animator = GameObject.Find("SampleCharacter").transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        animator_state_info = animator.GetCurrentAnimatorStateInfo(0);
        if (animator_state_info.normalizedTime > 0.0f && animator_state_info.normalizedTime < 1.0f) {
            slider_animation_elapsed.value = animator_state_info.normalizedTime;
        }
    }

    void StopHandler() {
        animator.Play(animator_state_info.fullPathHash, 0, 0.0f);
        animator.speed = 0.0f;
    }

    void PauseHandler() {
        animator.speed = 0.0f;
    }

    void PlayHandler() {
        animator.speed = 1.0f;
    }

    void AnimatorSpeed() {
    }

}