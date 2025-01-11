using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn.Unity;

public class Option : MonoBehaviour
{
    private OptionView optionView;
    private TextMeshProUGUI text;
    
    void Awake()
    {
        optionView = GetComponent<OptionView>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        optionView.OnOptionSelected -= GetBeforeSelect;
        optionView.OnOptionSelected += GetBeforeSelect;
    }

    private void GetBeforeSelect(DialogueOption option)
    {
        GameManager.Instance.data.beforeChoice = text.text;
    }
}
