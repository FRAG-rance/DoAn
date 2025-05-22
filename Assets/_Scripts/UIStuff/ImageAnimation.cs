using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageAnimation : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image image;

    [SerializeField] float fps = 10;

    private void Awake()
    {
        image = GetComponent<Image>();
        ShowFrame(0);
    }

    public void Play()
    {
        Stop();
        StartCoroutine(AnimSequence());
        ShowFrame(0);
    }

    public void Stop()
    {
        StopAllCoroutines();
        ShowFrame(0);
    }

    IEnumerator AnimSequence()
    {
        var delay = new WaitForSeconds(1 / fps);
        int index = 0;
        while (index < sprites.Length)
        {
            ShowFrame(index);
            index++;
            yield return delay;
        }
    }

    void ShowFrame(int index)
    {
        image.sprite = sprites[index];
    }


}