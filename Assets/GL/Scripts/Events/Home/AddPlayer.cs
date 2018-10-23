using GL.Database;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Events.Home
{
    /// <summary>
    /// プレイヤーを追加した際のイベント
    /// </summary>
    public sealed class AddPlayer : Message<AddPlayer, CharacterRecord>
    {
        public CharacterRecord Blueprint => this.param1;
    }
}
