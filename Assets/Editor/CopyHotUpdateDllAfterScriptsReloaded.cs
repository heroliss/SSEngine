using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CopyHotUpdateDllAfterScriptsReloaded
{
    [UnityEditor.Callbacks.DidReloadScripts]
    private static void OnScriptsReloaded()
    {
        HuaTuo.HuaTuoEditorHelper.CompileDllActiveBuildTarget(false);
    }
}
