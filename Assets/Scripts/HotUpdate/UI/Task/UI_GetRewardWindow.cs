using DG.Tweening;
using JKFrame;
using System.Collections.Generic;
using UnityEngine;

public class UI_GetRewardWindow : UI_CustomWindowBase
{
    public Transform root;
    public CanvasGroup canvasGroup;
    public List<UI_RewardItem> itemList = new List<UI_RewardItem>(5);
    private Tween tween;

    public void Show(params RewardData[] itemData)
    {
        for(int i = 0; i < itemData.Length; i++)
        {
            Sprite icon = ResSystem.LoadAsset<Sprite>(itemData[i].iconKey);
            UI_RewardItem item = ResSystem.InstantiateGameObject(root, nameof(UI_RewardItem)).GetComponent<UI_RewardItem>();
            itemList.Add(item);
            item.Init(icon, itemData[i].count);
        }
    }
    public override void OnShow()
    {
        base.OnShow();
        canvasGroup.alpha = 0;
        ShowWindowAlpha();
    }

    public override void OnClose()
    {
        base.OnClose();
    }

    public void ShowWindowAlpha()
    {
        if (tween != null) tween.Kill();
        tween = canvasGroup.DOFade(1, 2f).OnComplete(() =>
        {
            HideWindowAlpha();
        });
    }

    public void HideWindowAlpha()
    {
        if (tween != null) tween.Kill();
        tween = canvasGroup.DOFade(0, 2f).OnComplete(() =>
        {
            UISystem.Close<UI_GetRewardWindow>();
        });
    }
}
