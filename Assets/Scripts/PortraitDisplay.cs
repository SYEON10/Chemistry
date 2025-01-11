using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class PortraitDisplay : MonoBehaviour
{
    public List<Image> images;
    public int portraitNumber = 1;

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
        images[0].sprite = sprite; // TODO: 인물 여러명일때도 되도록 바꾸기
    }

    void SetPortrait(int portraitNumber)
    {
        // TODO: 인물 사진 개수 설정하기
    }
}
