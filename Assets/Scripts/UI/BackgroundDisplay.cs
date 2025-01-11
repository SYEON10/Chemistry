using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class BackgroundDisplay : MonoBehaviour
{
    [SerializeField] private Image image;

    [YarnCommand("SetBackground")]
    public void SetBackground(string backgroundName)
    {
        Sprite sprite = Resources.Load<Sprite>("Backgrounds/" + backgroundName);
        if (sprite == null)
        {
            Debug.LogError("Background not found: " + backgroundName);
            return;
        }
        image.sprite = sprite;
    }
}
