using OfficeOpenXml;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class MonsterExcelImporter
{
    [MenuItem("Project/Generate/ImportExcelToMonsterConfig")]
    public static void ImportExcelToMonsterConfig()
    {
        string ExcelPath = Application.dataPath + "/Config/Excel/怪物配置.xlsx";
        FileInfo ExcelFile = new FileInfo(ExcelPath);
        using (ExcelPackage excelPackage = new ExcelPackage(ExcelFile))
        {
            ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[1];
            int maxRow = excelWorksheet.Dimension.Rows;
            for (int row = 2; row <= maxRow; ++row)
            {
                string key = excelWorksheet.Cells[row, 1].Text.Trim();
                string name = excelWorksheet.Cells[row, 2].Text.Trim();
                float maxHp = float.Parse(excelWorksheet.Cells[row, 3].Text.Trim());
                float atk = float.Parse(excelWorksheet.Cells[row, 4].Text.Trim());
                float atkDistance = float.Parse(excelWorksheet.Cells[row, 5].Text.Trim());
                float maxIdleTime = float.Parse(excelWorksheet.Cells[row, 6].Text.Trim());
                float maxPatrolTime = float.Parse(excelWorksheet.Cells[row, 7].Text.Trim());
                float maxChaseDistance = float.Parse(excelWorksheet.Cells[row, 8].Text.Trim());
                float maxChaseTime = float.Parse(excelWorksheet.Cells[row, 9].Text.Trim());

                string SOPath = $"Assets/Config/Monster/{key}.asset";
                MonsterConfig config = AssetDatabase.LoadAssetAtPath<MonsterConfig>(SOPath);
                if (config == null)
                {
                    config = ScriptableObject.CreateInstance<MonsterConfig>();
                    AssetDatabase.CreateAsset(config, SOPath);
                }
                config.mosterName = name;
                config.maxHp = maxHp;
                config.atk = atk;
                config.atkDistance = atkDistance;
                config.maxIdleTime = maxIdleTime;
                config.maxPatrolTime = maxPatrolTime;
                config.maxChaseDistance = maxChaseDistance;
                config.maxChaseTime = maxChaseTime;
                EditorUtility.SetDirty(config);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
