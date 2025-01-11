using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class PortraitDisplay : MonoBehaviour
{
    public List<Image> images;
    public int portraitNumber { get; private set; }
    const int MIN_PORTRAIT_NUMBER = 1;
    const int MAX_PORTRAIT_NUMBER = 3;

    [YarnCommand("SetPortrait")]
    public void SetPortrait(string[] parameters)
    {
        List<string> spriteNames = parameters.ToList();
        if (spriteNames.Count < MIN_PORTRAIT_NUMBER)
        {
            Debug.LogError("Not enough arguments for SetPortrait command");
            return;
        }
        else if (spriteNames.Count > MAX_PORTRAIT_NUMBER)
        {
            Debug.LogWarning("Too many arguments for SetPortrait command");
            spriteNames.RemoveRange(MAX_PORTRAIT_NUMBER, spriteNames.Count - MAX_PORTRAIT_NUMBER);
        }

        SetImage(spriteNames.Count);

        for (int i = 0; i < spriteNames.Count; i++)
        {
            Debug.Log(spriteNames[i]);
            Sprite sprite = Resources.Load<Sprite>("Portraits/" + spriteNames[i]);
            if (sprite == null)
            {
                Debug.LogError("Sprite not found: " + spriteNames[i]);
                return;
            }
            images[i].sprite = sprite;
            images[i].rectTransform.sizeDelta = new Vector2(sprite.texture.width, sprite.texture.height);
        }
    }

    void SetImage(int portraitNumber)
    {
        // number 체크
        if (portraitNumber < MIN_PORTRAIT_NUMBER)
        {
            Debug.LogWarning($"Portrait number must be {MIN_PORTRAIT_NUMBER} or greater");
            portraitNumber = MIN_PORTRAIT_NUMBER;
        }
        else if (portraitNumber > MAX_PORTRAIT_NUMBER)
        {
            Debug.LogWarning($"Portrait number must be {MAX_PORTRAIT_NUMBER} or less");
            portraitNumber = MAX_PORTRAIT_NUMBER;
        }

        // 이미지 조정
        images = GetComponentsInChildren<Image>().ToList();
        if (images.Count < portraitNumber)
        {
            Debug.LogWarning("PortraitDisplay: Not enough images in the scene, creating new ones");
            CreateImages(portraitNumber - images.Count);
        }
        else if (images.Count > portraitNumber)
        {
            Debug.LogWarning("PortraitDisplay: Too many images in the scene, removing the extra ones");
            for (int i = portraitNumber; i < images.Count; i++)
            {
                Destroy(images[i].gameObject);
            }
            images.RemoveRange(portraitNumber, images.Count - portraitNumber);
        }
    }

    void CreateImages(int amount)
    {
        // Create the images
        for (int i = 0; i < amount; i++)
        {
            Image newImage = new GameObject($"Image_{i}").AddComponent<Image>();
            newImage.transform.SetParent(transform);

            images.Add(newImage);
        }
    }

    // IEnumerator Fade(Image image, float start, float end)
    // {
    //     float fadeDuration = 1f; // 페이드 지속 시간
    //     float elapsed = 0f;

    //     while (elapsed < fadeDuration)
    //     {
    //         elapsed += Time.deltaTime;
    //         canvasGroup.alpha = Mathf.Lerp(start, end, elapsed / fadeDuration);
    //         yield return null;
    //     }

    //     canvasGroup.alpha = end;
    // }
}
