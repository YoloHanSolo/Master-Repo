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
    public Sprite sprite_mimic_yes;
    public Sprite sprite_mimic_no;


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
    GameObject avatar;
    AnimatorStateInfo animator_state_info;

    int current_term_index = 0; 

    private void EventHandler(BaseEventData eventData) {
        animator.Play(animator_state_info.fullPathHash, 0, slider_animation_elapsed.value);
        animator.speed = 0.0f;

        playing = false;
        button_playPause.image.sprite = sprite_play;
    }

    void Start()
    {
        avatar = GameObject.Find("Avatar");
        animator = avatar.GetComponent<Animator>();

        term = GameObject.Find("Dictionary_Term").GetComponent<TextMeshProUGUI>();
        definition = GameObject.Find("Dictionary_Definition").GetComponent<TextMeshProUGUI>();

        initSliderAnimationElapsed();
        initSliderAnimationSpeed();
        initButtons();
        initDictionary();

        mimic = true;
        playing = true;
    }

    void Update()
    {
        animator_state_info = animator.GetCurrentAnimatorStateInfo(0);

        if (playing) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                Animation_Pause_Playing();
            }
            if (animator_state_info.normalizedTime >= 0.0f && animator_state_info.normalizedTime <= 1.0f) {
                slider_animation_elapsed.value = animator_state_info.normalizedTime;
            }
            if (animator_state_info.normalizedTime > 1.0f) {
                slider_animation_elapsed.value = 1.0f;
                animator.Play(animator_state_info.fullPathHash, 0, 0.0f);
                Animation_Stop_Playing();
            }
        } else {
            if (Input.GetKeyDown(KeyCode.Space)) {
                Animation_Start_Playing();
            }
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            Handle_Button_Mirror();
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            Handle_Button_Mimic();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Handle_Button_Prev();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            Handle_Button_Next();
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
        if (avatar.transform.localScale.x == 1) {
            avatar.transform.localScale = new Vector3(-1, 1, 1);
            button_mirror.image.sprite = sprite_mirror_2;
        } else {
            avatar.transform.localScale = new Vector3(1, 1, 1);
            button_mirror.image.sprite = sprite_mirror_1;
        }
    }

    void Handle_Button_Mimic() {
        mimic = !mimic;
        animator.SetBool("MimicBool", mimic);
        if (mimic) {
            button_mimic.image.sprite = sprite_mimic_yes;
        } else {
            button_mimic.image.sprite = sprite_mimic_no;
        }
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
