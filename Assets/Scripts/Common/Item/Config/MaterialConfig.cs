using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialConfig", menuName = "GenerateConfig/Item/MaterialConfig")]
public class MaterialConfig : ItemConfigBase
{
    private MaterialData defaultData;
    public override ItemDataBase GetDefaultItemData(bool isNew = true)
    {
        if (isNew)
        {
            return new MaterialData()
            {
                id = this.name,
                count = 1
            };
        }
        else // ²Î¿¼ConsumableConfig
        {
            if (defaultData == null) defaultData = (MaterialData)GetDefaultItemData();
            return defaultData;
        }
    }
    public override string GetItemType(LanguageType language)
    {
        string typeInfo = string.Empty;
        switch (language)
        {
            case LanguageType.SimplifiedChinese:
                typeInfo = "²ÄÁÏ";
                break;
            case LanguageType.English:
                typeInfo = "Material";
                break;
        }
        return typeInfo;
    }
}
