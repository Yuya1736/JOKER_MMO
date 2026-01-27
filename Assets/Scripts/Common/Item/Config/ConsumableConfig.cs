using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableConfig", menuName = "GenerateConfig/Item/ConsumableConfig")]
public class ConsumableConfig : ItemConfigBase
{
    public float recoverNum;

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
