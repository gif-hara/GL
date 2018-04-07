using GL.Scripts.Battle.CharacterControllers;
using HK.Framework.EventSystems;

namespace HK.GL.Events.Battle
{
    /// <summary>
    /// ターンが終了した際に通知するイベント
    /// </summary>
    public sealed class EndTurn : Message<EndTurn, Character>
    {
        /// <summary>
        /// 今回行動したキャラクター
        /// </summary>
        public Character Character { get { return this.param1; } }
    }
}
