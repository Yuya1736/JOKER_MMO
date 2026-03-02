using JKFrame;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAtkConfigList", menuName = "GenerateConfig/PlayerAtkConfigList")]
public class PlayerAtkConfigList : ConfigBase
{
    public List<PlayerAtkConfig> playerAtkConfigs = new List<PlayerAtkConfig>();
}
