using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using HotUpdate.GameCommon;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading.Tasks;
using System;
using Random = UnityEngine.Random;
using QFramework;
using TMPro;
using HotUpdate.Game1.Events;

namespace HotUpdate.Game1
{
    public class Game1Controller : MonoBehaviour, IController, IOnEvent<Events.CountToTenEvent>
    {
        [Tooltip("只能接收包含ColorChanger脚本的预制体")]
        public ComponentReferenceColorChanger PrefabReference;
        public Button InstantiateButton;
        public TextMeshProUGUI Text;

        //[Inject]
        //public Model.Game1Model GameModel;
        private void Awake()
        {
            GetArchitecture().InjectAll(this);

            InstantiateButton.onClick.AddListener(UniTask.UnityAction(InstantiateObj));

            //----------数据读取-------------
            //GameModel = this.GetModel<Model.Game1Model>();
            //GameModel.Count.RegisterWithInitValue(v => Text.text = $"Count:{v}").UnRegisterWhenGameObjectDestroyed(gameObject);
            this.SendQuery(query: new CountQuery()).RegisterWithInitValue(v => Text.text = $"Count:{v}").UnRegisterWhenGameObjectDestroyed(gameObject);
            //-------------------------------
        }

        async UniTaskVoid InstantiateObj()
        {
            var randomPos = new Vector3(Random.Range(-5f, maxInclusive: 5), Random.Range(-2f, 5), 0);
            var go = await Addressables.InstantiateAsync(PrefabReference, randomPos, Random.rotation, transform).Task;
            var colorChanger = go.GetComponent<ColorChanger>();
            go.transform.localScale = Vector3.zero;

            //------------数据操作-----------
            //GameModel.Count.Value++;
            this.SendCommand<AddCountCmd>();
            //-------------------------------

            await go.transform.DOScale(Vector3.one * 3f, 1f).SetEase(Ease.OutBounce).AsyncWaitForCompletion();
            colorChanger.ResetColor();
            await UniTask.Delay(3000);
            await go.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBounce).AsyncWaitForCompletion();
            Addressables.ReleaseInstance(go);
        }

        public IArchitecture GetArchitecture()
        {
            return Game1Architecture.Interface;
        }

        public async void OnEvent(CountToTenEvent e)
        {
            await Text.transform.DOScale(2, 0.5f).AsyncWaitForCompletion();
            Text.transform.DOScale(1, 0.5f);
        }
    }
}
