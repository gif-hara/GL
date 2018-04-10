using GL.Scripts.Battle.CharacterControllers;
using HK.Framework.EventSystems;

namespace GL.Scripts.Events.Battle
{
    /// <summary>
    /// 回復を通知するイベント
    /// </summary>
    public sealed class RecoveryNotify : Message<RecoveryNotify, Character, int>
    {
        /// <summary>
        /// 回復したキャラクター
        /// </summary>
        public Character Receiver { get { return this.param1; } }

        /// <summary>
        /// 回復量
        /// </summary>
        public int Value { get { return this.param2; } }
    }
}
