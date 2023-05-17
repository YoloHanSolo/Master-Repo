using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Slider_AnimationElapsed_Handler : MonoBehaviour
{

    Slider slider_animation_elapsed;

    Animator animator;
    AnimatorStateInfo animator_state_info;
    

    private void EventHandler(BaseEventData eventData) {
        animator.Play(animator_state_info.nameHash, 0, slider_animation_elapsed.value);
        animator.speed = 0.0f;
    }

    void Start() {
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry clickEvent = new EventTrigger.Entry() {
            eventID = EventTriggerType.Drag
        };
        clickEvent.callback.AddListener(EventHandler);
        eventTrigger.triggers.Add(clickEvent);

        slider_animation_elapsed = GameObject.Find("Slider_AnimationElapsed").GetComponent<Slider>();
        animator = GameObject.Find("Lynn").GetComponent<Animator>();
    }
    
    void Update()
    {
        animator_state_info = animator.GetCurrentAnimatorStateInfo(0);
    }

}