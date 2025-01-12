using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayBGM("Ingame");
    }
}
