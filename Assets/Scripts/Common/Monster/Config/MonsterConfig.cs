using JKFrame;
using UnityEngine;


[CreateAssetMenu(fileName = "MonsterConfig", menuName = "GenerateConfig/MonsterConfig")]
public class MonsterConfig : ConfigBase
{
    public string mosterName;
    public float maxHp;
    public float atk;
    public float atkDistance;
    public float maxIdleTime;
    public float maxPatrolTime;
    public float maxChaseDistance;
    public float maxChaseTime;
}
