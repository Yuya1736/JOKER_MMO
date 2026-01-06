using System;

[Serializable]
public class GameSetting
{
    // 登录面板
    public bool UI_LoginWindowTogRemember;
    public string userName = "";
    public string password = "";

    // 游戏设置面板
    public LanguageType language = LanguageType.SimplifiedChinese;
    public float musicValue = 1;
    public float musicEffValue = 1;
}
