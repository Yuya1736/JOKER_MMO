using HybridCLR;
using JKFrame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class HotUpdateSystem : MonoBehaviour
{

    [Serializable]
    public class HotUpdateSystemState
    {
        public bool succeed;
    }

    [SerializeField] private List<TextAsset> aotDllTextList;
    [SerializeField] private List<string> hotUpdateDllKeyList;
    [SerializeField] private string versionInfoAddressableKey;
    private Action<float> onUpdatePercentDo;
    private Action<bool> onEnd;

    public void StartHotUpdate(Action<float> onUpdatePercentDo, Action<bool> onEnd)
    {
        this.onUpdatePercentDo = onUpdatePercentDo;
        this.onEnd = onEnd;

        StartCoroutine(DoUpdateAddressables());
    }

    bool succeed;
    private IEnumerator DoUpdateAddressables()
    {
        // 断点传续
        HotUpdateSystemState hotUpdateSystemState = SaveSystem.LoadSetting<HotUpdateSystemState>();
        if (hotUpdateSystemState == null || hotUpdateSystemState.succeed == false)
        {
            string path = Application.persistentDataPath + "\\com.unity.addressables";
            if (Directory.Exists(path)) Directory.Delete(path, true);
        }
        succeed = true;
        // 初始化Addressables
        yield return Addressables.InitializeAsync();
        // 检查catalog更新
        AsyncOperationHandle<List<string>> checkForCatalogUpdatesHandle = Addressables.CheckForCatalogUpdates(false);
        yield return checkForCatalogUpdatesHandle;

        if(checkForCatalogUpdatesHandle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.Log("CheckForCatalogUpdates失败: " + checkForCatalogUpdatesHandle.OperationException.Message);
            Addressables.Release(checkForCatalogUpdatesHandle);
            succeed = false;
        }
        else
        {
            OpenLoadingWindow();
            List<string> catalogResult = checkForCatalogUpdatesHandle.Result;
            Addressables.Release(checkForCatalogUpdatesHandle);

            string versionInfo = GetVersionInfo(LanguageType.SimplifiedChinese);
            Debug.Log("版本信息：" + versionInfo);

            // 有新的catalog需要更新
            if (catalogResult.Count > 0)
            {
                //更新catalog
                AsyncOperationHandle<List<IResourceLocator>> updateCatalogsHandle = Addressables.UpdateCatalogs(catalogResult, false);
                yield return updateCatalogsHandle;
                if (updateCatalogsHandle.Status == AsyncOperationStatus.Failed)
                {
                    Debug.Log("updateCatalogsHandle失败: " + updateCatalogsHandle.OperationException.Message);
                    Addressables.Release(updateCatalogsHandle);
                    succeed = false;
                }
                else
                {
                    List<IResourceLocator> locatorList = updateCatalogsHandle.Result;
                    Addressables.Release(updateCatalogsHandle);
                    // 逐个加载需要更新的资源
                    List<object> keys = new List<object>(1000);
                    foreach (IResourceLocator locator in locatorList)
                    {
                        keys.AddRange(locator.Keys);
                    }
                    SetLoadingWindowInfo();
                    yield return DownloadAssets(keys);
                }
            }
            else
            {
                Debug.Log("版本已是最新，无需更新");
            }
            CloseLoadingWindow();
        }
        

        if (hotUpdateSystemState == null) hotUpdateSystemState = new HotUpdateSystemState();
        hotUpdateSystemState.succeed = succeed;
        SaveSystem.SaveSetting(hotUpdateSystemState);

        if (succeed)
        {
            LoadUpdateDll();
            LoadAotDll();
        }
        else
        {
            Debug.Log("更新失败");
        }
        onEnd?.Invoke(succeed);
    }

    private IEnumerator DownloadAssets(List<object> keys)
    {
        
        AsyncOperationHandle<long> downloadSizeHandle = Addressables.GetDownloadSizeAsync((IEnumerable<object>)keys);
        yield return downloadSizeHandle;

        if (downloadSizeHandle.Status == AsyncOperationStatus.Failed)
        {
            Debug.Log("downloadSizeHandle失败: " + downloadSizeHandle.OperationException.Message);
            Addressables.Release(downloadSizeHandle);
            succeed = false;
        }
        else
        {
            long downloadSize = downloadSizeHandle.Result;
            Addressables.Release(downloadSizeHandle);
            if (downloadSize > 0)
            {
                // 下载依赖异常Handle
                AsyncOperationHandle downloadDependenciesHandle = Addressables.DownloadDependenciesAsync((IEnumerable<object>)keys, Addressables.MergeMode.Union, false);

                while (!downloadDependenciesHandle.IsDone)
                {
                    if (downloadDependenciesHandle.Status == AsyncOperationStatus.Failed)
                    {
                        Debug.Log("downloadDependenciesHandle失败: " + downloadDependenciesHandle.OperationException.Message);
                        succeed = false;
                        break;
                    }

                    float downloadPercent = downloadDependenciesHandle.GetDownloadStatus().Percent;
                    onUpdatePercentDo?.Invoke(downloadPercent);
                    // 更新UI面板的进度
                    UpdateLoadingWindow(downloadSize * downloadPercent, downloadSize);
                    Debug.Log("当前文件下载进度: " + downloadPercent * 100 + "%");
                    yield return new WaitForSecondsRealtime(0.5f);
                }

                if (downloadDependenciesHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log($"当前文件下载完成");
                }

                Addressables.Release(downloadDependenciesHandle);
            }
        }
    }

    private void SetLoadingWindowInfo()
    {
        GameBasicSetting basicSetting = SaveSystem.LoadSetting<GameBasicSetting>();
        LanguageType languageType;
        // 如果没有设置信息，那么就看系统语言是否是简体中文，否则都为英文
        if (basicSetting == null) languageType = Application.systemLanguage == SystemLanguage.ChineseSimplified ? LanguageType.SimplifiedChinese : LanguageType.English;
        else languageType = basicSetting.LanguageType; 
        loadingWindow.SetDescription(GetVersionInfo(languageType));
    }
    private string GetVersionInfo(LanguageType languageType)
    {
        Addressables.DownloadDependenciesAsync(versionInfoAddressableKey, true).WaitForCompletion();
        GameBasicSetting basicSetting = Addressables.LoadAssetAsync<GameBasicSetting>(versionInfoAddressableKey).WaitForCompletion();
        string content = basicSetting.GetVersionData(languageType).description;
        Addressables.Release(basicSetting);
        return content;
    }

    private void LoadUpdateDll()
    {
        foreach (string dllName in hotUpdateDllKeyList)
        {
            TextAsset textAsset = Addressables.LoadAssetAsync<TextAsset>(dllName).WaitForCompletion();
            System.Reflection.Assembly.Load(textAsset.bytes);
        }
        Debug.Log("HotUpdate所有dll加载完成");
    }

    private void LoadAotDll()
    {
        foreach (TextAsset aotText in aotDllTextList)
        {
            byte[] bytes = aotText.bytes;
            LoadImageErrorCode errorCode = RuntimeApi.LoadMetadataForAOTAssembly(bytes, HomologousImageMode.SuperSet);
            if(errorCode != LoadImageErrorCode.OK)
            {
                Debug.Log("加载AOT错误: " + errorCode);
            }
        }
        Debug.Log("Aot所有dll加载完成");
    }

    private UI_LoadingWindow loadingWindow; 
    private void OpenLoadingWindow()
    {
        loadingWindow = UISystem.Show<UI_LoadingWindow>();
        loadingWindow.SetDescription("Loading");
    }

    private void CloseLoadingWindow()
    {
        UISystem.Close<UI_LoadingWindow>();
        loadingWindow = null;
    }

    private void UpdateLoadingWindow(float now, float max)
    {
        if (loadingWindow == null) return;
        loadingWindow.UpdateProgressByBytes(now, max);
    }
}
