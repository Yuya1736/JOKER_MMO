using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class ExcelImpoter
{
    [MenuItem("Project/Generate/ImportExcelToGlobalLocalizationConfig")]
    public static void ImportExcelToGlobalLocalizationConfig()
    {
        string SOPath = "Assets/Config/GlobalLocalizationConfig.asset";
        LocalizationConfig localizationConfig = AssetDatabase.LoadAssetAtPath<LocalizationConfig>(SOPath);
        if (localizationConfig == null)
        {
            localizationConfig = ScriptableObject.CreateInstance<LocalizationConfig>();
            AssetDatabase.CreateAsset(localizationConfig, SOPath);
        }
        else localizationConfig.config.Clear();

        string ExcelPath = Application.dataPath + "/Config/Excel/GlobalLocalizationConfigExcel.xlsx";
        FileInfo ExcelFile = new FileInfo(ExcelPath);
        using (ExcelPackage excelPackage = new ExcelPackage(ExcelFile))
        {
            ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[1];
            int maxRow = excelWorksheet.Dimension.Rows;
            // 已知有三列 Key, 简体中文, 英文
            for(int row = 2; row <= maxRow; ++ row)
            {
                string key = excelWorksheet.Cells[row, 1].Text.Trim();
                if (string.IsNullOrEmpty(key)) continue;

                Dictionary<LanguageType, LocalizationDataBase> contentDic = new Dictionary<LanguageType, LocalizationDataBase>();
                contentDic.Add(LanguageType.SimplifiedChinese, new LocalizationStringData() { content = excelWorksheet.Cells[row, 2].Text.Trim() });
                contentDic.Add(LanguageType.English, new LocalizationStringData() { content = excelWorksheet.Cells[row, 3].Text.Trim() });

                localizationConfig.config.Add(key, contentDic);
            }
            // 通过编辑器脚本修改的数据 必须SetDirty让Unity知道数据被修改了
            EditorUtility.SetDirty(localizationConfig);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("Excel转GlobalLocalizationConfig成功");
        }
    }
}
