using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class PortraitDisplay : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    public List<Image> images;
    public int portraitNumber { get; private set; }
    private const int MIN_PORTRAIT_NUMBER = 0;
    private const int MAX_PORTRAIT_NUMBER = 3;
    private float fadeDuration = 0.2f; // 페이드 지속 시간
    private List<string> previousSpriteNames = new List<string>();

    [YarnCommand("SetPortrait")]
    public void SetPortrait(string[] parameters = null)
    {
        List<string> spriteNames;
        if (parameters != null) spriteNames = parameters.ToList();
        else spriteNames = new List<string>();

        if (spriteNames.Count > MAX_PORTRAIT_NUMBER)
        {
            Debug.LogWarning("Too many arguments for SetPortrait command");
            spriteNames.RemoveRange(MAX_PORTRAIT_NUMBER, spriteNames.Count - MAX_PORTRAIT_NUMBER);
        }
        StartCoroutine(ChangePortrait(spriteNames));
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

        // 이미지 개수 조정
        images = GetComponentsInChildren<Image>().ToList();
        if (images.Count < portraitNumber)
        {
            CreateImages(portraitNumber - images.Count);
        }
        else if (images.Count > portraitNumber)
        {
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

    IEnumerator ChangePortrait(List<string> spriteNames)
    {
        bool doFade;
        List<string> spriteNamesModified = spriteNames.Select(item => item.Split('_')[0]).ToList();
        List<string> previousSpriteNamesModified = previousSpriteNames.Select(item => item.Split('_')[0]).ToList();
        doFade = !spriteNamesModified.SequenceEqual(previousSpriteNamesModified);

        if (doFade) yield return Fade(1, 0);
        SetImage(spriteNames.Count);
        for (int i = 0; i < spriteNames.Count; i++)
        {
            Sprite sprite = Resources.Load<Sprite>("Portraits/" + spriteNames[i]);
            if (sprite == null)
            {
                Debug.LogError("Sprite not found: " + spriteNames[i]);
                continue;
            }
            images[i].sprite = sprite;
            images[i].rectTransform.sizeDelta = new Vector2(sprite.texture.width, sprite.texture.height);
            images[i].rectTransform.localScale = new Vector3(1, 1, 1);
        }
        // 왼쪽사람은 오른쪽 보게

        if (images != null & images.Count != 0)
        {
            if (images[0] != null) images[0].rectTransform.localScale = new Vector3(-1, 1, 1);
        }
        
        if (doFade) yield return Fade(0, 1);

        previousSpriteNames = spriteNames;
    }

    IEnumerator Fade(float start, float end)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = end;
    }
}
