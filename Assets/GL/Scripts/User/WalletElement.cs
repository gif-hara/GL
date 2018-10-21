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
                this.reactiveProperty = this.reactiveProperty ?? new ReactiveProperty<int>(this.value);
                return this.reactiveProperty;
            }
        }

        public void Pay(int value)
        {
            Assert.IsTrue((this.value - value) > 0, $"足りないのに減算処理が実行されました this.value = {this.value}, value = {value}");
            this.reactiveProperty.Value -= value;
        }

        public void Add(int value)
        {
            this.reactiveProperty.Value += value;
        }

        public bool IsEnough(int value)
        {
            return this.reactiveProperty.Value >= value;
        }
    }
}
