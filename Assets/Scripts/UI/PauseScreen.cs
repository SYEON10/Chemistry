using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private Slider bgmScrollBar;
    [SerializeField] private Slider sfxScrollBar;

    public void ChangeBGM()
    {
        SoundManager.Instance.ChangeBGMVolume(bgmScrollBar.value);
    }

    public void ChangeSFX()
    {
        SoundManager.Instance.ChangeSFXVolume(sfxScrollBar.value);
    }
}
