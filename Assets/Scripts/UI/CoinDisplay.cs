using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;
using Random = System.Random;

public class CoinDisplay : MonoBehaviour
{
    private const string IconPath = "StatIcons/";
    [SerializeField] private TextMeshProUGUI choice;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI percent;
    [SerializeField] private Image icon1;
    [SerializeField] private Image icon2;
    [SerializeField] private TextMeshProUGUI ratio1;
    [SerializeField] private TextMeshProUGUI ratio2;
    [SerializeField] private GameObject panel;

    private void SetRaw()
    {
        choice.text = GameManager.Instance.data.beforeChoice;
        percent.text = "0%";
        icon1.color = Color.clear;
        icon2.color = Color.clear;
        ratio1.text = "";
        ratio2.text = "";
    }
    public void ShowBeforeSpin(int percent, string statName)
    {
        if (percent == 0)
        {
            SetRaw();
            return;
        }
        resultPanel.SetActive(false);
        choice.text = GameManager.Instance.data.beforeChoice;
        icon1.sprite = Resources.Load<Sprite>(IconPath + statName);
        icon1.color = Color.white;
        this.ratio1.text = "x1";
        ratio2.text = "";
        icon2.color = Color.clear;
        this.percent.text = $"{percent}%";
    }
    public void ShowBeforeSpin(int percent, int ratio1, string statName1, int ratio2, string statName2)
    {
        if (percent == 0)
        {
            SetRaw();
            return;
        }
        resultPanel.SetActive(false);
        choice.text = GameManager.Instance.data.beforeChoice;
        icon1.sprite = Resources.Load<Sprite>(IconPath + statName1);
        icon2.sprite = Resources.Load<Sprite>(IconPath + statName2);
        icon1.color = Color.white;
        icon2.color = Color.white;
        this.ratio1.text = $"x{ratio1}";
        this.ratio2.text = $"x{ratio2}";
        this.percent.text = $"{percent}%";
    }

    public bool SpinCoin(string statName)
    {
        panel.SetActive(true);
        Debug.Log("Spinning coin...");
        Stat stat = GameManager.Instance.data.GetStat(statName);
        Random random = new Random();
        ShowBeforeSpin(stat.value, statName);
        return random.Next(1, 100) <= stat.value;
    }

    public bool SpinCoin(int ratio1, string statName1, int ratio2, string statName2)
    {
        panel.SetActive(true);
        Debug.Log("Spinning coin...");
        Stat stat1 = GameManager.Instance.data.GetStat(statName1);
        Stat stat2 = GameManager.Instance.data.GetStat(statName2);
        int value;
        if (ratio1 + ratio2 != 0) value = (stat1.value * ratio1 + stat2.value * ratio2) / (ratio1 + ratio2);
        else value = 0;
        Random random = new Random();
        ShowBeforeSpin(value, ratio1, statName1, ratio2, statName2);
        return random.Next(1, 100) <= value;
    }
}
