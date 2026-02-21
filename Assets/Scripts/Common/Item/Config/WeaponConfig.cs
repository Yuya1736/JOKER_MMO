using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "GenerateConfig/Item/WeaponConfig")]
public class WeaponConfig : ItemConfigBase
{
    public float atk;
    public GameObject prefab;
    private WeaponData defaultData;

    public override ItemDataBase GetDefaultItemData(bool isNew = true)
    {
        if (isNew)
        {
            return new WeaponData()
            {
                id = this.name
            };
        }
        else
        {
            if (defaultData == null) defaultData = (WeaponData)GetDefaultItemData();
            return defaultData;
        }
    }
    public override string GetItemType(LanguageType language)
    {
        string typeInfo = string.Empty;
        switch (language)
        {
            case LanguageType.SimplifiedChinese:
                typeInfo = "ÎäÆ÷";
                break;
            case LanguageType.English:
                typeInfo = "Weapon";
                break;
        }
        return typeInfo;
    }
}
