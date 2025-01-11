using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInput : MonoBehaviour
{
    public Screen pause;
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            pause.ShowScreen();
        }
    }
}
