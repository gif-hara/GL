using GL.Scripts.Events.Battle;
using UnityEngine;
using UnityEngine.Assertions;
using HK.Framework.EventSystems;
using UniRx;

namespace HK.GL.UI.Battle
{
    /// <summary>
    /// ダメージUIを生成するヤーツ
    /// </summary>
    public sealed class DamageUISpawner : MonoBehaviour
    {
        [SerializeField]
        private DamageUIController controller;

        [SerializeField]
        private RectTransform canvasTransform;

        [SerializeField]
        private Camera uiCamera;

        [SerializeField]
        private Camera worldCamera;

        private Transform cachedTransform;

        void Awake()
        {
            this.cachedTransform = this.transform;
            Assert.IsNotNull(this.cachedTransform);

            Broker.Global.Receive<DamageNotify>()
                .SubscribeWithState(this, (d, _this) => _this.Create(d.Receiver.transform, d.Value))
                .AddTo(this);
        }

        public void Create(Transform receiver, int damage)
        {
            var instance = Instantiate(this.controller, this.cachedTransform, false);
            instance.SetProperty(receiver, damage, this.canvasTransform, this.uiCamera, this.worldCamera);
        }
    }
}
