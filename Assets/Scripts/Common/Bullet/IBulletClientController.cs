using UnityEngine;

public interface IBulletClientController
{
    public void PlayHitEffect(EffectConfig effectConfig);
    public void PlayHitEffect(Vector3 point);
}