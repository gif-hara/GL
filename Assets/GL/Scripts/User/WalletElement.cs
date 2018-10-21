using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.User
{
    /// <summary>
    /// <see cref="Wallet"/>の要素
    /// </summary>
    [Serializable]
    public sealed class WalletElement
    {
        [SerializeField]
        private int value;
        public int Value => this.ReactiveProperty.Value;

        private ReactiveProperty<int> reactiveProperty = null;
        public IReadOnlyReactiveProperty<int> ReactiveProperty
        {
            get
            {
                return this.GetReactiveProperty;
            }
        }

        private ReactiveProperty<int> GetReactiveProperty
        {
            get
            {
                this.reactiveProperty = this.reactiveProperty ?? new ReactiveProperty<int>(this.value);
                return this.reactiveProperty;
            }
        }

        public void Pay(int value)
        {
            Assert.IsTrue((this.Value - value) > 0, $"足りないのに減算処理が実行されました this.value = {this.value}, value = {value}");
            this.GetReactiveProperty.Value -= value;
            this.value = this.Value;
        }

        public void Add(int value)
        {
            this.GetReactiveProperty.Value += value;
            this.value = this.Value;
        }

        public bool IsEnough(int value)
        {
            return this.GetReactiveProperty.Value >= value;
        }
    }
}
