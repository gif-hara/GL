using GL.User;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Events.Home
{
    /// <summary>
    /// プレイヤーがレベルアップしたことを通知するイベント
    /// </summary>
    public sealed class LevelUppedPlayer : Message<LevelUppedPlayer, Player>
    {
        public Player Player => this.param1;
    }
}
