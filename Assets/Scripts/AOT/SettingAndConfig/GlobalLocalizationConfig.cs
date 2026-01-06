using JKFrame;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalLocalizationConfig", menuName = "GenerateConfig/GlobalLocalizationConfig")]
public class GlobalLocalizationConfig : ConfigBase 
{

    // Dic<key, Dic<language, content>>
    // 例：Dic<标题, Dic<简体中文, 我是一个标题>>
    // 例：Dic<标题, Dic<English, I am a title>>
    public Dictionary<string, Dictionary<LanguageType, string>> config = new Dictionary<string, Dictionary<LanguageType, string>>();

    public void Clear()
    {
        config.Clear(); 
    }
}
