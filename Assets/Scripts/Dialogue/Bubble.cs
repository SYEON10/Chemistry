using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;

public class Bubble : MonoBehaviour
{
    [SerializeField] private PortraitDisplay portrait;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private DialogueRunner runner;
    
    void Start()
    {
        runner.onNodeStart.AddListener(MintVoice);
    }

    void MintVoice(string comment)
    {
        if (name.text != "민트") return;

        bool isIdle = false;
        foreach (Image image in portrait.images)
        {
            if (image.sprite.name == "Mint_Idle") isIdle = true;
        }

        if (!isIdle) return;

        if (text.text.Length > 2 && text.text[text.text.Length - 2] == '?')
        {
            SoundManager.Instance.PlaySFX("MintCurious");
        }
        else
        {
            System.Random random = new System.Random();
            int randomNumber = random.Next(1, 2);
            SoundManager.Instance.PlaySFX($"MintIdle{random}");
        }
    }
}
