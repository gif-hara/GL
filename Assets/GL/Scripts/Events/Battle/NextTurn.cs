using System.Collections.Generic;
using GL.Battle.CharacterControllers;
using HK.Framework.EventSystems;

namespace GL.Events.Battle
{
    /// <summary>
    /// 次のターンに進んだことを通知するイベント
    /// </summary>
    public sealed class NextTurn : Message<NextTurn, List<Character>>
    {
        /// <summary>
        /// 行動順
        /// </summary>
        public List<Character> Order { get{ return this.param1; } }

        /// <summary>
        /// 次に行動するキャラクターを返す
        /// </summary>
        public Character NextCharacter{ get{ return this.Order[0]; } }
    }
}
