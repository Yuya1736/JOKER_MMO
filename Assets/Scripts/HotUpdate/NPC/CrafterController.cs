#if !UNITY_SERVER || UNITY_EDITOR
using JKFrame;
using UnityEngine;

public class CrafterController : NPCControllerBase
{
    public bool isShopOpen = false;
    public override void EnterInteract()
    {
        base.EnterInteract();
        if (UISystem.GetWindow<UI_DialogWindow>() == null) StartDialog(npcDialogConfig.GetDialogConfig("打招呼"), OpenShop);
    }

    public void OpenShop()
    {
        PlayerManager.Instance.RequestOpenCraft(configKey);
        isShopOpen = true;
    }

    protected override void Update()
    {
        base.Update();
        if (isShopOpen)
        {
            if (UISystem.GetWindow<UI_CraftWindow>() == null)
            {
                isShopOpen = false;
                ExitInteract();
            }
        }
    }

    public override void ExitInteract()
    {
        base.ExitInteract();

    }
}
#endif