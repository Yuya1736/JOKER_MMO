using UnityEngine;

[CreateAssetMenu(fileName = "MaterialConfig", menuName = "GenerateConfig/Item/MaterialConfig")]
public class MaterialConfig : ItemConfigBase
{
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
