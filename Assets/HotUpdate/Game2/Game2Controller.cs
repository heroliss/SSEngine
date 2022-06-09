using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

namespace HotUpdate.Game2
{
    public class Game2Controller : MonoBehaviour
    {
        public Button BackButton;
        void Awake()
        {
            BackButton.onClick.AddListener(() =>
            {
                Addressables.LoadSceneAsync("Scenes/HallScene.unity");
            });
        }

    }
}
