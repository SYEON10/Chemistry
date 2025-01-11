using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    
    public void SpinCoin()
    {
        Debug.Log("Spinning coin...");
    }
}
