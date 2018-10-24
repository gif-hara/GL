using GL.Battle.CharacterControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Events.Battle
{
    /// <summary>
    /// ステータスに変更があった際のイベント
    /// </summary>
    public sealed class ModifiedStatus : Message<ModifiedStatus, Character>
    {
        public Character Character => this.param1;
    }
}
