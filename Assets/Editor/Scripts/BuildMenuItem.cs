using HybridCLR.Editor;
using HybridCLR.Editor.Commands;
using JKFrame;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildMenuItem
{
    private static string buildPath = "Builds";
    private static string ServerPath = "Server";
    private static string ClientPath = "Client";

    [MenuItem("Project/Build/All")]
    public static void All()
    {
        Server();
        Client();
    }

    [MenuItem("Project/Build/Server")]
    public static void Server()
    {
        Debug.Log("开始构建服务端");
        // 开启程序集过滤
        ServerBuildFliterAssembly.serverMode = true;
        // 关闭HybridCLR
        HybridCLR.Editor.SettingsUtil.Enable = false;
        // 关闭Addressables
        AddressableAssetSettingsDefaultObject.Settings.BuildAddressablesWithPlayerBuild = AddressableAssetSettings.PlayerBuildOption.DoNotBuildWithPlayer;

        List<string> list = new List<string>(EditorSceneManager.sceneCountInBuildSettings);
        for(int i = 0;i < EditorSceneManager.sceneCountInBuildSettings;++ i)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            if(scenePath != null && !scenePath.Contains("Client") && !scenePath.Contains("Test"))
            {
                Debug.Log("正在加载场景：" + scenePath);
                list.Add(scenePath);
            }
        }

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions()
        {
            scenes = list.ToArray(),
            target = BuildTarget.StandaloneWindows64,
            subtarget = (int)StandaloneBuildSubtarget.Server,
            locationPathName = new DirectoryInfo(Application.dataPath).Parent.FullName + '/' + buildPath + '/' + ServerPath + "/Server.exe"
        };

        BuildPipeline.BuildPlayer(buildPlayerOptions);

        HybridCLR.Editor.SettingsUtil.Enable = true;
        AddressableAssetSettingsDefaultObject.Settings.BuildAddressablesWithPlayerBuild = AddressableAssetSettings.PlayerBuildOption.PreferencesValue;
        Debug.Log("完成构建服务端");
    }

    [MenuItem("Project/Build/Client")]
    public static void Client()
    {
        Debug.Log("开始构建客户端");
        ServerBuildFliterAssembly.serverMode = false;
        // if (serverEditorTest) JKFrameSetting.RemoveScriptCompilationSymbol(serverEditorTestSymbolName);
        // 清除Addressables缓存
        if (Directory.Exists(Application.persistentDataPath + "/com.unity.addressables")) Directory.Delete(Application.persistentDataPath + "/com.unity.addressables", true);
        AddressableAssetSettings.CleanPlayerContent();
        // 处理热更新相关
        PrebuildCommand.GenerateAll();
        GenerateDllBytesFiles();

        List<string> list = new List<string>(EditorSceneManager.sceneCountInBuildSettings);
        for (int i = 0; i < EditorSceneManager.sceneCountInBuildSettings; ++i)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            if (scenePath != null && !scenePath.Contains("Server") && !scenePath.Contains("Test"))
            {
                Debug.Log("正在加载场景：" + scenePath);
                list.Add(scenePath);
            }
        }

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions()
        {
            scenes = list.ToArray(),
            target = BuildTarget.StandaloneWindows64,
            subtarget = (int)StandaloneBuildSubtarget.Player,
            locationPathName = new DirectoryInfo(Application.dataPath).Parent.FullName + '/' + buildPath + '/' + ClientPath + "/Client.exe"
        };
        // Addressable会自动构建
        Debug.Log(new DirectoryInfo(Application.dataPath).Parent.FullName + '/' + buildPath + '/' + ClientPath + "/Client.exe");
        BuildPipeline.BuildPlayer(buildPlayerOptions);

        //if (serverEditorTest) JKFrameSetting.AddScriptCompilationSymbol(serverEditorTestSymbolName);

        Debug.Log("完成构建客户端");
    }

    [MenuItem("Project/Build/UpdateClient")]
    public static void UpdateClient()
    {
        Debug.Log("开始构建客户端更新包");
        ServerBuildFliterAssembly.serverMode = false;
        CompileDllCommand.CompileDllActiveBuildTarget();
        GenerateDllBytesFiles();
        // Addressable更新包
        // 拿到上一次的更新路径
        string path = ContentUpdateScript.GetContentStateDataPath(false);
        // 拿到Addressable的总配置文件
        AddressableAssetSettings addressableAssetSettings = AddressableAssetSettingsDefaultObject.Settings;
        // 根据配置文件和上一次的路径来进行update
        ContentUpdateScript.BuildContentUpdate(addressableAssetSettings, path);

        Debug.Log("完成构建客户端更新包");
    }

    [MenuItem("Project/Generate/GenerateDllBytesFiles")]
    public static void GenerateDllBytesFiles()
    {
        Debug.Log("开始创建DllBytes");

        string hotUpdateSourceDllDir = System.Environment.CurrentDirectory + '/' + SettingsUtil.GetHotUpdateDllsOutputDirByTarget(EditorUserBuildSettings.activeBuildTarget);
        string hotUpdateDllDir = Application.dataPath + "/Scripts/DllBytes/HotUpdate";
        string aotSourceDllDir = System.Environment.CurrentDirectory + '/' + SettingsUtil.GetAssembliesPostIl2CppStripDir(EditorUserBuildSettings.activeBuildTarget);
        string aotDllDir = Application.dataPath + "/Scripts/DllBytes/AOT";

        foreach (string dllName in SettingsUtil.AOTAssemblyNames)
        {
            File.Copy(aotSourceDllDir + '/' + dllName + ".dll", aotDllDir + '/' + dllName + ".dll.bytes", true);
        }

        foreach (string dllName in SettingsUtil.HotUpdateAssemblyNamesExcludePreserved)
        {
            File.Copy(hotUpdateSourceDllDir + '/' + dllName + ".dll", hotUpdateDllDir + '/' + dllName + ".dll.bytes", true);
        }
        AssetDatabase.Refresh();

        Debug.Log("完成创建DllBytes");
    }

    private static bool serverEditorTest;
    private const string serverEditorTestSymbolName = "SERVER_EDITOR_TEST";

    [MenuItem("Project/ServerEditorTest")]
    public static void ServerEditorTest()
    {
        serverEditorTest = !serverEditorTest;
        if (serverEditorTest) JKFrameSetting.AddScriptCompilationSymbol(serverEditorTestSymbolName);
        else JKFrameSetting.RemoveScriptCompilationSymbol(serverEditorTestSymbolName);
        Menu.SetChecked("Project/ServerEditorTest", serverEditorTest);
    }
}
