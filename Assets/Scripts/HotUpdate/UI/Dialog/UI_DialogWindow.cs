using DG.Tweening;
using JKFrame;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DialogWindow : UI_CustomWindowBase, IPointerClickHandler, IInputBlockerUI
{
    public Text textName;
    public Text textContent;
    private List<DialogClip> _clips;
    private Tween _tween;
    private bool _isDoingText;
    private string _currContent;
    private int _currIndex;
    private Action _onComplete;

    public void Show(DialogConfig config, Action onComplete)
    {
        _onComplete = onComplete;
        _clips = config.clipList;
        _currIndex = 0;
        if (_clips.Count > 0) ShowClip(_clips[0]); // 加载窗口时先显示第一个对话切片
    }

    public void ShowClip(DialogClip clip)
    {
        ClearWindow();
        _tween?.Kill();
        _currIndex++;
        textName.text = clip.name;
        _isDoingText = true;
        _currContent = clip.content;
        //textContent.do
        _tween = textContent.DOText(_currContent, 5).SetSpeedBased(true).OnComplete(() =>
        {
            _isDoingText = false;
        });
    }

    public override void OnShow()
    {
        base.OnShow();
        ClearWindow();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void OnClose()
    {
        base.OnClose();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ClearWindow()
    {
        textName.text = "";
        textContent.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 如果正在打字，则跳过打字，显示全文
        if (_isDoingText) 
        {
            _tween?.Kill();
            _isDoingText = false;
            textContent.text = _currContent;
        }
        else
        {
            if (_clips.Count > _currIndex)
            {
                ShowClip(_clips[_currIndex]);
            }
            else
            {
                _onComplete?.Invoke();
                UISystem.Close<UI_DialogWindow>();
            }
        }
    }
}
