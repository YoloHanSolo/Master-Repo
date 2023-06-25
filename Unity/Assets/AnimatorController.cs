using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class AnimatorController : MonoBehaviour
{
    bool playing;

    public Sprite sprite_play;
    public Sprite sprite_pause;

    Button button_playPause;
    Button button_stop;

    Slider slider_animation_speed;
    Slider slider_animation_elapsed;

    Animator animator;
    AnimatorStateInfo animator_state_info;

    private void EventHandler(BaseEventData eventData) {
        animator.Play(animator_state_info.fullPathHash, 0, slider_animation_elapsed.value);
        animator.speed = 0.0f;

        playing = false;
        button_playPause.image.sprite = sprite_play;
    }

    void Start()
    {
        EventTrigger eventTrigger = GameObject.Find("Slider_AnimationElapsed").AddComponent<EventTrigger>();
        EventTrigger.Entry clickEvent = new EventTrigger.Entry() {
            eventID = EventTriggerType.Drag
        };
        clickEvent.callback.AddListener(EventHandler);
        eventTrigger.triggers.Add(clickEvent);

        slider_animation_elapsed = GameObject.Find("Slider_AnimationElapsed").GetComponent<Slider>();
        animator = GameObject.Find("Lynn").GetComponent<Animator>();

        button_stop = GameObject.Find("Button_Stop").GetComponent<Button>();
        button_stop.onClick.AddListener(StopHandler);

        button_playPause = GameObject.Find("Button_PlayPause").GetComponent<Button>();
        button_playPause.onClick.AddListener(PlayPauseHandler);  

        slider_animation_speed = GameObject.Find("Slider_AnimationSpeed").GetComponent<Slider>();
        slider_animation_speed.value = 1.0f;
        slider_animation_speed.onValueChanged.AddListener(delegate {AnimatorSpeed(); });

        animator = GameObject.Find("SampleCharacter").transform.GetChild(0).gameObject.GetComponent<Animator>();
        
        playing = true;
        button_playPause.image.sprite = sprite_pause;   
    }

    void Update()
    {
        animator_state_info = animator.GetCurrentAnimatorStateInfo(0);

        if (playing) {
            // ANIMATION PLAYING
            if (animator_state_info.normalizedTime >= 0.0f && animator_state_info.normalizedTime <= 1.0f) {
                slider_animation_elapsed.value = animator_state_info.normalizedTime;
            }
            // ANIMATION OVER
            if (animator_state_info.normalizedTime > 1.0f) {
                slider_animation_elapsed.value = 1.0f;
                animator.Play(animator_state_info.fullPathHash, 0, 0.0f);
                animator.speed = 0.0f;

                playing = false;
                button_playPause.image.sprite = sprite_play;
            }

        } 
    }

    void StopHandler() {
        animator.Play(animator_state_info.fullPathHash, 0, 0.0f);
        slider_animation_elapsed.value = 0.0f;
        animator.speed = 0.0f;

        playing = false;
        button_playPause.image.sprite = sprite_play;
    }

    void PlayPauseHandler() {
        if (playing) {
            animator.speed = 0.0f;

            playing = false;
            button_playPause.image.sprite = sprite_play;

        } else {
            if (animator_state_info.normalizedTime > 0.99f) {
                
                animator_state_info = animator.GetCurrentAnimatorStateInfo(0);
                slider_animation_elapsed.value = 0.0f;
                Debug.Log(animator_state_info.normalizedTime);
            }
            animator.speed = slider_animation_speed.value;

            playing = true;
            button_playPause.image.sprite = sprite_pause;
        }
    }

    void AnimatorSpeed() {
        if (playing) {
            animator.speed = slider_animation_speed.value;
        }
    }

}