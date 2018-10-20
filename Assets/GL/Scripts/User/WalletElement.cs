﻿using System;
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
        public int Value => this.value;

        public void Pay(int value)
        {
            Assert.IsTrue((this.value - value) > 0, $"足りないのに減算処理が実行されました this.value = {this.value}, value = {value}");
            this.value -= value;
        }

        public void Add(int value)
        {
            this.value += value;
        }

        public bool IsEnough(int value)
        {
            return this.value >= value;
        }
    }
}