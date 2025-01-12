using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScreen : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayBGM("Ending");
    }
}
