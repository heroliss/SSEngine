using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.IO;

public class ProcessBuild : IPreprocessBuildWithReport, IPostprocessBuildWithReport
{
    public int callbackOrder => 1000;

    public void OnPostprocessBuild(BuildReport report)
    {
        //Debug.Log("��������Temp/StagingArea/Data/Managed/mscorlib.dll �� Assets/HotUpdate/mscorlib.bytes");
        //File.Copy("Temp/StagingArea/Data/Managed/mscorlib.dll", "Assets/HotUpdate/mscorlib.bytes", true);
        //Debug.Log("mscorlib.bytes �Ѹ���");
    }

    public void OnPreprocessBuild(BuildReport report)
    {
    }
}