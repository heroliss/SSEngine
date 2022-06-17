using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
class MyValue
{
    public int x;
    public float y;
    public string s;
}
enum EEE { aaa, bbb }
public class App
{
    public static void Main()
    {
        Debug.Log("HotUpdate loaded successfully!");
        Init();
    }

    static async void Init()
    {
        Debug.Log("HotUpdate Init");
        if (Application.isEditor == false)
        {
            await LoadMetadataForAOTAssembly();
            TestAOTGeneric();
        }
        await LoadHallScene();
        Debug.Log("HotUpdate Init End");
    }

    static async Task LoadHallScene()
    {
        //加载大厅场景
        var a = await Addressables.LoadSceneAsync("Scenes/HallScene.unity").Task;
        if (a.Scene == null)
        {
            Debug.LogError("加载大厅场景失败！");
        }
        else
        {
            Debug.Log("加载大厅场景成功！");
        }
    }


    /// <summary>
    /// 测试 aot泛型，这个代码大家自己主动调吧
    /// </summary>
    public static void TestAOTGeneric()
    {
        var arr = new List<MyValue>();
        Dictionary<EEE, List<MyValue>> dic = new Dictionary<EEE, List<MyValue>>();
        dic.Add(EEE.bbb, new List<MyValue>());
        dic[EEE.bbb].Add(new MyValue() { x = 00000, y = 1111, s = "6578" });
        KeyValuePair<EEE, List<MyValue>> pair = dic.ElementAt(0);
        Debug.LogFormat("=======> AOT泛型测试：   x:{0} y:{1} s:{2}", dic[EEE.bbb][0].x, dic[EEE.bbb][0].y, dic[EEE.bbb][0].s);
        Debug.LogFormat("=======> AOT泛型测试222：   x:{0} y:{1} s:{2}", pair.Value[0].x, pair.Value[0].y, pair.Value[0].s);
    }

    /// <summary>
    /// 为aot assembly加载原始metadata， 这个代码放aot或者热更新都行。
    /// 一旦加载后，如果AOT泛型函数对应native实现不存在，则自动替换为解释模式执行
    /// </summary>
    public static async Task LoadMetadataForAOTAssembly()
    {
        Debug.Log("Load Metadata For AOT Assembly");
        // 可以加载任意aot assembly的对应的dll。但要求dll必须与unity build过程中生成的裁剪后的dll一致，而不能直接使用
        // 原始dll。
        // 这些dll可以在目录 Temp\StagingArea\Il2Cpp\Managed 下找到。
        // 对于Win Standalone，也可以在 build目录的 {Project}/Managed目录下找到。
        // 对于Android及其他target, 导出工程中并没有这些dll，因此还是得去 Temp\StagingArea\Il2Cpp\Managed 获取。
        //
        // 这里以最常用的mscorlib.dll举例
        //
        // 加载打包时 unity在build目录下生成的 裁剪过的 mscorlib，注意，不能为原始mscorlib
        //
        var dllList = await Addressables.LoadAssetAsync<BootStrap.DllList>("DLL/DllList").Task;
        Debug.Log("====================>" + dllList); //TODO: ？？？？？？？？？？？
        foreach (var dllName in dllList.List)
        {
            await LoadDllByteFile($"DLL/{dllName}.bytes");
        }
    }

    private static async Task LoadDllByteFile(string asset)
    {
        try
        {
            Debug.Log($"LoadDllByteFile: {asset}");
            var handle = Addressables.LoadAssetAsync<TextAsset>(asset);
            var result = await handle.Task;
            LoadMetadataForAOTAssembly(result);
            Addressables.Release(handle);
            Debug.Log($"LoadDllByteFile {asset} End!");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    static unsafe void LoadMetadataForAOTAssembly(TextAsset textAsset)
    {
        Debug.Log("Start Load Metadata For AOT Assembly. data size:" + textAsset.bytes.Length);
        byte[] dllBytes = textAsset.bytes;
        fixed (byte* ptr = dllBytes)
        {
            // 加载assembly对应的dll，会自动为它hook。一旦aot泛型函数的native函数不存在，用解释器版本代码
            int err = Huatuo.HuatuoApi.LoadMetadataForAOTAssembly((IntPtr)ptr, dllBytes.Length);
            Debug.Log("End Load Metadata For AOT Assembly. ret:" + err);
        }
    }
}
