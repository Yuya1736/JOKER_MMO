using JKFrame;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameBasicSetting", menuName = "GenerateConfig/GameBasicSetting")]
public class GameBasicSetting : ConfigBase
{
    public class VersionData
    {
        [Multiline]
        public string description;
    }

    public Dictionary<LanguageType, VersionData> versionDataDic = new Dictionary<LanguageType, VersionData>();
    public LanguageType LanguageType = LanguageType.English;

    public VersionData GetVersionData(LanguageType languageType)
    {
        if(versionDataDic.ContainsKey(languageType))
        {
            return versionDataDic[languageType];
        }
        return null;
    }
}
