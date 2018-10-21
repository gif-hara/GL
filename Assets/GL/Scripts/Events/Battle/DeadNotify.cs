using GL.Battle.CharacterControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Events.Battle
{
    /// <summary>
    /// キャラクターが死亡したことを通知するイベント
    /// </summary>
    public sealed class DeadNotify : Message<DeadNotify, Character>
    {
        public Character Character => this.param1;
    }
}
