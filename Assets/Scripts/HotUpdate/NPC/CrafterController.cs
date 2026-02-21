#if !UNITY_SERVER || UNITY_EDITOR
using UnityEngine;

public class CrafterController : NPCControllerBase
{
    public override void OnInteract()
    {
        PlayerManager.Instance.RequestOpenCraft(configKey);
    }
}
#endif