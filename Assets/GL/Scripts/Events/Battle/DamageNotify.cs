using GL.Scripts.Battle.CharacterControllers;
using HK.Framework.EventSystems;

namespace HK.GL.Events.Battle
{
    /// <summary>
    /// ダメージを通知するイベント
    /// </summary>
    public sealed class DamageNotify : Message<DamageNotify, Character, int>
    {
        /// <summary>
        /// ダメージを受けたヤーツ
        /// </summary>
        public Character Receiver{ get{ return this.param1; } }

        /// <summary>
        /// ダメージ量
        /// </summary>
        public int Value{ get{ return this.param2; } }
    }
}
