using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private const string touchClip = "Touch";
    private const string clickClip = "Click";

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySFX(touchClip);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySFX(clickClip);
    }
}
