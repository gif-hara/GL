using GL.User;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Events.Home
{
    /// <summary>
    /// プレイヤー交代処理を完了した際のイベント
    /// </summary>
    public sealed class CompletePlayerChange : Message<CompletePlayerChange, Player>
    {
        /// <summary>
        /// 交代されたプレイヤー
        /// </summary>
        public Player Changed => this.param1;
    }
}
