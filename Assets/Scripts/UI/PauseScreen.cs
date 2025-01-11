using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private Scrollbar bgmScrollBar;
    [SerializeField] private Scrollbar sfxScrollBar;

    public void ChangeBGM()
    {
        SoundManager.Instance.ChangeBGMVolume(bgmScrollBar.value);
    }

    public void ChangeSFX()
    {
        SoundManager.Instance.ChangeSFXVolume(sfxScrollBar.value);
    }
}
