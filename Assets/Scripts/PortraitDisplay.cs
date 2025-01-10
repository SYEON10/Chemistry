using System;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class PortraitDisplay : MonoBehaviour
{
    public Image image;

    void Start()
    {
        DialogManager.Instance.dialogueRunner.AddCommandHandler("ChangePortrait", (System.Action<string>)ChangePortrait);
    }
    
    void ChangePortrait(string spriteName)
    {
        Debug.Log("Changing portrait to " + spriteName);
        Sprite sprite = Resources.Load<Sprite>("Portraits/" + spriteName);
        if (sprite == null)
        {
            Debug.LogError("Sprite not found: " + spriteName);
            return;
        }
        image.sprite = sprite;
    }

}
