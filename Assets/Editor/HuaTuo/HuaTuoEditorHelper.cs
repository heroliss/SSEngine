using HuaTuo.Generators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Player;
using UnityEngine;
using FileMode = System.IO.FileMode;

namespace HuaTuo
{
    /// <summary>
    /// 这里仅仅是一个流程展示
    /// 简单说明如果你想将huatuo的dll做成自动化的简单实现
    /// </summary>
    public class HuaTuoEditorHelper
    {
        /// <summary>
        /// 打包时，将热更新脚本自动加入到link include的assembly列表，对于普通类，就不会出现因为类型裁剪导致热更新dll运行时
        /// 报 TypeMissing的错误了。
        /// 但====注意====，这个办法只是方便新手体验热更新。打包后，热更新dll新增类型引用，如果被裁剪了，依然会报错的。
        /// 因此正式的工作流，还是得靠link.xml里preserve你想要的类，后续我们会提供更正式的工作流及相关工具。
        /// 另外，这个办法只能解决普通类裁剪的问题，不能解决AOT泛型函数实例化缺失的问题。
        /// </summary>
        [InitializeOnLoadMethod]
        private static void Setup()
        {
            //var linkAdds = string.Join(" ", HuaTuo_BuildProcessor_2020_1_OR_NEWER.s_allHotUpdateDllNames
            //    .Select(s => $"--include-assembly={Path.Combine(Environment.CurrentDirectory, $"Temp/StagingArea/Data/Managed/{s}").Replace('\\', '/')}"));
            //var envVar = Environment.GetEnvironmentVariable("UNITYLINKER_ADDITIONAL_ARGS");

            //if (envVar != linkAdds)
            //{
            //    Environment.SetEnvironmentVariable("UNITYLINKER_ADDITIONAL_ARGS", linkAdds);
            //}
        }

        private static void CreateDirIfNotExists(string dirName)
        {
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
        }

        public static string ToReleateAssetPath(string s)
        {
            return s.Substring(s.IndexOf("Assets/"));
        }

        private static void CompileDll(string buildDir, BuildTarget target, bool showLog = true)
        {
            var group = BuildPipeline.GetBuildTargetGroup(target);

            ScriptCompilationSettings scriptCompilationSettings = new ScriptCompilationSettings();
            scriptCompilationSettings.group = group;
            scriptCompilationSettings.target = target;
            scriptCompilationSettings.options = ScriptCompilationOptions.DevelopmentBuild;
            CreateDirIfNotExists(buildDir);
            ScriptCompilationResult scriptCompilationResult = PlayerBuildInterface.CompilePlayerScripts(scriptCompilationSettings, buildDir);
            foreach (var ass in scriptCompilationResult.assemblies)
            {
                if (showLog) Debug.LogFormat("compile assemblies:{0}", ass);
            }
            //拷贝dll到Assets中
            File.Copy(Path.Combine(buildDir, "HotUpdate.dll"), "Assets/HotUpdate/DLL/HotUpdate.bytes", true);
            Debug.Log("HotUpdate.bytes 已更新\nAssets/HotUpdate/DLL/HotUpdate.bytes");
        }

        public static string DllBuildOutputDir => Path.GetFullPath($"{Application.dataPath}/../Temp/HuaTuo/build");

        public static string GetDllBuildOutputDirByTarget(BuildTarget target)
        {
            return $"{DllBuildOutputDir}/{target}";
        }

        [MenuItem("HuaTuo/CompileDll/ActiveBuildTarget")]
        public static void CompileDllActiveBuildTarget(bool showLog = true)
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            CompileDll(GetDllBuildOutputDirByTarget(target), target, showLog);
        }

        [MenuItem("HuaTuo/CompileDll/Win64")]
        public static void CompileDllWin64()
        {
            var target = BuildTarget.StandaloneWindows64;
            CompileDll(GetDllBuildOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/CompileDll/Linux64")]
        public static void CompileDllLinux()
        {
            var target = BuildTarget.StandaloneLinux64;
            CompileDll(GetDllBuildOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/CompileDll/OSX")]
        public static void CompileDllOSX()
        {
            var target = BuildTarget.StandaloneOSX;
            CompileDll(GetDllBuildOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/CompileDll/Android")]
        public static void CompileDllAndroid()
        {
            var target = BuildTarget.Android;
            CompileDll(GetDllBuildOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/CompileDll/IOS")]
        public static void CompileDllIOS()
        {
            //var target = EditorUserBuildSettings.activeBuildTarget;
            var target = BuildTarget.iOS;
            CompileDll(GetDllBuildOutputDirByTarget(target), target);
        }


        public static string AssetBundleOutputDir => Application.dataPath + "/HuaTuo/Output";

        public static string GetAssetBundleOutputDirByTarget(BuildTarget target)
        {
            return $"{AssetBundleOutputDir}/{target}";
        }

        public static string GetAssetBundleTempDirByTarget(BuildTarget target)
        {
            return $"{Application.dataPath}/HuaTuo/AssetBundleTemp/{target}";
        }

        /*

        /// <summary>
        /// 将HotFix.dll和HotUpdatePrefab.prefab打入common包.
        /// 将HotUpdateScene.unity打入scene包.
        /// </summary>
        /// <param name="tempDir"></param>
        /// <param name="outputDir"></param>
        /// <param name="target"></param>
        private static void BuildAssetBundles(string tempDir, string outputDir, BuildTarget target)
        {
            CreateDirIfNotExists(tempDir);
            CreateDirIfNotExists(outputDir);

            List<string> notSceneAssets = new List<string>();

            CompileDll(GetDllBuildOutputDirByTarget(target), target);

            var hotfixDlls = new List<string>()
            {
                "HotFix.dll",
                "HotFix2.dll",
            };

            foreach (var dll in hotfixDlls)
            {
                string dllPath = $"{GetDllBuildOutputDirByTarget(target)}/{dll}";
                string dllBytesPath = $"{tempDir}/{dll}.bytes";
                File.Copy(dllPath, dllBytesPath, true);
                notSceneAssets.Add(dllBytesPath);
            }


            string testPrefab = $"{Application.dataPath}/Prefabs/HotUpdatePrefab.prefab";
            notSceneAssets.Add(testPrefab);
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);



            List<AssetBundleBuild> abs = new List<AssetBundleBuild>();
            AssetBundleBuild notSceneAb = new AssetBundleBuild
            {
                assetBundleName = "common",
                assetNames = notSceneAssets.Select(s => ToReleateAssetPath(s)).ToArray(),
            };
            abs.Add(notSceneAb);


            string testScene = $"{Application.dataPath}/Scenes/HotUpdateScene.unity";
            string[] sceneAssets =
            {
                testScene,
            };
            AssetBundleBuild sceneAb = new AssetBundleBuild
            {
                assetBundleName = "scene",
                assetNames = sceneAssets.Select(s => s.Substring(s.IndexOf("Assets/"))).ToArray(),
            };

            abs.Add(sceneAb);

            BuildPipeline.BuildAssetBundles(outputDir, abs.ToArray(), BuildAssetBundleOptions.None, target);

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);

            string streamingAssetPathDst = $"{Application.streamingAssetsPath}";
            CreateDirIfNotExists(streamingAssetPathDst);

            foreach (var ab in abs)
            {
                AssetDatabase.CopyAsset(ToReleateAssetPath($"{outputDir}/{ab.assetBundleName}"),
                    ToReleateAssetPath($"{streamingAssetPathDst}/{ab.assetBundleName}"));
            }
        }

        [MenuItem("HuaTuo/BuildBundles/ActiveBuildTarget")]
        public static void BuildSeneAssetBundleActiveBuildTarget()
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            BuildAssetBundles(GetAssetBundleTempDirByTarget(target), GetAssetBundleOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/BuildBundles/Win64")]
        public static void BuildSeneAssetBundleWin64()
        {
            var target = BuildTarget.StandaloneWindows64;
            BuildAssetBundles(GetAssetBundleTempDirByTarget(target), GetAssetBundleOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/BuildBundles/OSX")]
        public static void BuildSeneAssetBundleOSX64()
        {
            var target = BuildTarget.StandaloneOSX;
            BuildAssetBundles(GetAssetBundleTempDirByTarget(target), GetAssetBundleOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/BuildBundles/Linux64")]
        public static void BuildSeneAssetBundleLinux64()
        {
            var target = BuildTarget.StandaloneLinux64;
            BuildAssetBundles(GetAssetBundleTempDirByTarget(target), GetAssetBundleOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/BuildBundles/Android")]
        public static void BuildSeneAssetBundleAndroid()
        {
            var target = BuildTarget.Android;
            BuildAssetBundles(GetAssetBundleTempDirByTarget(target), GetAssetBundleOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/BuildBundles/IOS")]
        public static void BuildSeneAssetBundleIOS()
        {
            var target = BuildTarget.iOS;
            BuildAssetBundles(GetAssetBundleTempDirByTarget(target), GetAssetBundleOutputDirByTarget(target), target);
        }

        */

        [MenuItem("HuaTuo/Generate/MethodBridge_X64")]
        public static void MethodBridge_X86()
        {
            //var target = EditorUserBuildSettings.activeBuildTarget;
            string outputFile = $"{Application.dataPath}/../Library/Huatuo/MethodBridge_x64.cpp";
            var g = new MethodBridgeGenerator(new MethodBridgeGeneratorOptions()
            {
                CallConvention = CallConventionType.X64,
                Assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList(),
                OutputFile = outputFile,
            });

            g.PrepareMethods();
            g.Generate();
            Debug.LogFormat("== output:{0} ==", outputFile);
        }

        [MenuItem("HuaTuo/Generate/MethodBridge_Arm64")]
        public static void MethodBridge_Arm64()
        {
            string outputFile = $"{Application.dataPath}/../Library/Huatuo/MethodBridge_arm64.cpp";
            var g = new MethodBridgeGenerator(new MethodBridgeGeneratorOptions()
            {
                CallConvention = CallConventionType.Arm64,
                Assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList(),
                OutputFile = outputFile,
            });

            g.PrepareMethods();
            g.Generate();
            Debug.LogFormat("== output:{0} ==", outputFile);
        }
    }
}