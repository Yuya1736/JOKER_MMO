using JKFrame;
using System.Collections.Generic;
using UnityEngine;

public struct MerchantItemConfig
{
    public ItemConfigBase itemConfig;
    public int count;
}

[CreateAssetMenu(fileName = "MerchantConfig", menuName = "GenerateConfig/MerchantConfig")]
public class MerchantConfig : ConfigBase
{
    public List<MerchantItemConfig> itemConfigs = new List<MerchantItemConfig>(100);
}
