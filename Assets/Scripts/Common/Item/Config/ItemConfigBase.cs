using JKFrame;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemConfigBase : ConfigBase
{
    public Sprite icon;
    public string slotKey;
    public int price;
    public Dictionary<LanguageType, string> itemNameDic = new Dictionary<LanguageType, string>();
    public Dictionary<LanguageType, string> itemDescriptionDic = new Dictionary<LanguageType, string>();
    public Dictionary<string, int> craftItemDic = new Dictionary<string, int>(5);

    public string GetItemName(LanguageType language)
    {
        return itemNameDic[language]; 
    }

    public string GetItemDescription(LanguageType language) 
    { 
        return itemDescriptionDic[language]; 
    }

    public abstract ItemDataBase GetDefaultItemData(bool isNew = true);

    public abstract string GetItemType(LanguageType language); // 类型这里比较简单 就不用配置Excel转化了 直接写死
}