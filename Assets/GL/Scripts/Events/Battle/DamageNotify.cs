using GL.Battle.CharacterControllers;
using HK.Framework.EventSystems;

namespace GL.Events.Battle
{
    /// <summary>
    /// ダメージを通知するイベント
    /// </summary>
    public sealed class DamageNotify : Message<DamageNotify, Character, int, bool, Character>
    {
        /// <summary>
        /// ダメージを受けたキャラクター
        /// </summary>
        public Character Receiver => this.param1;

        /// <summary>
        /// ダメージ量
        /// </summary>
        public int Value => this.param2;

        /// <summary>
        /// 当たったか
        /// </summary>
        public bool IsHit => this.param3;

        /// <summary>
        /// ダメージを与えたキャラクター
        /// </summary>
        public Character Invoker => this.param4;
    }
}
