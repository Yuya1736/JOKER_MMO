using JKFrame;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterAtkConfig", menuName = "GenerateConfig/MonsterAtkConfig")]
public class MonsterAtkConfig : ConfigBase
{
    public float damage;

    public EffectConfig effect;
}