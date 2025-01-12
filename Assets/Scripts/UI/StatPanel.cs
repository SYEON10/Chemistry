using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mov;
    [SerializeField] private TextMeshProUGUI charm;
    [SerializeField] private TextMeshProUGUI mental;

    void Awake()
    {
        GameManager.Instance.InitData();
    }
    
    void Start()
    {
        UpdateMov();
        UpdateCharm();
        UpdateMental();
        GameManager.Instance.data.stats.mov.StatChanged -= UpdateMov;
        GameManager.Instance.data.stats.mov.StatChanged += UpdateMov;
        GameManager.Instance.data.stats.charm.StatChanged -= UpdateCharm;
        GameManager.Instance.data.stats.charm.StatChanged += UpdateCharm;
        GameManager.Instance.data.stats.mental.StatChanged -= UpdateMental;
        GameManager.Instance.data.stats.mental.StatChanged += UpdateMental;
    }

    private void UpdateMov()
    {
        mov.text = GameManager.Instance.data.stats.mov.value.ToString();
    }

    private void UpdateCharm()
    {
        charm.text = GameManager.Instance.data.stats.charm.value.ToString();
    }

    private void UpdateMental()
    {
        mental.text = GameManager.Instance.data.stats.mental.value.ToString();
    }
}
