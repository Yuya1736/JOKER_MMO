using JKFrame;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAtkConfig", menuName = "GenerateConfig/PlayerAtkConfig")]
public class PlayerAtkConfig : ConfigBase
{
    public string animName;
    public int damage;

    public EffectConfig atkEffectConfig; // 挥砍武器的特效
    public EffectConfig hitEffectConfig; // 击打到敌人的特效
}
