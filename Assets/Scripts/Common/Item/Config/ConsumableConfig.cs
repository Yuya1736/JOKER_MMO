using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableConfig", menuName = "GenerateConfig/Item/ConsumableConfig")]
public class ConsumableConfig : ItemConfigBase
{
    public float recoverNum;
    private ConsumableData defaultData;

    public override ItemDataBase GetDefaultItemData(bool isNew = true)
    {
        if (isNew)
        {
            return new ConsumableData()
            {
                id = this.name,
                count = 1
            };
        }
        else
        {
            if (defaultData == null) defaultData = (ConsumableData)GetDefaultItemData();
            return defaultData;
        }
    }

    public override string GetItemType(LanguageType language)
    {
        string typeInfo = string.Empty;
        switch (language)
        {
            case LanguageType.SimplifiedChinese:
                typeInfo = "ÏûºÄÆ·";
                break;
            case LanguageType.English:
                typeInfo = "Consumable";
                break;
        }
        return typeInfo;
    }
}
