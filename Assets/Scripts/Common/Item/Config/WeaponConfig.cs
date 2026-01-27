using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "GenerateConfig/Item/WeaponConfig")]
public class WeaponConfig : ItemConfigBase
{
    public float atk;
    public GameObject prefab;

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
