using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HelperTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject helper;

    public void OnPointerEnter(PointerEventData eventData)
    {
        helper.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        helper.SetActive(false);
    }

}