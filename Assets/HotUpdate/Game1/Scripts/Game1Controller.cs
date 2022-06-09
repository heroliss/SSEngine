using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using HotUpdate.GameCommon;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace HotUpdate.Game1
{
    public class Game1Controller : MonoBehaviour
    {
        [Tooltip("只能接收包含ColorChanger脚本的预制体")]
        public ComponentReferenceColorChanger PrefabReference;
        public Button InstantiateButton;

        private void OnEnable()
        {
            InstantiateButton.onClick.AddListener(InstantiateObj /*UniTask.UnityAction(InstantiateObj)*/);

        }

        private void OnDisable()
        {
            InstantiateButton.onClick.RemoveAllListeners();
        }

        async void InstantiateObj()
        {
            var randomPos = new Vector3(Random.Range(-5f, 5), Random.Range(-2f, 5), 0);
            var go = await Addressables.InstantiateAsync(PrefabReference, randomPos, Random.rotation, transform).Task;
            var colorChanger = go.GetComponent<ColorChanger>();
            colorChanger.SetColor(Random.ColorHSV());
            go.transform.localScale = Vector3.zero;
            await go.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce).AsyncWaitForCompletion();
            colorChanger.ResetColor();
            await UniTask.Delay(3000);
            await go.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBounce).AsyncWaitForCompletion();
            Addressables.ReleaseInstance(go);
        }
    }
}
