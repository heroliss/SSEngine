using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
struct MyValue
{
    public int x;
    public float y;
    public string s;
}
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
        //���ش�������
        var a = await Addressables.LoadSceneAsync("Scenes/HallScene.unity").Task;
        if (a.Scene == null)
        {
            Debug.LogError("���ش�������ʧ�ܣ�");
        }
        else
        {
            Debug.Log("���ش��������ɹ���");
        }
    }


    /// <summary>
    /// ���� aot���ͣ�����������Լ���������
    /// </summary>
    public static void TestAOTGeneric()
    {
        var arr = new List<MyValue>();
        arr.Add(new MyValue() { x = 1000, y = 144550, s = "abcghjdfgjhdgh" });
        var e = arr[0];
        Debug.LogFormat("=======> AOT���Ͳ��ԣ�   x:{0} y:{1} s:{2}", e.x, e.y, e.s);
    }

    /// <summary>
    /// Ϊaot assembly����ԭʼmetadata�� ��������aot�����ȸ��¶��С�
    /// һ�����غ����AOT���ͺ�����Ӧnativeʵ�ֲ����ڣ����Զ��滻Ϊ����ģʽִ��
    /// </summary>
    public static async Task LoadMetadataForAOTAssembly()
    {
        Debug.Log("Load Metadata For AOT Assembly");
        // ���Լ�������aot assembly�Ķ�Ӧ��dll����Ҫ��dll������unity build���������ɵĲü����dllһ�£�������ֱ��ʹ��
        // ԭʼdll��
        // ��Щdll������Ŀ¼ Temp\StagingArea\Il2Cpp\Managed ���ҵ���
        // ����Win Standalone��Ҳ������ buildĿ¼�� {Project}/ManagedĿ¼���ҵ���
        // ����Android������target, ���������в�û����Щdll����˻��ǵ�ȥ Temp\StagingArea\Il2Cpp\Managed ��ȡ��
        //
        // ��������õ�mscorlib.dll����
        //
        // ���ش��ʱ unity��buildĿ¼�����ɵ� �ü����� mscorlib��ע�⣬����Ϊԭʼmscorlib
        //
        await LoadDllByteFile("DLL/mscorlib.bytes");
        await LoadDllByteFile("DLL/UniTask.Addressables.bytes");
        await LoadDllByteFile("DLL/UniTask.bytes");
        await LoadDllByteFile("DLL/UniTask.DOTween.bytes");
        await LoadDllByteFile("DLL/UniTask.Linq.bytes");
        await LoadDllByteFile("DLL/UniTask.TextMeshPro.bytes");
        await LoadDllByteFile("DLL/Unity.Addressables.bytes");
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
        Debug.Log("Start Load Metadata For AOT Assembly. data size:" + textAsset.dataSize);
        byte[] dllBytes = textAsset.bytes;
        fixed (byte* ptr = dllBytes)
        {
            // ����assembly��Ӧ��dll�����Զ�Ϊ��hook��һ��aot���ͺ�����native���������ڣ��ý������汾����
            int err = Huatuo.HuatuoApi.LoadMetadataForAOTAssembly((IntPtr)ptr, dllBytes.Length);
            Debug.Log("End Load Metadata For AOT Assembly. ret:" + err);
        }
    }
}