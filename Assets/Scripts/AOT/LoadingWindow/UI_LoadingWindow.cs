using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JKFrame;
using UnityEngine.UI;
using System;

[UIWindowData(nameof(UI_LoadingWindow), false, nameof(UI_LoadingWindow), 4)]
public class UI_LoadingWindow : UI_WindowBase
{
    [SerializeField] private Text updateContentText;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private Text progressText;

    public void Init(string updateContent)
    {
        updateContentText.text = $"¸üÐÂÄÚÈÝ£º{updateContent}";
    }

    public void UpdateProgress(float now, float max)
    {
        float nowMB = now / 1024f / 1024f;
        float maxMB = max / 1024f / 1024f;

        progressSlider.maxValue = nowMB;
        progressSlider.value = maxMB;

        progressText.text = $"{Math.Round(nowMB, 2)}MB / {Math.Round(maxMB, 2)}MB";
    }
}
