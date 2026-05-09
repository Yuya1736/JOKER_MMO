using OfficeOpenXml;
using System.IO;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public static class DialogExcelImporter
{
    public const string DialogExcelPath = "Assets/Config/Excel/Dialog";
    public const string DialogClipPath = "Assets/Config/Dialog/DialogClip";
    public const string DialogPath = "Assets/Config/Dialog";

    [MenuItem("Project/Generate/ImportExcelToDialogConfig")]
    public static void ImportAll()
    {
        string[] files = Directory.GetFiles(DialogExcelPath, "*.xlsx");
        foreach (string file in files)
        {
            if (file.Contains("~$")) continue; // 包含“~$”的为正在修改的文件，不需要读取
            string fullPath = $"{Application.dataPath.Replace("/Assets", "")}/{file}";
            ImportConfig(fullPath);
        }
        AssetDatabase.Refresh();
    }

    public static void ImportConfig(string excelPath)
    {
        FileInfo ExcelFile = new FileInfo(excelPath);

        using (ExcelPackage excelPackage = new ExcelPackage(ExcelFile))
        {
            int sheetCount = excelPackage.Workbook.Worksheets.Count;
            for (int i = 1; i <= sheetCount;++ i)
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[i];
                if (excelWorksheet.Dimension == null) continue;
                // 读取Dialog配置文件，如果不存在就创建，存在就清空ClipList重新配置
                string configDir = $"{DialogPath}/{Path.GetFileNameWithoutExtension(ExcelFile.Name)}";
                string configPath = $"{configDir}/{excelWorksheet.Name}.asset";
                // 确保目录存在
                if (!Directory.Exists(configDir)) Directory.CreateDirectory(configDir);
                DialogConfig dialogConfig = AssetDatabase.LoadAssetAtPath<DialogConfig>(configPath);
                bool create = dialogConfig == null;
                if (create) dialogConfig = ScriptableObject.CreateInstance<DialogConfig>();
                else dialogConfig.clipList.Clear();


                int maxRow = excelWorksheet.Dimension.Rows;
                for (int row = 2; row <= maxRow;++row)
                {
                    string name = excelWorksheet.Cells[row, 1].Text;
                    if (string.IsNullOrEmpty(name)) name = " ";
                    string content = excelWorksheet.Cells[row, 2].Text;
                    if (string.IsNullOrEmpty(content)) break;
                    DialogClip clip = new DialogClip()
                    {
                        name = name,
                        content = content
                    };
                    dialogConfig.clipList.Add(clip);
                }
                if (create) AssetDatabase.CreateAsset(dialogConfig, configPath);
                else
                {
                    EditorUtility.SetDirty(dialogConfig);
                    AssetDatabase.SaveAssetIfDirty(dialogConfig);
                }
            }
        }
    }
}
