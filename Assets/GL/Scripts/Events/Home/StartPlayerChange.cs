using GL.User;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Events.Home
{
    /// <summary>
    /// プレイヤー交代処理を開始した際のイベント
    /// </summary>
    public sealed class StartPlayerChange : Message<StartPlayerChange, Player>
    {
        /// <summary>
        /// 交代したいプレイヤー
        /// </summary>
        public Player Target => this.param1;
    }
}
