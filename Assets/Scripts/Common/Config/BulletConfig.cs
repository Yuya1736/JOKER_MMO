using JKFrame;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletConfig", menuName = "GenerateConfig/BulletConfig")]
public class BulletConfig : ConfigBase
{
    public float speed;
    public float despawnTime;
    public EffectConfig boomEffect;
}
