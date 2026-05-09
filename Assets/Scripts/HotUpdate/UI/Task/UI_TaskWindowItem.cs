using JKFrame;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TaskWindowItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Text textName;
    public Text textDescription;
    public Text textProgress;
    [HideInInspector] public TaskConfig config;
    public Image highLightBk;

    public Action<TaskConfig> onTaskBeClickAction;
    public Action onTaskEndAction;

    private bool isSelected;

    public void Init(TaskConfig config)
    {
        this.config = config;
        textName.text = config.taskName;
        textDescription.text = config.taskDescription;
        UpdateProgress(0, config.taskInfo.GetCount());
        SetSelected(false);
    }

    public void UpdateProgress(int cur, int max)
    {
        textProgress.text = $"{cur} / {max}";
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        highLightBk.gameObject.SetActive(selected);
    }

    public void Destroy()
    {
        onTaskEndAction?.Invoke();
        this.GameObjectPushPool();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highLightBk.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highLightBk.gameObject.SetActive(isSelected);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            onTaskBeClickAction?.Invoke(config);
        }
    }
}
