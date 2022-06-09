using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.IO;

namespace BootStrap
{
    public class BootStrap : MonoBehaviour
    {
        async void Start()
        {
            Debug.Log("Bootstrap start");
            await UpdateCatalog();
            await DownAssetImpl().ToUniTask();
            await LoadHotUpdateDllAndRunMain();
        }

        /// <summary>
        /// 加载热更dll，并运行Main方法
        /// </summary>
        async Task LoadHotUpdateDllAndRunMain()
        {
            Debug.Log("Load HotUpdate Dll And Run Main");
            System.Reflection.Assembly hotUpdateAssembly;
            //#if UNITY_EDITOR
            //            hotUpdateAssembly = AppDomain.CurrentDomain.GetAssemblies().First(assembly => assembly.GetName().Name == "HotUpdate");
            //#else
            TextAsset dllBytes = await Addressables.LoadAssetAsync<TextAsset>("HotUpdate").Task;
            hotUpdateAssembly = System.Reflection.Assembly.Load(dllBytes.bytes);
            //hotUpdateAssembly = System.Reflection.Assembly.GetAssembly(typeof(App));
            //#endif
            if (hotUpdateAssembly == null)
            {
                Debug.LogError("HotUpdate.dll 加载失败!");
                return;
            }
            var appType = hotUpdateAssembly.GetType("App");
            var mainMethod = appType.GetMethod("Main");
            mainMethod.Invoke(null, null);
        }

        private List<object> _updateKeys = new List<object>();

        private async Task UpdateCatalog()
        {
            Debug.Log("Update catalog start");
            //初始化Addressable
            var init = Addressables.InitializeAsync();
            await init.Task;

            //开始连接服务器检查更新
            var handle = Addressables.CheckForCatalogUpdates(false);
            await handle.Task;
            Debug.Log("check catalog status " + handle.Status);
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                List<string> catalogs = handle.Result;
                if (catalogs != null && catalogs.Count > 0)
                {
                    foreach (var catalog in catalogs)
                    {
                        Debug.Log("catalog  " + catalog);
                    }
                    Debug.Log("download catalog start ");
                    var updateHandle = Addressables.UpdateCatalogs(catalogs, false);
                    await updateHandle.Task;
                    foreach (var item in updateHandle.Result)
                    {
                        Debug.Log("catalog result " + item.LocatorId);
                        foreach (var key in item.Keys)
                        {
                            Debug.Log("catalog key " + key);
                        }
                        _updateKeys.AddRange(item.Keys);
                    }
                    Debug.Log("download catalog finish " + updateHandle.Status);
                }
                else
                {
                    Debug.Log("dont need update catalogs");
                }
            }
            Addressables.Release(handle);
        }

        public void DownLoad()
        {
            StartCoroutine(DownAssetImpl());
        }

        public IEnumerator DownAssetImpl()
        {
            Debug.Log("Download assets start");
            var downloadsize = Addressables.GetDownloadSizeAsync(_updateKeys);
            yield return downloadsize;
            Debug.Log("start download size :" + downloadsize.Result);

            if (downloadsize.Result > 0)
            {
                var download = Addressables.DownloadDependenciesAsync(_updateKeys, Addressables.MergeMode.Union);
                yield return download;
                //await download.Task;
                Debug.Log("download result type " + download.Result.GetType());
                foreach (var item in download.Result as List<UnityEngine.ResourceManagement.ResourceProviders.IAssetBundleResource>)
                {
                    var ab = item.GetAssetBundle();
                    Debug.Log("ab name " + ab.name);
                    foreach (var name in ab.GetAllAssetNames())
                    {
                        Debug.Log("asset name " + name);
                    }
                }
                Addressables.Release(download);
            }
            Addressables.Release(downloadsize);
            Debug.Log("Download assets end");
        }
    }
}
