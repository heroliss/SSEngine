using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class HallController : MonoBehaviour
{
    public Button GameButton1;
    public Button GameButton2;

    private void OnEnable()
    {
        GameButton1.onClick.AddListener(() =>
        {
            Addressables.LoadSceneAsync("Game1/Scenes/Game1Scene.unity");
        });
        GameButton2.onClick.AddListener(() =>
        {
            Addressables.LoadSceneAsync("Game2/Scenes/Game2Scene.unity");
        });
    }
}
