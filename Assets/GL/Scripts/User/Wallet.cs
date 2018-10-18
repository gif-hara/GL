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
        private WalletElement gold;

        /// <summary>
        /// お金
        /// </summary>
        /// <remarks>
        /// ゲーム内で取得できる通貨
        /// </remarks>
        public WalletElement Gold => this.gold;
    }
}
