using GL.Battle.CharacterControllers;
using HK.Framework.EventSystems;

namespace GL.Scripts.Events.Battle
{
    /// <summary>
    /// ダメージを通知するイベント
    /// </summary>
    public sealed class DamageNotify : Message<DamageNotify, Character, int>
    {
        /// <summary>
        /// ダメージを受けたキャラクター
        /// </summary>
        public Character Receiver { get { return this.param1; } }

        /// <summary>
        /// ダメージ量
        /// </summary>
        public int Value { get { return this.param2; } }
    }
}
