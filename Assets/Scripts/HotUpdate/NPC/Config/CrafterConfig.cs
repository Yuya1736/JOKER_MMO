using JKFrame;
using System.Collections.Generic;
using UnityEngine;

public struct CrafterItemConfig
{
    public ItemConfigBase itemConfig;
    public int count;
}

[CreateAssetMenu(fileName = "CrafterConfig", menuName = "GenerateConfig/CrafterConfig")]
public class CrafterConfig : ConfigBase
{
    public List<CrafterItemConfig> itemConfigs = new List<CrafterItemConfig>(100);
}
