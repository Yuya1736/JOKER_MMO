#if !UNITY_SERVER || UNITY_EDITOR
using JKFrame;

public class MerchantController : NPCControllerBase
{
    private bool isShopOpen = false;
    public override void EnterInteract()
    {
        base.EnterInteract();
        if (UISystem.GetWindow<UI_DialogWindow>() == null) StartDialog(npcDialogConfig.GetDialogConfig("打招呼"), OpenShop);
        //PlayerManager.Instance.RequestOpenShop(configKey);
    }

    public void OpenShop()
    {
        PlayerManager.Instance.RequestOpenShop(configKey);
        isShopOpen = true;
    }

    protected override void Update()
    {
        base.Update();
        if (isShopOpen)
        {
            if (UISystem.GetWindow<UI_ShopWindow>() == null)
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