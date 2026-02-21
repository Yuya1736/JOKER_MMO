using OfficeOpenXml;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class ItemExcelImpoter
{
    [MenuItem("Project/Generate/ImportExcelToItemConfig")]
    public static void ImportExcelToItemConfig()
    {
        string ExcelPath = Application.dataPath + "/Config/Excel/物品配置.xlsx";
        FileInfo ExcelFile = new FileInfo(ExcelPath);
        using (ExcelPackage excelPackage = new ExcelPackage(ExcelFile))
        {
            // 三个Sheet分别为 1.武器，2.消耗品，3.材料
            for (int i = 1; i <= 3; ++i)
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[i];
                int maxRow = excelWorksheet.Dimension.Rows;
                // 已知有6列 Key, 中文名称, 英文名称，中文描述，英文描述，SlotKey
                //string SOPath = SOPathList[i - 1];
                for (int row = 2; row <= maxRow; ++row)
                {
                    // 这几个属性是共有的
                    string key = excelWorksheet.Cells[row, 1].Text.Trim();
                    if (string.IsNullOrEmpty(key)) continue;
                    string chineseName = excelWorksheet.Cells[row, 2].Text.Trim();
                    string englishName = excelWorksheet.Cells[row, 3].Text.Trim();
                    string chineseDescription = excelWorksheet.Cells[row, 4].Text.Trim();
                    string englishDescription = excelWorksheet.Cells[row, 5].Text.Trim();
                    
                    if (i == 1) // 武器
                    {
                        string SOPath = $"Assets/Config/Item/Weapon/{key}.asset";
                        string iconPath = $"Assets/Res/Icon/Weapon/{key}.png";
                        string prefabPath = $"Assets/Res/Prefab/Weapon/{key}.prefab";
                        Sprite icon = AssetDatabase.LoadAssetAtPath<Sprite>(iconPath);
                        string atk = excelWorksheet.Cells[row, 6].Text.Trim();
                        string slotKey = excelWorksheet.Cells[row, 7].Text.Trim();
                        int price = int.Parse(excelWorksheet.Cells[row, 8].Text.Trim());
                        string[] craftItems = excelWorksheet.Cells[row, 9].Text.Trim().Split(',');
                        WeaponConfig weaponConfig = AssetDatabase.LoadAssetAtPath<WeaponConfig>(SOPath);
                        if (weaponConfig == null)
                        {
                            weaponConfig = ScriptableObject.CreateInstance<WeaponConfig>();
                            AssetDatabase.CreateAsset(weaponConfig, SOPath);
                        }
                        ImportBaseInfo(weaponConfig, icon, slotKey, chineseName, englishName, chineseDescription, englishDescription, price);
                        weaponConfig.atk = float.Parse(atk);
                        weaponConfig.prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                        weaponConfig.craftItemDic.Clear();
                        if (craftItems != null) for (int j = 0; j < craftItems.Length - 1; j += 2) weaponConfig.craftItemDic.Add(craftItems[j], int.Parse(craftItems[j + 1]));
                        EditorUtility.SetDirty(weaponConfig);
                    }
                    else if (i == 2) // 消耗品
                    {
                        string SOPath = $"Assets/Config/Item/Consumable/{key}.asset";
                        string iconPath = $"Assets/Res/Icon/Consumable/{key}.png";
                        Sprite icon = AssetDatabase.LoadAssetAtPath<Sprite>(iconPath);
                        string recoverNum = excelWorksheet.Cells[row, 6].Text.Trim();
                        string slotKey = excelWorksheet.Cells[row, 7].Text.Trim();
                        int price = int.Parse(excelWorksheet.Cells[row, 8].Text.Trim());
                        string[] craftItems = excelWorksheet.Cells[row, 9].Text.Trim().Split(',');
                        ConsumableConfig consumableConfig = AssetDatabase.LoadAssetAtPath<ConsumableConfig>(SOPath);
                        if (consumableConfig == null)
                        {
                            consumableConfig = ScriptableObject.CreateInstance<ConsumableConfig>();
                            AssetDatabase.CreateAsset(consumableConfig, SOPath);
                        }
                        ImportBaseInfo(consumableConfig, icon, slotKey, chineseName, englishName, chineseDescription, englishDescription, price);
                        consumableConfig.recoverNum = float.Parse(recoverNum);
                        consumableConfig.craftItemDic.Clear();
                        if (craftItems != null) for (int j = 0; j < craftItems.Length - 1; j += 2) consumableConfig.craftItemDic.Add(craftItems[j], int.Parse(craftItems[j + 1]));
                        EditorUtility.SetDirty(consumableConfig);
                    }
                    else if (i == 3) // 材料
                    {
                        string SOPath = $"Assets/Config/Item/Material/{key}.asset";
                        string iconPath = $"Assets/Res/Icon/Material/{key}.png";
                        Sprite icon = AssetDatabase.LoadAssetAtPath<Sprite>(iconPath);
                        string slotKey = excelWorksheet.Cells[row, 6].Text.Trim();
                        int price = int.Parse(excelWorksheet.Cells[row, 7].Text.Trim());
                        MaterialConfig materialConfig = AssetDatabase.LoadAssetAtPath<MaterialConfig>(SOPath);
                        if (materialConfig == null)
                        {
                            materialConfig = ScriptableObject.CreateInstance<MaterialConfig>();
                            AssetDatabase.CreateAsset(materialConfig, SOPath);
                        }
                        ImportBaseInfo(materialConfig, icon, slotKey, chineseName, englishName, chineseDescription, englishDescription, price);
                        EditorUtility.SetDirty(materialConfig);
                    }
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
        }  
        Debug.Log("Excel转ItemConfig成功");
    }

    private static void ImportBaseInfo(ItemConfigBase itemConfig, Sprite icon, string slotKey, string chineseName, string englishName, string chineseDescription, string englishDescription, int price)
    {
        itemConfig.icon = icon;
        itemConfig.price = price;
        if(string.IsNullOrEmpty(slotKey)) itemConfig.slotKey = slotKey;

        itemConfig.itemNameDic[LanguageType.SimplifiedChinese] = chineseName;
        itemConfig.itemNameDic[LanguageType.English] = englishName;

        itemConfig.itemDescriptionDic[LanguageType.SimplifiedChinese] = chineseDescription;
        itemConfig.itemDescriptionDic[LanguageType.English] = englishDescription;
    }
}
