using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.User
{
    /// <summary>
    /// お金類を管理するクラス
    /// </summary>
    /// <remarks>
    /// 課金システムも導入したい
    /// </remarks>
    [Serializable]
    public sealed class Wallet
    {
        [SerializeField]
        private int gold;

        /// <summary>
        /// お金
        /// </summary>
        /// <remarks>
        /// ゲーム内で取得できる通貨
        /// </remarks>
        public int Gold => this.gold;

        /// <summary>
        /// <see cref="this.gold"/>で<paramref name="value"/>を支払う
        /// </summary>
        /// <param name="value"></param>
        public void PayFromGold(int value)
        {
            Assert.IsTrue((this.gold - value) > 0, $"お金が足りないのに減算処理が実行されました gold = {this.gold}, value = {value}");

            this.gold -= value;
        }

        /// <summary>
        /// お金を加算する
        /// </summary>
        public void AddFromGold(int value)
        {
            this.gold += value;
        }
    }
}
