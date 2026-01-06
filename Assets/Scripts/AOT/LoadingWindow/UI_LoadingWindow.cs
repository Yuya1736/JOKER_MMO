using JKFrame;
using System;
using UnityEngine;
using UnityEngine.UI;

[UIWindowData(nameof(UI_LoadingWindow), false, nameof(UI_LoadingWindow), 4)]
public class UI_LoadingWindow : UI_WindowBase
{
    [SerializeField] private Text updateContentText;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private Text progressText;

    public void SetDescription(string description)
    {
        updateContentText.text = $"¸üÐÂÄÚÈÝ£º{description}";
    }

    public void UpdateProgress(float now, float max)
    {
        progressSlider.maxValue = max;
        progressSlider.value = now;

        float percent = now / max * 100;
        progressText.text = $"{percent:F1}%";
    }

    public void UpdateProgressByBytes(float nowBytes, float maxBytes)
    {
        float nowMB = nowBytes / 1024f / 1024f;
        float maxMB = maxBytes / 1024f / 1024f;

        progressSlider.maxValue = maxMB;
        progressSlider.value = nowMB;

        progressText.text = $"{Math.Round(nowMB, 2)}MB / {Math.Round(maxMB, 2)}MB";
    }
}
