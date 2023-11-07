using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


[System.Serializable]
public class DictionaryObject
{
    public string term;
    public string definition; 
}

[System.Serializable]
public class JsonRoot
{
    public DictionaryObject[] dictionary;
}


public class ControllerAnimation : MonoBehaviour
{
    bool playing;

    bool mimic;

    public Sprite sprite_play;
    public Sprite sprite_pause;
    public Sprite sprite_resume;
    public Sprite sprite_mirror_1;
    public Sprite sprite_mirror_2;

    Button button_playPause;
    Button button_stop;
    Button button_mirror;
    Button button_mimic;
    Button button_next;
    Button button_prev;

    Slider slider_animation_speed;
    Slider slider_animation_elapsed;

    TextAsset jsonFile;
    JsonRoot root;

    TMP_Text term;
    TMP_Text definition;

    Animator animator;
    public GameObject lynn;
    AnimatorStateInfo animator_state_info;

    int current_term_index = 0; 

    ColorBlock color;

    private void EventHandler(BaseEventData eventData) {
        animator.Play(animator_state_info.fullPathHash, 0, slider_animation_elapsed.value);
        animator.speed = 0.0f;

        playing = false;
        mimic = true;
        button_playPause.image.sprite = sprite_play;
    }

    void Start()
    {
        animator = lynn.GetComponent<Animator>();
        //animator = GameObject.Find("Lynn").GetComponent<Animator>();
        //lynn = GameObject.Find("Lynn");
        term = GameObject.Find("Dictionary_Term").GetComponent<TextMeshProUGUI>();
        definition = GameObject.Find("Dictionary_Definition").GetComponent<TextMeshProUGUI>();

        color.disabledColor = new Color(0.75f, 0.75f, 0.75f, 1);
        color.highlightedColor = new Color(1, 1, 1, 1);
        color.normalColor = new Color(0.75f, 0.75f, 0.75f, 1);
        color.pressedColor = new Color(0.75f, 0.75f, 0.75f, 1);
        color.selectedColor = new Color(0.75f, 0.75f, 0.75f, 1);
        color.colorMultiplier = 1.0f;

        initSliderAnimationElapsed();
        initSliderAnimationSpeed();
        initButtons();
        initDictionary();

        playing = true;
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
                Animation_Stop_Playing();
            }
        }

    }

    //////////
    // INIT //
    //////////

    void initSliderAnimationElapsed() {
        slider_animation_elapsed = GameObject.Find("Slider_AnimationElapsed").GetComponent<Slider>();

        EventTrigger eventTrigger = GameObject.Find("Slider_AnimationElapsed").AddComponent<EventTrigger>();
        EventTrigger.Entry clickEvent = new EventTrigger.Entry() {
            eventID = EventTriggerType.Drag
        };
        clickEvent.callback.AddListener(EventHandler);
        eventTrigger.triggers.Add(clickEvent);
    }

    void initSliderAnimationSpeed() {
        slider_animation_speed = GameObject.Find("Slider_AnimationSpeed").GetComponent<Slider>();
        slider_animation_speed.value = 1.0f;
        slider_animation_speed.onValueChanged.AddListener(
            delegate {AnimatorSpeed();}
        );
    }

    void initButtons() {
        button_stop = GameObject.Find("Button_Stop").GetComponent<Button>();
        button_stop.onClick.AddListener(StopHandler);

        button_playPause = GameObject.Find("Button_PlayPause").GetComponent<Button>();
        button_playPause.onClick.AddListener(PlayPauseHandler);  
        button_playPause.image.sprite = sprite_pause;    

        button_next = GameObject.Find("Button_Next").GetComponent<Button>();
        button_next.onClick.AddListener(Handle_Button_Next);

        button_prev = GameObject.Find("Button_Prev").GetComponent<Button>();
        button_prev.onClick.AddListener(Handle_Button_Prev);

        button_mirror = GameObject.Find("Button_Mirror").GetComponent<Button>();
        button_mirror.onClick.AddListener(Handle_Button_Mirror);

        button_mimic = GameObject.Find("Button_Mimic").GetComponent<Button>();
        button_mimic.onClick.AddListener(Handle_Button_Mimic);
        /*
        button_mirror.colors = color;
        button_mimic.colors = color;
        button_playPause.colors = color;
        button_stop.colors = color;
        button_prev.colors = color;
        button_next.colors = color;
        */
    }

    void initDictionary() {
        jsonFile = Resources.Load<TextAsset>("dictionary");
        root = JsonUtility.FromJson<JsonRoot>(jsonFile.text);
        term.SetText(root.dictionary[current_term_index].term);
        definition.SetText(root.dictionary[current_term_index].definition);
    }

    //////////////
    // HANDLERS //
    //////////////

    void StopHandler() {
        animator.Play(animator_state_info.fullPathHash, 0, 0.0f);
        slider_animation_elapsed.value = 0.0f;
        Animation_Stop_Playing();
    }

    void PlayPauseHandler() {
        if (playing) { 
            Animation_Pause_Playing();
        } else {
            if (animator_state_info.normalizedTime > 0.99f) {
                animator_state_info = animator.GetCurrentAnimatorStateInfo(0);
                slider_animation_elapsed.value = 0.0f;
            }
            Animation_Start_Playing();
        }
    }

    void AnimatorSpeed() {
        if (playing) {
            animator.speed = slider_animation_speed.value;
        }
    }

    void Handle_Button_Next() {
        current_term_index = (current_term_index + 1) % root.dictionary.Length;
        term.SetText(root.dictionary[current_term_index].term);
        definition.SetText(root.dictionary[current_term_index].definition);
        animator.SetInteger("AnimationCounter", current_term_index);
        Animation_Start_Playing();
    }

    void Handle_Button_Prev() {
        current_term_index = current_term_index - 1;
        if (current_term_index < 0) {
            current_term_index = root.dictionary.Length - 1;
        }     
        term.SetText(root.dictionary[current_term_index].term);
        definition.SetText(root.dictionary[current_term_index].definition);
        animator.SetInteger("AnimationCounter", current_term_index);
        Animation_Start_Playing();
    }

    void Handle_Button_Mirror() {
        if (lynn.transform.localScale.x == 1) {
            lynn.transform.localScale = new Vector3(-1, 1, 1);
            button_mirror.image.sprite = sprite_mirror_2;
        } else {
            lynn.transform.localScale = new Vector3(1, 1, 1);
            button_mirror.image.sprite = sprite_mirror_1;
        }
    }

    void Handle_Button_Mimic() {
        mimic = !mimic;
        animator.SetBool("MimicBool", mimic);
    }

    void Animation_Start_Playing() {
        playing = true;
        animator.speed = slider_animation_speed.value;
        button_playPause.image.sprite = sprite_pause;
    }

    void Animation_Pause_Playing() {
        playing = false;
        animator.speed = 0.0f;
        button_playPause.image.sprite = sprite_resume;
    }

    void Animation_Stop_Playing() {
        playing = false;
        animator.speed = 0.0f;
        button_playPause.image.sprite = sprite_play;
    }

}
