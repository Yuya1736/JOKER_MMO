using JKFrame;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_TaskWindow : UI_CustomWindowBase
{
    public Transform itemRoot;

    public List<UI_TaskWindowItem> taskWindowItems = new List<UI_TaskWindowItem>(5);
    public Action<TaskConfig> onTaskBeClickAction;
    public Action onTaskEndAction;

    private UI_TaskWindowItem selectedItem;

    public override void OnShow()
    {
        base.OnShow();
        Clear();

        List<TaskData> taskList = PlayerManager.taskDatas.taskDataList;
        for (int i = 0; i < taskList.Count; i++)
        {
            UI_TaskWindowItem item = ResSystem.InstantiateGameObject(itemRoot, nameof(UI_TaskWindowItem)).GetComponent<UI_TaskWindowItem>();
            TaskConfig config = ResSystem.LoadAsset<TaskConfig>(taskList[i].taskConfigId);
            item.Init(config);
            item.UpdateProgress(taskList[i].progress, config.taskInfo.GetCount());
            taskWindowItems.Add(item);
        }

        BindAction();
    }

    public void BindAction()
    {
        for (int i = 0; i < taskWindowItems.Count; i++)
        {
            UI_TaskWindowItem item = taskWindowItems[i];
            item.onTaskBeClickAction = OnTaskItemClick;
            item.onTaskEndAction = onTaskEndAction;
        }
    }

    private void OnTaskItemClick(TaskConfig config)
    {
        for (int i = 0; i < taskWindowItems.Count; i++)
        {
            UI_TaskWindowItem item = taskWindowItems[i];
            bool selected = item.config == config;
            item.SetSelected(selected);
            if (selected) selectedItem = item;
        }

        onTaskBeClickAction?.Invoke(config);
    }

    public void Clear()
    {
        selectedItem = null;

        for (int i = taskWindowItems.Count - 1; i >= 0; i--)
        {
            taskWindowItems[i].Destroy();
        }
        taskWindowItems.Clear();
    }

    public override void OnClose()
    {
        base.OnClose();
        Clear();
    }
}
