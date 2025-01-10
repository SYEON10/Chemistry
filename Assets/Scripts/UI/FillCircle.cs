using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FillCircle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image circle;
    [SerializeField] private StatEnum statEnum;
    private Stat stat;

    public void Start()
    {
        stat = GameManager.Instance.data.GetStat(statEnum);
        UpdateView();
        stat.StatChanged -= UpdateView;
        stat.StatChanged += UpdateView;
    }

    public void UpdateView()
    {
        int value = stat.value;
        text.text = value.ToString();
        circle.fillAmount = value * 0.0075f;
        if(value >= 50) circle.color = Color.green;
        else circle.color = Color.red;
    }
}
