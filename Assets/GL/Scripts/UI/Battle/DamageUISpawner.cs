using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.GL.Events;
using HK.GL.Events.Battle;
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

        private Transform cachedTransform;

        void Awake()
        {
            this.cachedTransform = this.transform;
            Assert.IsNotNull(this.cachedTransform);

            GLEvent.GlobalBroker.Receive<DamageNotify>()
                .SubscribeWithState(this, (d, _this) => _this.Create(d.Receiver.transform, d.Value))
                .AddTo(this);
        }

        public void Create(Transform receiver, int damage)
        {
            var instance = Instantiate(this.controller, this.cachedTransform, false);
            instance.SetProperty(receiver, damage);
        }
    }
}
