using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DisablePanel : MonoBehaviour
{
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject panel2;

    [YarnCommand("HidePanels")]
    public void HidePanels()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
    }
}
