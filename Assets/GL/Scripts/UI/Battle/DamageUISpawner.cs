using GL.Events.Battle;
using UnityEngine;
using UnityEngine.Assertions;
using HK.Framework.EventSystems;
using UniRx;

namespace GL.Battle.UI
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
                .SubscribeWithState(this, (x, _this) => _this.CreateAsDamage(x.Receiver.transform, x.Value))
                .AddTo(this);

            Broker.Global.Receive<RecoveryNotify>()
                .SubscribeWithState(this, (x, _this) => _this.CreateAsRecovery(x.Receiver.transform, x.Value))
                .AddTo(this);
        }

        /// <summary>
        /// ダメージとしてダメージUIを生成する
        /// </summary>
        public void CreateAsDamage(Transform receiver, int damage)
        {
            var instance = Instantiate(this.controller, this.cachedTransform, false);
            instance.AsDamage(receiver, damage, this.canvasTransform, this.uiCamera, this.worldCamera);
        }
        
        /// <summary>
        /// 回復としてダメージUIを生成する
        /// </summary>
        public void CreateAsRecovery(Transform receiver, int damage)
        {
            var instance = Instantiate(this.controller, this.cachedTransform, false);
            instance.AsRecovery(receiver, damage, this.canvasTransform, this.uiCamera, this.worldCamera);
        }
    }
}
