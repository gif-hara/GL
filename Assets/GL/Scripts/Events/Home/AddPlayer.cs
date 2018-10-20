using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Events.Home
{
    /// <summary>
    /// プレイヤーを追加した際のイベント
    /// </summary>
    public sealed class AddPlayer : Message<AddPlayer, GL.Battle.CharacterControllers.Blueprint>
    {
        public GL.Battle.CharacterControllers.Blueprint Blueprint => this.param1;
    }
}
