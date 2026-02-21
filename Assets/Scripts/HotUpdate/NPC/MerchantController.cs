#if !UNITY_SERVER || UNITY_EDITOR
using UnityEngine;

public class MerchantController : NPCControllerBase
{
    public override void OnInteract()
    {
        PlayerManager.Instance.RequestOpenShop(configKey);
    }
}
#endif